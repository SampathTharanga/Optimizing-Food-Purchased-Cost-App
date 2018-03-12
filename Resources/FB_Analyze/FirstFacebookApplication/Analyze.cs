using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook;
using Facebook.MiniJSON;

namespace FB_Analyze
{
    public partial class Analyze : Form
    {
        protected readonly FacebookClient _fb;

        private string usrid;
        private string usrname;
        private string name;

        public Analyze(FacebookClient fb)
        {
            if (fb == null)
                throw new ArgumentNullException("fb");

            _fb = fb;

            InitializeComponent();
        }

        private void Analyze_Load(object sender, EventArgs e)
        {
            var resultusr = _fb.Get("me", new { fields = new[] { "id", "name" } });
            var dictusr = Json.Deserialize(resultusr.ToString()) as Dictionary<string, object>;
            usrid = dictusr["id"].ToString();
            usrname = dictusr["name"].ToString();

            // print user id and data
            textBox1.Text = usrid + " ";
            textBox1.Text += usrname + " ";
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {



            var result = _fb.Get("246389925824704", new { fields = new[] { "id,name,posts{id,message,likes{id,name}}" } });

            //JsonResult res = JsonConvert.DeserializeObject<JsonResult>(result.ToString());

            
            var dict = Json.Deserialize(result.ToString()) as Dictionary<string, object>;

            
            name = dict["name"].ToString();

            object objData;
            if (dict.TryGetValue("posts", out objData))
            {
                var dataDict = ((Dictionary<string, object>)(objData));

                object objPosts;
                if (dataDict.TryGetValue("data", out objPosts))
                {
                    int cnt = 0;

                    var listPost = (List<object>)(objPosts);

                    

                    foreach (var pair in listPost)
                    {
                        var dataPosts = ((Dictionary<string, object>)(pair));

                        object likeDict;
                        if (dataPosts.TryGetValue("likes", out likeDict))
                        {
                            var listlikes = ((Dictionary<string, object>)(likeDict));

                            object objLikes;
                            if (listlikes.TryGetValue("data", out objLikes))
                            {

                                var objLike = (List<object>)(objLikes);

                                foreach (var like in objLike)
                                {
                                    var likeobj = ((Dictionary<string, object>)(like));
                                    if (likeobj.ContainsKey("id"))
                                    {
                                        if (usrid == likeobj["id"].ToString())
                                        {
                                            if (dataPosts.ContainsKey("id"))
                                            {
                                                // methanin ganna user like karapu post wala id list eka
                                                textBox1.Text += dataPosts["id"].ToString();
                                            }
                                        }

                                    }
                                    

                                }
                            }

                        }

                        
                        
                        cnt = cnt + 1;
                    }
                }
            }

        }
    }
}

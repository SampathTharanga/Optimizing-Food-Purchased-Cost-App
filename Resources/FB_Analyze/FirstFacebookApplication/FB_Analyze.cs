namespace FB_Analyze
{
    using System;
    using System.Windows.Forms;
    using Facebook;

    public partial class FB_Analyze : Form
    {
        private const string AppId = "338669506535489";
        private const string ExtendedPermissions = "user_about_me,user_posts";
        private string _accessToken;
        Analyze fbAnalyze;

        public FB_Analyze()
        {
            InitializeComponent();
        }

        private void btnFacebookLogin_Click(object sender, EventArgs e)
        {
            var fbLoginDialog = new FB_LoginDialog(AppId, ExtendedPermissions);
            fbLoginDialog.ShowDialog();

            DisplayAppropriateMessage(fbLoginDialog.FacebookOAuthResult);
        }

        private void DisplayAppropriateMessage(FacebookOAuthResult facebookOAuthResult)
        {
            if (facebookOAuthResult != null)
            {
                if (facebookOAuthResult.IsSuccess)
                {
                    _accessToken = facebookOAuthResult.AccessToken;
                    var fb = new FacebookClient(facebookOAuthResult.AccessToken);

                    fbAnalyze = new Analyze(fb);
                    fbAnalyze.Show();

                    btnLogout.Visible = true;
                }
                else
                {
                    MessageBox.Show(facebookOAuthResult.ErrorDescription);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var webBrowser = new WebBrowser();
            var fb = new FacebookClient();
            var logouUrl = fb.GetLogoutUrl(new { access_token = _accessToken, next = "https://www.facebook.com/connect/login_success.html" });
            webBrowser.Navigate(logouUrl);
            fbAnalyze.Close();
            btnLogout.Visible = false;
        }

        private void FB_Analyze_Load(object sender, EventArgs e)
        {

        }

    }
}

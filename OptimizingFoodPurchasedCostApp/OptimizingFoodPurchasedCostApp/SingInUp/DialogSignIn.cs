using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.IO;

namespace OptimizingFoodPurchasedCostApp
{
    public class onSignInEventArgs : EventArgs
    {
        private string loginEmail;
        private string loginPassword;

        public string login_Email
        {
            get { return loginEmail; }
            set { loginEmail = value; }
        }

        public string login_password
        {
            get { return loginPassword; }
            set { loginPassword = value; }
        }

        public onSignInEventArgs(string logEmail, string logPass) : base()
        {
            loginEmail = logEmail;
            loginPassword = logPass;
        }
    }

    class DialogSignIn : DialogFragment
    {
        Button btnSignIn;
        EditText txtLoginEmail;
        EditText txtLoginPass;

        public event EventHandler<onSignInEventArgs> onSignInComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogSingIn, container, false);


            txtLoginEmail = view.FindViewById<EditText>(Resource.Id.txtLoginEmail);
            txtLoginPass = view.FindViewById<EditText>(Resource.Id.txtLoginmPassword);
            btnSignIn = view.FindViewById<Button>(Resource.Id.btnDialogLogin);

            btnSignIn.Click += btnSignIn_Click;
            return view;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            //Check user details and Login
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3"); //Call Database  
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>(); //Call Table  
                var data1 = data.Where(x => x.email == txtLoginEmail.Text && x.password == txtLoginPass.Text).FirstOrDefault(); //Linq Query  
                if (data1 != null)
                {
                    Toast.MakeText(this.Activity, "Login Success", ToastLength.Short).Show();
                    var intent = new Intent(Activity, typeof(HomeActivity));
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this.Activity, "Username or Password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Short).Show();
            }
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            //set the title bar to invisible
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);

            //set the animation
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}
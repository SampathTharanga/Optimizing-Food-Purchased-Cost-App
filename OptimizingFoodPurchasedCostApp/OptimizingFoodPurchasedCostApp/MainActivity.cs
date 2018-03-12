using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.IO;
using System.Threading;

namespace OptimizingFoodPurchasedCostApp
{
    //MainLauncher = true,
    [Activity(Label = "Optimizing Food Purchased Cost App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mbtnSignUp;
        private Button mbtnSignIn;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Create database method call
            CreateDB();

            mbtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mbtnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            //SignUp Button Code section
            mbtnSignUp.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogSingUp signUpDialog = new DialogSingUp();
                signUpDialog.Show(transaction, "Dialog fragment SignUp");
                signUpDialog.onSignUpComplete += signUpDialog_onSignUpComplete;
            };

            //SignIn Button Code section
            mbtnSignIn.Click += (object sender, EventArgs args) =>
            {
                //pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogSignIn signInDialog = new DialogSignIn();
                signInDialog.Show(transaction, "Dialog fragment SignIn");
                signInDialog.onSignInComplete += signInDialog_onSignInComplete;
            };
        }

        private void signInDialog_onSignInComplete(object sender, onSignInEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
        }

        private void signUpDialog_onSignUpComplete(object sender, OnSingUpEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
        }

        private void ActLikeARequest()
        {
            Thread.Sleep(3000);
            RunOnUiThread(() => { mProgressBar.Visibility = ViewStates.Invisible; });
        }

        //Create database and set connection string
        public string CreateDB()
        {
            var output = "";
            output += "Creating Database if it doesn't exists";
            string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");//Create new database
            var db = new SQLiteConnection(dbpath);
            output += "\nDatabase Created...";
            //Toast.MakeText(this, "WORKING..........", ToastLength.Short).Show();
            return output;
        }
    }
}
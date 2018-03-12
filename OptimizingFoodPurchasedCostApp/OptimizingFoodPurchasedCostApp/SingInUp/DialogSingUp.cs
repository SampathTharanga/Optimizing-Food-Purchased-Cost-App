using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;

namespace OptimizingFoodPurchasedCostApp
{
    public class OnSingUpEventArgs : EventArgs
    {
        private string firstName;
        private string email;
        private string pass;
        private string confirmPass;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return pass; }
            set { pass = value; }
        }

        public string ConfirmPass
        {
            get { return confirmPass; }
            set { confirmPass = value; }
        }

        public OnSingUpEventArgs(string fstName, string mail, string pass, string confirmPass) : base()
        {
            FirstName = fstName;
            Email = mail;
            Password = pass;
            ConfirmPass = confirmPass;

        }
    }

    class DialogSingUp : DialogFragment
    {
        EditText txtFName;
        EditText txtMail;
        EditText txtPass;
        Button btnDiaSignUp;

        public event EventHandler<OnSingUpEventArgs> onSignUpComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DialogSingUp, container, false);

            txtFName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            txtMail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            txtPass = view.FindViewById<EditText>(Resource.Id.txtPassword);
            btnDiaSignUp = view.FindViewById<Button>(Resource.Id.btnDialogSignUp);
            btnDiaSignUp.Click += btnDialogSignUp_Click;


            return view;
        }

        private void btnDialogSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<LoginTable>();
                LoginTable tbl = new LoginTable();
                tbl.firstName = txtFName.Text;
                tbl.email = txtMail.Text;
                tbl.password = txtPass.Text;
                db.Insert(tbl);
                Toast.MakeText(this.Activity, "Record Added Successfully...,", ToastLength.Short).Show();
                this.Dismiss();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Short).Show();
            }
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            //Set the title bar to invisible
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);

            //Set the animation
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}
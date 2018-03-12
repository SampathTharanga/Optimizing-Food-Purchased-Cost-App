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
    public class MemberRegisterFragment : Android.Support.V4.App.Fragment
    {
        EditText txtName;
        RadioGroup radioGroupGend;
        RadioButton radioGendReset;
        RadioGroup radioGroupPreg;
        RadioButton radioPregReset;
        DatePicker datePickerBirthDay;
        EditText txtDiseases;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //// Create your fragment here
            //var intent = new Intent(Activity, typeof(MembersRegisterActivity));
            //StartActivity(intent);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(Resource.Layout.MemberRegisterLayout, container, false);

            txtName = v.FindViewById<EditText>(Resource.Id.txtMemberName);
            datePickerBirthDay = v.FindViewById<DatePicker>(Resource.Id.datePickerBirthday);
            radioGroupGend = v.FindViewById<RadioGroup>(Resource.Id.radioGroupGender);
            radioGendReset= v.FindViewById<RadioButton>(Resource.Id.radioButtonMale);
            radioGroupPreg = v.FindViewById<RadioGroup>(Resource.Id.radioGroupPregnancy);
            radioPregReset = v.FindViewById<RadioButton>(Resource.Id.radioButtonYes);
            txtDiseases = v.FindViewById<EditText>(Resource.Id.txtDiseases);


            Button button = v.FindViewById<Button>(Resource.Id.btnMemberReg);
            button.Click += delegate
            {
                try
                {

                    string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
                    var db = new SQLiteConnection(dpPath);
                   // db.CreateTable<MembersTable>();
                    MembersTable tbl = new MembersTable();
                    tbl.name = txtName.Text;
                    tbl.birthday = datePickerBirthDay.DateTime.ToString();
                    //RadioGroup use check Gender.
                    RadioButton checkedGender = v.FindViewById<RadioButton>(radioGroupGend.CheckedRadioButtonId);
                    if (checkedGender.Text == "Female")
                        tbl.gender = checkedGender.Text;
                    else
                        tbl.gender = checkedGender.Text;
                    //RadioGroup use Check Pregnat or not.
                    RadioButton checkedPreg = v.FindViewById<RadioButton>(radioGroupPreg.CheckedRadioButtonId);
                    if (checkedPreg.Text == "Yes")
                        tbl.pregnancy = checkedPreg.Text;
                    else
                        tbl.pregnancy = checkedPreg.Text;
                    tbl.diseases = txtDiseases.Text;


                    //SET MALE CHECKED THEN PREGNANCY RADIO GROUP ENABLE FALSE CODE SECTION
                    //if (radioGendReset.Checked == true)
                    //    radioGroupPreg.Enabled = false;
                    //else
                    //    radioGroupPreg.Enabled = true;

                    db.Insert(tbl);
                    Toast.MakeText(this.Activity, "Record Added Successfully...,", ToastLength.Short).Show();

                    //Reset all inputs for defalt value
                    txtName.Text = "";
                    radioGendReset.Checked = true;
                    radioPregReset.Checked = true;
                    txtDiseases.Text = "";
                    radioGroupPreg.Enabled = true;
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Short);
                }
            };


            return v;
        }
    }
}
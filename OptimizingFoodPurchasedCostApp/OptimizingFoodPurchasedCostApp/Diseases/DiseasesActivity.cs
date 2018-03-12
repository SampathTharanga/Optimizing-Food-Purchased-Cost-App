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
    [Activity(Label = "DiseasesActivity")]
    public class DiseasesActivity : Activity
    {
        private EditText txtDisName;
        private EditText txtMinDisCarbohydrates;
        private EditText txtMaxDisCarbohydrates;
        private EditText txtMinDisProtein;
        private EditText txtMaxDisProtein;
        private EditText txtMinDisFat;
        private EditText txtMaxDisFat;
        private Button btnDiseasesAdd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TempDiseasesAdd);
            txtDisName = FindViewById<EditText>(Resource.Id.txtDisName);
            txtMinDisCarbohydrates = FindViewById<EditText>(Resource.Id.txtMinDisCarbohydrate);
            txtMaxDisCarbohydrates = FindViewById<EditText>(Resource.Id.txtMaxDisCarbohydrate);
            txtMinDisProtein = FindViewById<EditText>(Resource.Id.txtMinDisProtein);
            txtMaxDisProtein = FindViewById<EditText>(Resource.Id.txtMaxDisProtein);
            txtMinDisFat = FindViewById<EditText>(Resource.Id.txtMinDisFat);
            txtMaxDisFat = FindViewById<EditText>(Resource.Id.txtMaxDisFat);
            btnDiseasesAdd = FindViewById<Button>(Resource.Id.btnDisAdd);

            btnDiseasesAdd.Click += btnDiseasesAdd_Click;
        }

        private void btnDiseasesAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<DiseasesTable>();
                DiseasesTable tbl = new DiseasesTable();
                tbl.diseasesName = txtDisName.Text;
                tbl.main_carbohydrates = float.Parse(txtMinDisCarbohydrates.Text);
                tbl.maax_carbohydrates = float.Parse(txtMaxDisCarbohydrates.Text);
                tbl.min_protein = float.Parse(txtMinDisProtein.Text);
                tbl.max_protein = float.Parse(txtMaxDisProtein.Text);
                tbl.min_fat = float.Parse(txtMinDisFat.Text);
                tbl.max_fat = float.Parse(txtMaxDisFat.Text);
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();

                //Clear all
                txtDisName.Text = "";
                txtMinDisCarbohydrates.Text = "";
                txtMaxDisCarbohydrates.Text = "";
                txtMinDisProtein.Text = "";
                txtMaxDisProtein.Text = "";
                txtMinDisFat.Text = "";
                txtMaxDisFat.Text = "";
    }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}
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
    //, MainLauncher = true
    [Activity(Label = "StandardHumanNutritionActivity")]
    public class StandardHumanNutritionActivity : Activity
    {
        EditText txtGender;
        EditText txtAgeLow;
        EditText txtAgeUpp;
        private EditText txtMaxTotCalary;
        private EditText txtMinTotCalary;
        EditText txtMinCarbohydrates;
        EditText txtMaxCarbohydrates;
        EditText txtMinProtein;
        EditText txtMaxProtein;
        EditText txtMinFat;
        EditText txtMaxFat;
        Button btnAdd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TempHumanStandardNutrition);

            txtGender = FindViewById<EditText>(Resource.Id.txtGender);
            txtAgeLow = FindViewById<EditText>(Resource.Id.txtAgeLower);
            txtAgeUpp = FindViewById<EditText>(Resource.Id.txtAgeUpper);
            txtMinTotCalary = FindViewById<EditText>(Resource.Id.txtMinTotalCalary);
            txtMaxTotCalary = FindViewById<EditText>(Resource.Id.txtMaxTotalCalary);
            txtMinCarbohydrates = FindViewById<EditText>(Resource.Id.txtMinCarbohydrate);
            txtMaxCarbohydrates = FindViewById<EditText>(Resource.Id.txtMaxCarbohydrate);
            txtMinProtein = FindViewById<EditText>(Resource.Id.txtMinProtein);
            txtMaxProtein = FindViewById<EditText>(Resource.Id.txtMaxProtein);
            txtMinFat = FindViewById<EditText>(Resource.Id.txtMinFat);
            txtMaxFat = FindViewById<EditText>(Resource.Id.txtMaxFat);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);

            btnAdd.Click += btnAdd_Click;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
                var db = new SQLiteConnection(dpPath);
                db.CreateTable<SNLevelTable>();
                SNLevelTable tbl = new SNLevelTable();
                tbl.gender = txtGender.Text;
                tbl.age_lower = int.Parse(txtAgeLow.Text);
                tbl.age_upper = int.Parse(txtAgeUpp.Text);
                tbl.min_totalCalary = float.Parse(txtMinTotCalary.Text);
                tbl.max_totalCalary = float.Parse(txtMaxTotCalary.Text);
                tbl.min_carbohydrates = float.Parse(txtMinCarbohydrates.Text);
                tbl.max_carbohydrates = float.Parse(txtMaxCarbohydrates.Text);
                tbl.min_protein = float.Parse(txtMinProtein.Text);
                tbl.max_protein = float.Parse(txtMaxProtein.Text);
                tbl.min_fat = float.Parse(txtMinFat.Text);
                tbl.max_fat = float.Parse(txtMaxFat.Text);
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}
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
using Java.IO;

namespace OptimizingFoodPurchasedCostApp
{
    [Activity(Label = "FoodsActivity")]
    public class FoodsActivity : Activity
    {
        EditText txtFdName;
        EditText txtType;
        EditText txtFdPrice;
        EditText txtFdCalary;
        EditText txtfdCarbohydrate;
        EditText txtfdProtein;
        EditText txtfdFat;
        Button btnfdAdd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.TempFoodsRegPage);


            txtFdName = FindViewById<EditText>(Resource.Id.txtFoodName);
            txtType = FindViewById<EditText>(Resource.Id.txtFoodType);
            txtFdPrice = FindViewById<EditText>(Resource.Id.txtFoodPrice);
            txtFdCalary = FindViewById<EditText>(Resource.Id.txtCalary);
            txtfdCarbohydrate = FindViewById<EditText>(Resource.Id.txtCarbohydrate);
            txtfdProtein = FindViewById<EditText>(Resource.Id.txtProtein);
            txtfdFat = FindViewById<EditText>(Resource.Id.txtFat);
            btnfdAdd = FindViewById<Button>(Resource.Id.btnAdd);

            //string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
            //var db = new SQLiteConnection(dpPath);

            ////Vegitable
            //db.Execute("delete from FoodsTable");

            btnfdAdd.Click += btnfdAdd_Click;
        }

        private void btnfdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
                var db = new SQLiteConnection(dpPath);

                //Foods
                db.CreateTable<FoodsTable>();
                FoodsTable tbl = new FoodsTable();

                tbl.name = txtFdName.Text;
                tbl.type = txtType.Text;
                tbl.price = float.Parse(txtFdPrice.Text);
                tbl.calary = float.Parse(txtFdCalary.Text);
                tbl.carbohydrates = float.Parse(txtfdCarbohydrate.Text);
                tbl.protein = float.Parse(txtfdProtein.Text);
                tbl.fat = float.Parse(txtfdFat.Text);
                db.Insert(tbl);
                Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();

                txtFdName.Text = "";
                txtType.Text = "";
                txtFdPrice.Text = "";
                txtFdPrice.Text = "";
                txtfdCarbohydrate.Text = "";
                txtfdProtein.Text = "";
                txtfdFat.Text = "";
                txtFdCalary.Text = "";

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

    }
}
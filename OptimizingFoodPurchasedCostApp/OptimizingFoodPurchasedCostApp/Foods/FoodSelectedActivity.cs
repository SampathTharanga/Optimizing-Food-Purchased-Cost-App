using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Android.App;
using Android.Content;
using Android.OS;
using SQLite;
using System.IO;

namespace OptimizingFoodPurchasedCostApp
{
    //, MainLauncher = true
    [Activity(Label = "Foods List")]
    public class FoodSelectedActivity : Activity
    {
        public static Context context;
        public static List<FoodsTable> FoodInfoList = new List<FoodsTable>();

        public static ListView ListView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SelectFoodsLayout);
            ListView = FindViewById<ListView>(Resource.Id.listViewFoods);

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
            var db = new SQLiteConnection(dpPath);
            db.DropTable<SelectFoodsTable>();//Delete exist table
            db.CreateTable<SelectFoodsTable>();

            //How to edit text in a EditText component in a custom view within a ListView
            ListView.DescendantFocusability = DescendantFocusability.AfterDescendants;

            //Clear the Selected food list
            FoodInfoList.Clear();
            

            GetList list = new GetList();
            list.Execute();
        }


        public class GetList : AsyncTask
        {
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
                var db = new SQLiteConnection(dbpath);
                var foodData = db.Table<FoodsTable>();//Call Table
                var from = new List<string>();

                foreach (var data in foodData)
                {
                    FoodInfoList.Add(data);
                }
                return true;
            }
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);
                ListView.Adapter = new FoodInfoListAdapter(context, FoodInfoList);
            }

        }


        class FoodInfoListAdapter : BaseAdapter<FoodsTable>
        {
            private List<FoodsTable> mItem = new List<FoodsTable>();
            private Context context;
            public FoodInfoListAdapter(Context mcontext, List<FoodsTable> mItems)
            {
                mItem.Clear();
                mItem = mItems;
                context = mcontext;
                this.NotifyDataSetChanged();

            }
            public override FoodsTable this[int position]
            {
                get
                {
                    return mItem[position];
                }
            }

            public override int Count
            {
                get
                {
                    return mItem.Count;
                }
            }

            public Context MContext { get; private set; }

            public override long GetItemId(int position)
            {
                return position;
            }


            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                View listitem = convertView;
                listitem = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.FoodListViewDesign, parent, false);
                TextView TxtName = listitem.FindViewById<TextView>(Resource.Id.nameTextView);
                TextView Txtcarbohydrate = listitem.FindViewById<TextView>(Resource.Id.CarbohydtateTextView);
                TextView TxtProtein = listitem.FindViewById<TextView>(Resource.Id.ProteinTextView);
                TextView TxtFat = listitem.FindViewById<TextView>(Resource.Id.fatTextView);
                EditText EdtWeight = listitem.FindViewById<EditText>(Resource.Id.edittextWeight);
                Button BtnAdd = listitem.FindViewById<Button>(Resource.Id.btnAdd);

                TxtName.Text = mItem[position].name;
                Txtcarbohydrate.Text = (mItem[position].carbohydrates).ToString();
                TxtProtein.Text = (mItem[position].protein).ToString();
                TxtFat.Text = (mItem[position].fat).ToString();

                //listitem.Click += (object sender, EventArgs e) =>
                //{
                //    Toast.MakeText(parent.Context, "Clicked " + mItem[position].name, ToastLength.Long).Show();
                //};



                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
                var db = new SQLiteConnection(dpPath);


                BtnAdd.Click += (object sender, EventArgs e) =>
                {

                    /*
                                        Selected foods store the database table
                                   -------------------------------------------------
                    */
                    SelectFoodsTable tbl = new SelectFoodsTable();
                    tbl.name = mItem[position].name;
                    tbl.type = mItem[position].type;
                    tbl.price = float.Parse(mItem[position].price.ToString());
                    tbl.calary = float.Parse(mItem[position].calary.ToString());
                    tbl.carbohydrates = float.Parse(mItem[position].carbohydrates.ToString());
                    tbl.protein = float.Parse(mItem[position].protein.ToString());
                    tbl.fat = float.Parse(mItem[position].fat.ToString());
                    tbl.amount = int.Parse(EdtWeight.Text);
                    db.Insert(tbl);
                    Toast.MakeText(parent.Context, "Record Added Successfully...,", ToastLength.Short).Show();
                    
                    //Select item remove in the foodlist.
                    mItem.RemoveAt(position);
                    NotifyDataSetChanged();
                };

                return listitem;
            }
        }

    }

}
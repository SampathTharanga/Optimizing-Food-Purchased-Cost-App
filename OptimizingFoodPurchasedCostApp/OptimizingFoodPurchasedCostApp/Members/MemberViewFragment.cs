using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using Android.Webkit;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace OptimizingFoodPurchasedCostApp
{
    public class MemberViewFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            //var intent = new Intent(Activity, typeof(MembersActivity));
            //StartActivity(intent);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var v = inflater.Inflate(Resource.Layout.MemberViewPage, container, false);


            string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");//Create new database
            var db = new SQLiteConnection(dbpath);
            //db.CreateTable<MembersTable>();
            var data = db.Table<MembersTable>();//Call Table
            var from = new List<string>();
            foreach (var listing in data)
            {
                from.Add(listing.name + "   -   " + listing.gender + "   -   " + listing.diseases + "   -   " + listing.birthday);
            }
            ListView listtable = (ListView)v.FindViewById(Resource.Id.listViewMember);
            listtable.Adapter = new ArrayAdapter(this.Activity, Android.Resource.Layout.SimpleListItem1, from.ToArray());

            //int ac = db.Table<MembersTable>().Count();

            //Toast.MakeText(this.Activity, ac.ToString(), ToastLength.Short).Show();
            return v;
        }
    }
}
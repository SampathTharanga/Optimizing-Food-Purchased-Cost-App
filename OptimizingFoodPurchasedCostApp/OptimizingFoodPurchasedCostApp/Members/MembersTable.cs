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
using SQLite;

namespace OptimizingFoodPurchasedCostApp
{
    public class MembersTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; }
        [MaxLength(25)]
        public string name { get; set; }
        [MaxLength(10)]
        public string birthday { get; set; }
        [MaxLength(10)]
        public string gender { get; set; }
        [MaxLength(10)]
        public string pregnancy { get; set; }
        [MaxLength(50)]
        public string diseases { get; set; } 
    }
}
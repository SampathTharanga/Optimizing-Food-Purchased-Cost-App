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
    public class SNLevelTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; }
        [MaxLength(25)]
        public string gender { get; set; }
        [MaxLength(25)]
        public int age_lower { get; set; }
        public int age_upper { get; set; }
        [MaxLength(10)]
        public float min_totalCalary { get; set; }
        [MaxLength(10)]
        public float max_totalCalary { get; set; }
        [MaxLength(10)]
        public float min_carbohydrates { get; set; }
        [MaxLength(10)]
        public float max_carbohydrates { get; set; }
        [MaxLength(10)]
        public float min_protein { get; set; }
        [MaxLength(10)]
        public float max_protein { get; set; }
        [MaxLength(10)]
        public float min_fat { get; set; }
        [MaxLength(10)]
        public float max_fat { get; set; }
    }
}
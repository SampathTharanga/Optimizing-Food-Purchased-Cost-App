using SQLite;

namespace OptimizingFoodPurchasedCostApp
{
    class SelectFoodsTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(25)]
        public string type { get; set; }
        public float price { get; set; }
        public float calary { get; set; }
        public float carbohydrates { get; set; }
        public float protein { get; set; }
        public float fat { get; set; }
        public int amount { get; set; }
    }
}
using SQLite;

namespace OptimizingFoodPurchasedCostApp
{
    public class FoodsTable
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; }
        [MaxLength(25)]
        public string name { get; set; }
        [MaxLength(25)]
        public string type { get; set; }
        [MaxLength(10)]
        public float price { get; set; }
        [MaxLength(10)]
        public float calary { get; set; }
        [MaxLength(10)]
        public float carbohydrates { get; set; }
        [MaxLength(10)]
        public float protein { get; set; }
        [MaxLength(10)]
        public float fat { get; set; }
    }
}
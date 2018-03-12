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
using System.Drawing;

namespace OptimizingFoodPurchasedCostApp
{
    [Activity(Label = "Optimized Food List")]
    public class ModelActivity : Activity
    {
        int noOfMembers;
        float stdMinCarbohydrate, stdMaxCarbohydrate, stdMinProtein, stdMaxProtein, stdMinFat, stdMaxFat;
        float dises_stdMinCarbohydrate, dises_stdMaxCarbohydrate, dises_stdMinProtein, dises_stdMaxProtein, dises_stdMinFat, dises_stdMaxFat;
        DateTime Dob;
        string Gender, Diseases;
        float stdTotMinCalary, stdTotMaxCalary;
        float totMinCarbohydrate, totMaxCarbohydrate, totMinProtein, totMaxProtein, totMinFat, totMaxFat;
        TextView totalPrice, tvTotalCarbo, tvTotalProtein, tvTotalFat, tvExtraCarbo, tvExtraProtein, tvExtraFat;
        TextView TVCarbo, TVProtein, TVFat, TextView04;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PlanWithModelLayout);

            totalPrice = FindViewById<TextView>(Resource.Id.textViewTotalPrice);
            tvTotalCarbo = FindViewById<TextView>(Resource.Id.TextViewCarbo);
            tvTotalProtein = FindViewById<TextView>(Resource.Id.TextViewProtein);
            tvTotalFat = FindViewById<TextView>(Resource.Id.TextViewFat);
            tvExtraCarbo = FindViewById<TextView>(Resource.Id.TextViewExtraCarbo);
            tvExtraProtein = FindViewById<TextView>(Resource.Id.TextViewExtraProtein);
            tvExtraFat = FindViewById<TextView>(Resource.Id.TextViewExtraFat);

            TVCarbo = FindViewById<TextView>(Resource.Id.TextViewCarbo);
            TVProtein = FindViewById<TextView>(Resource.Id.TextViewProtein);
            TVFat = FindViewById<TextView>(Resource.Id.TextViewFat);
            TextView04 = FindViewById<TextView>(Resource.Id.TextView04);
            try
            {
                /*
                                            VARIABLES SET DEFAULT FOR VALUE
                                      ------------------------------------------
                */
                stdMinCarbohydrate = 0.0f; stdMaxCarbohydrate = 0.0f; stdMinProtein = 0.0f; stdMaxProtein = 0.0f; stdMinFat = 0.0f; stdMaxFat = 0.0f;
                noOfMembers = 0;
                dises_stdMinCarbohydrate = 0.0f; dises_stdMaxCarbohydrate = 0.0f; dises_stdMinProtein = 0.0f; dises_stdMaxProtein = 0.0f; dises_stdMinFat = 0.0f; dises_stdMaxFat = 0.0f;
                totMinCarbohydrate = 0.0f; totMaxCarbohydrate = 0.0f; totMinProtein = 0.0f; totMaxProtein = 0.0f; totMinFat = 0.0f; totMaxFat = 0.0f;


                /*
                                        SET CONNECTION WITH SQLITE DATABASE
                                   -----------------------------------------------
                */
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "appdb.db3");
                var db = new SQLiteConnection(dpPath);
                //db.DropTable<SNLevelTable>();//Delete exist table

                /*
                                                CALL DATABASE TABLEAS
                                           -------------------------------
                
                */
                var tblStdNutritions = db.Table<SNLevelTable>();
               var tblMembers = db.Table<MembersTable>();
               var tblDiseases = db.Table<DiseasesTable>();
               var tblFoods = db.Table<FoodsTable>(); 
               var tblSelectFoods = db.Table<SelectFoodsTable>();

                /*
                                                NUMBER OF MEMBERS
                                          ------------------------------
                */
                noOfMembers = db.Table<MembersTable>().Count();


                /*
                                    CALCULATE TOTAL STANDARD NUTRITION MAX & MIN  
                              ----------------------------------------------------------
                */
                for (int row = 1; row <= noOfMembers; row++)//<--- START THE MEMBER LOOP
               {
                   /*
                                CALCULATE AGE USING THE MEMBERS BIRTHDAY | GENDER CHECK
                           -------------------------------------------------------------------
                   */
                   var dateOfBirth = (from values in tblMembers
                                      where values.id == row
                                      select new MembersTable
                                      {
                                          birthday = values.birthday,
                                          gender = values.gender,
                                          diseases = values.diseases
                                      }).ToList<MembersTable>();
                   foreach (var val in dateOfBirth)
                   {
                       Dob = Convert.ToDateTime(val.birthday.ToString());
                       Gender = val.gender.ToString();
                       Diseases = val.diseases.ToString();
                   }

                   /*
                                                AGE CALCULATE
                                           ----------------------
                   */
                   DateTime Now = DateTime.Now;
                   int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
                   DateTime PastYearDate = Dob.AddYears(Years);
                   int Months = 0;
                   for (int i = 1; i <= 12; i++)
                   {
                       if (PastYearDate.AddMonths(i) == Now)
                       {
                           Months = i;
                           break;
                       }
                       else if (PastYearDate.AddMonths(i) >= Now)
                       {
                           Months = i - 1;
                           break;
                       }
                   }
                   
                   if (Months > 6) Years ++;//IF (Months > 6) Years += Years;



                    /*
                                         COMPLETE TOTAL STANDARD NUTRITION CALCULATE
                                    ------------------------------------------------------
                    */
                    if (Years == 0 && Months > 0) // less than 1 Year(0.5 Month to 1 Year)
                   {
                       stdMinCarbohydrate = 365;
                       totMinCarbohydrate += stdMinCarbohydrate;
                       stdMaxCarbohydrate = 465;
                       totMaxCarbohydrate += stdMaxCarbohydrate;

                       stdMinProtein = 100;
                       totMinProtein += stdMinProtein;
                       stdMaxProtein = 127;
                       totMaxProtein += stdMaxProtein;

                       stdMinFat = 200;
                       totMinFat += stdMinFat;
                       stdMaxFat = 225;
                       totMaxFat += stdMaxFat;
                   }
                   else if (Years > 0)// greter than1 Year 
                   {

                       /*
                                                TOTAL STANDARD CALARY FOR DISEASES
                                           ---------------------------------------------
                       */
                       var dataTotCalary = (from values in tblStdNutritions
                                      where values.age_lower <= Years && values.age_upper >= Years && values.gender == Gender
                                      select new SNLevelTable
                                      {
                                          min_totalCalary = values.min_totalCalary,
                                          max_totalCalary = values.max_totalCalary,

                                      }).ToList<SNLevelTable>();
                       if (dataTotCalary != null)
                       {

                           foreach (var val in dataTotCalary)
                           {
                               stdTotMinCalary = float.Parse(val.min_totalCalary.ToString());
                               stdTotMaxCalary = float.Parse(val.max_totalCalary.ToString());
                           }
                       }


                       /*
                                                    MEMBER HAS NOT DISEASES
                                               --------------------------------
                       */
                       if (Diseases == null || Diseases == "")
                       {
                           /*
                                        STANDARD HUMAN NUTRITION LEVEL USE CALCULATE NUTRITIONS
                                   --------------------------------------------------------------------
                           */
                           var dataAge = (from values in tblStdNutritions
                                          where values.age_lower <= Years && values.age_upper >= Years && values.gender == Gender
                                          select new SNLevelTable
                                          {
                                              min_carbohydrates = values.min_carbohydrates,
                                              max_carbohydrates = values.max_carbohydrates,
                                              min_protein = values.min_protein,
                                              max_protein = values.max_protein,
                                              min_fat = values.min_fat,
                                              max_fat = values.max_fat

                                          }).ToList<SNLevelTable>();


                           if (dataAge != null)
                           {

                               foreach (var val in dataAge)
                               {
                                   stdTotMinCalary = float.Parse(val.min_totalCalary.ToString());
                                   stdTotMaxCalary = float.Parse(val.max_totalCalary.ToString());

                                   stdMinCarbohydrate = float.Parse(val.min_carbohydrates.ToString());
                                   totMinCarbohydrate += stdMinCarbohydrate;

                                   stdMaxCarbohydrate = float.Parse(val.max_carbohydrates.ToString());
                                   totMaxCarbohydrate += stdMaxCarbohydrate;

                                   stdMinProtein = float.Parse(val.min_protein.ToString());
                                    totMinProtein += stdMinProtein;

                                   stdMaxProtein = float.Parse(val.max_protein.ToString());
                                   totMaxProtein += stdMaxProtein;

                                   stdMinFat = float.Parse(val.min_fat.ToString());
                                   totMinFat += stdMinFat;

                                   stdMaxFat = float.Parse(val.max_fat.ToString());
                                   totMaxFat += stdMaxFat;
                               }
                           }
                       }
                        /*
                                                      MEMBER HAS NOT DISEASES
                                                 --------------------------------
                        */
                        else
                        {
                            /*
                                            STANDARD HUMAN NUTRITION LEVEL AND DISESAS USE CALCULATE NUTRITIONS
                                    --------------------------------------------------------------------
                            */
                            var disesasData = (from values in tblDiseases
                                              where values.diseasesName == Diseases
                                              select new DiseasesTable
                                              {
                                                  main_carbohydrates = values.main_carbohydrates,
                                                  maax_carbohydrates = values.maax_carbohydrates,
                                                  min_protein = values.min_protein,
                                                  max_protein = values.max_protein,
                                                  min_fat = values.min_fat,
                                                  max_fat = values.max_fat
                                              }).ToList<DiseasesTable>();



                           foreach (var val in disesasData)
                           {
                               dises_stdMinCarbohydrate = float.Parse(val.main_carbohydrates.ToString());//Disease MinCarbohydrate Calary % here
                               totMinCarbohydrate += ((stdTotMinCalary * dises_stdMinCarbohydrate) / 4);// g

                               dises_stdMaxCarbohydrate = float.Parse(val.maax_carbohydrates.ToString());//Disease MaxCarbohydrate Calary % here
                               totMaxCarbohydrate += ((stdTotMaxCalary * dises_stdMaxCarbohydrate) / 4);// g

                               dises_stdMinProtein = float.Parse(val.min_protein.ToString());
                               totMinProtein += ((stdTotMinCalary * dises_stdMinProtein) / 4);

                               dises_stdMaxProtein = float.Parse(val.max_protein.ToString());
                               totMaxProtein += ((stdTotMaxCalary * dises_stdMaxProtein) / 4);

                               dises_stdMinFat = float.Parse(val.min_fat.ToString());
                               totMinFat += ((stdTotMinCalary * dises_stdMinFat) / 9);

                               dises_stdMaxFat = float.Parse(val.max_fat.ToString());
                               totMaxFat += ((stdTotMaxCalary * dises_stdMaxFat) / 9);
                           }
                       }

                   }
                   else
                   {
                       Toast.MakeText(this, "Not here valide age!", ToastLength.Short).Show();
                   }
               }//<--- END THE MEMBERS LOOP

                /*
                                                STANDART NUTRITIONS CALCULATE PER WEEK
                                           ------------------------------------------------
                */

                totMinCarbohydrate = totMinCarbohydrate * 7;
                totMaxCarbohydrate = totMaxCarbohydrate * 7;
                totMinProtein = totMinProtein * 7;
                totMaxProtein = totMaxProtein * 7;
                totMinFat = totMinFat * 7;
                totMaxFat = totMaxFat * 7;
                /*
                                                    # OF SELECTED FOOD ITEMS NUTRITIONS CALCULATE
                                               -------------------------------------------------------
                */
                int tblSize = db.Table<SelectFoodsTable>().Count();//How many foods are selected

                //Variable reset
                float SlectedFoodPrice = 0.0f, SlectedFoodCarbo = 0.0f, SlectedFoodProte = 0.0f, SlectedFoodFat = 0.0f;

                /*
                                            How many total Nutritions calculate selected foods of amount
                                        --------------------------------------------------------------------
                */
                for (int x = 0; x <= tblSize; x++)//One by one food item check
                {
                    //foods nutritions
                    var selectFoodData = (from values in tblSelectFoods
                                          where values.id == x
                                       select new SelectFoodsTable
                                       {
                                           price = values.price,
                                           carbohydrates = values.carbohydrates,
                                           protein = values.protein,
                                           fat = values.fat,
                                           amount=values.amount
                                       }).ToList<SelectFoodsTable>();

                    if (selectFoodData != null)
                    {
                        foreach (var valf in selectFoodData)
                        {
                            SlectedFoodPrice += (float.Parse(valf.price.ToString()) / 100) * valf.amount;
                            SlectedFoodCarbo += (float.Parse(valf.carbohydrates.ToString()) / 100) * valf.amount;
                            SlectedFoodProte += (float.Parse(valf.protein.ToString()) / 100) * valf.amount;
                            SlectedFoodFat += (float.Parse(valf.fat.ToString()) / 100) * valf.amount;
                        }
                    }
                }

                ////SELECTED FOOD NUTRITIONS CALCULATE PER WEEK
                //SlectedFoodCarbo = SlectedFoodCarbo ;
                //SlectedFoodProte = SlectedFoodProte ;
                //SlectedFoodFat = SlectedFoodFat ;


                //Display the Nutritions Level and Price
                tvTotalCarbo.Text = SlectedFoodCarbo.ToString();
                tvTotalProtein.Text = SlectedFoodProte.ToString();
                tvTotalFat.Text = SlectedFoodFat.ToString();
                totalPrice.Text = SlectedFoodPrice.ToString() + "/=";


                //Check the Nutrition satisfy or not
                //Carbohydrate
                if (totMinCarbohydrate >= SlectedFoodCarbo)
                {
                    tvTotalCarbo.SetTextColor(Android.Graphics.Color.Green);
                    //tvExtraCarbo.SetTextColor(Android.Graphics.Color.Green);
                    tvExtraCarbo.Text = "+" + (totMinCarbohydrate - SlectedFoodCarbo).ToString();
                }
                if (totMaxCarbohydrate <= SlectedFoodCarbo)
                {
                    tvTotalCarbo.SetTextColor(Android.Graphics.Color.Red);
                    //tvExtraCarbo.SetTextColor(Android.Graphics.Color.Red);
                    tvExtraCarbo.Text = "-" + (SlectedFoodCarbo - totMaxCarbohydrate).ToString();
                }
               // if((totMinCarbohydrate <= SlectedFoodCarbo) && (totMaxCarbohydrate >= SlectedFoodCarbo))


                //Protien
                if (totMinProtein >= SlectedFoodProte)
                {
                    tvTotalProtein.SetTextColor(Android.Graphics.Color.Green);
                   // tvExtraProtein.SetTextColor(Android.Graphics.Color.Green);
                    tvExtraProtein.Text= "+" + (totMinProtein - SlectedFoodProte).ToString();
                }
                if (totMaxProtein <= SlectedFoodProte)
                {
                    tvTotalProtein.SetTextColor(Android.Graphics.Color.Red);
                   // tvExtraProtein.SetTextColor(Android.Graphics.Color.Red);
                    tvExtraProtein.Text = "-" + (SlectedFoodProte - totMaxProtein).ToString();
                }
                //if((totMinProtein <= SlectedFoodProte) && (totMaxProtein >= SlectedFoodProte))


                //Fat
                if (totMinFat >= SlectedFoodFat)
                {
                    tvTotalFat.SetTextColor(Android.Graphics.Color.Green);
                    //tvExtraFat.SetTextColor(Android.Graphics.Color.Green);
                    tvExtraFat.Text = "+" + (totMinFat - SlectedFoodFat).ToString();
                }
                if (totMaxFat <= SlectedFoodFat)
                {
                    tvTotalFat.SetTextColor(Android.Graphics.Color.Red);
                   // tvExtraFat.SetTextColor(Android.Graphics.Color.Red);
                    tvExtraFat.Text = "-" + (SlectedFoodFat - totMaxFat).ToString();
                }
                //if((totMinFat <= SlectedFoodFat) && (totMaxFat >= SlectedFoodFat))

                if (TVCarbo.Text == "Normal" && TVProtein.Text == "Normal" && TVFat.Text == "Normal")
                {
                    totalPrice.SetTextColor(Android.Graphics.Color.Brown);
                    TextView04.SetTextColor(Android.Graphics.Color.Brown);
                }

                //==============OUTPUT SECTION==============
                var dataFood = db.Table<SelectFoodsTable>();//Call Table
                var fromList = new List<string>();
                foreach (var listing in dataFood)
                {
                    fromList.Add(listing.name + "    " + listing.amount + "g    Rs" + (listing.price / 100) * listing.amount + "/=");
                }
                ListView listtable = FindViewById<ListView>(Resource.Id.listViewSelectFood);
                listtable.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, fromList.ToArray());
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
    }
}
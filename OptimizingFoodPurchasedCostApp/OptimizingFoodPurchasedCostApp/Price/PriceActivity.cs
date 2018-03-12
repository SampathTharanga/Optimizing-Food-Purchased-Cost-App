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
using Android.Webkit;

namespace OptimizingFoodPurchasedCostApp
{
    [Activity(Label = "PriceActivity")]
    public class PriceActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PricePage);

            WebView WebViewPrices = FindViewById<WebView>(Resource.Id.PricewebView);
            WebViewPrices.SetWebViewClient(new WebViewClient());
            //WebViewPrices.LoadUrl("http://www.harti.gov.lk/index.php/en/market-information/data-food-commodities-bulletin");
            WebViewPrices.LoadUrl("https://www.google.lk/");
        }
    }
}
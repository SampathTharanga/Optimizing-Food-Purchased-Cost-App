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
using Android.Graphics;
using Android.Views.Animations;


namespace OptimizingFoodPurchasedCostApp
{
    [Activity(Label = "HelpActivity")]
    public class HelpActivity : Activity
    {
        GestureDetector gestureDetector;
        GestureListener gestureListener;

        ListView menuListView;
        MenuListAdapterClass1 objAdapterMenu;
        ImageView menuIconImageView;
        int intDisplayWidth;
        bool isSingleTapFired = false;
        TextView txtActionBarText;
        //TextView txtPageName;
        // TextView txtDescription;
        //ImageView btnDescExpander;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.HelpLayout);

            FnInitialization();
            TapEvent();
            FnBindMenu(); //find definition in below steps   

            Button btnDiseses = FindViewById<Button>(Resource.Id.btnDiseases);
            Button btnFoodRegistor = FindViewById<Button>(Resource.Id.btnFoodRegister);
            Button btnNutrition = FindViewById<Button>(Resource.Id.btnHumanStandardNutrition);

            btnDiseses.Click += btnDiseses_Click;
            btnFoodRegistor.Click += btnFoodRegistor_Click;
            btnNutrition.Click += btnNutrition_Click;
        }
        private void btnNutrition_Click(object sender, EventArgs e)
        {
            this.StartActivity(typeof(StandardHumanNutritionActivity));
        }

        private void btnFoodRegistor_Click(object sender, EventArgs e)
        {
            this.StartActivity(typeof(FoodsActivity));
        }

        private void btnDiseses_Click(object sender, EventArgs e)
        {
            this.StartActivity(typeof(DiseasesActivity));
        }
        void TapEvent()
        {
            //title bar menu icon
            menuIconImageView.Click += delegate (object sender, EventArgs e)
            {
                if (!isSingleTapFired)
                {
                    FnToggleMenu();  //find definition in below steps
                    isSingleTapFired = false;
                }
            };
            //////bottom expandable description window
            ////btnDescExpander.Click += delegate (object sender, EventArgs e)
            ////{
            ////    FnDescriptionWindowToggle();
            ////};
        }
        void FnInitialization()
        {
            //gesture initialization
            gestureListener = new GestureListener();
            gestureListener.LeftEvent += GestureLeft; //find definition in below steps
            gestureListener.RightEvent += GestureRight;
            gestureListener.SingleTapEvent += SingleTap;
            gestureDetector = new GestureDetector(this, gestureListener);

            menuListView = FindViewById<ListView>(Resource.Id.menuListView);
            menuIconImageView = FindViewById<ImageView>(Resource.Id.menuIconImgView);
            txtActionBarText = FindViewById<TextView>(Resource.Id.txtActionBarText);
            //txtPageName = FindViewById<TextView>(Resource.Id.txtPage);
            //txtDescription = FindViewById<TextView>(Resource.Id.txtDescription);
            // btnDescExpander = FindViewById<ImageView>(Resource.Id.btnImgExpander);

            //changed sliding menu width to 3/4 of screen width 
            Display display = this.WindowManager.DefaultDisplay;
            var point = new Point();
            display.GetSize(point);
            intDisplayWidth = point.X;
            intDisplayWidth = intDisplayWidth - (intDisplayWidth / 3);
            using (var layoutParams = (RelativeLayout.LayoutParams)menuListView.LayoutParameters)
            {
                layoutParams.Width = intDisplayWidth;
                layoutParams.Height = ViewGroup.LayoutParams.MatchParent;
                menuListView.LayoutParameters = layoutParams;
            }
        }
        void FnBindMenu()
        {
            string[] strMnuText = { "Home", "Plan", "Prices", "Foods", "Diseases", "Profile", "Members", "Help", "ContactUs" };
            int[] strMnuUrl = { Resource.Drawable.icon_home, Resource.Drawable.icon_aboutus, Resource.Drawable.icon_product, Resource.Drawable.icon_event, Resource.Drawable.icon_service, Resource.Drawable.icon_client, Resource.Drawable.icon_solution, Resource.Drawable.icon_help, Resource.Drawable.icon_contactus };
            if (objAdapterMenu != null)
            {
                objAdapterMenu.actionMenuSelected -= FnMenuSelected;
                objAdapterMenu = null;
            }
            objAdapterMenu = new MenuListAdapterClass1(this, strMnuText, strMnuUrl);
            objAdapterMenu.actionMenuSelected += FnMenuSelected;
            menuListView.Adapter = objAdapterMenu;
        }
        void FnMenuSelected(string strMenuText)
        {
            txtActionBarText.Text = strMenuText;

            //Menu item clicked goes code here
            if (strMenuText == "Home") { this.StartActivity(typeof(HomeActivity)); }                //HOME
            else if (strMenuText == "Plan") { this.StartActivity(typeof(ModelActivity)); }           //PLAN
            else if (strMenuText == "Prices") { this.StartActivity(typeof(PriceActivity)); }        //PRICES
            else if (strMenuText == "Foods") { this.StartActivity(typeof(FoodSelectedActivity)); }         //FOODS
            else if (strMenuText == "Diseases") { /*this.StartActivity(typeof(DiseasesActivity));*/ }   //DISEASES
            else if (strMenuText == "Profile") { /*this.StartActivity(typeof(FoodsActivity)); */}     //PROFILE
            else if (strMenuText == "Members") { this.StartActivity(typeof(MembersTabMainActivity)); }     //MEMBERS
            else if (strMenuText == "Help") { this.StartActivity(typeof(HelpActivity)); }           //HELP
            else { }
        }
        #region " Menu related"
        void FnToggleMenu()
        {
            Console.WriteLine(menuListView.IsShown);
            if (menuListView.IsShown)
            {
                menuListView.Animation = new TranslateAnimation(0f, -menuListView.MeasuredWidth, 0f, 0f);
                menuListView.Animation.Duration = 300;
                menuListView.Visibility = ViewStates.Gone;
            }
            else
            {
                menuListView.Visibility = ViewStates.Visible;
                menuListView.RequestFocus();
                menuListView.Animation = new TranslateAnimation(-menuListView.MeasuredWidth, 0f, 0f, 0f);//starting edge of layout 
                menuListView.Animation.Duration = 300;
            }
        }
        #endregion

        #region "Gesture function "
        void GestureLeft()
        {
            if (!menuListView.IsShown)
                FnToggleMenu();
            isSingleTapFired = false;
        }
        void GestureRight()
        {
            if (menuListView.IsShown)
                FnToggleMenu();
            isSingleTapFired = false;
        }
        void SingleTap()
        {
            if (menuListView.IsShown)
            {
                FnToggleMenu();
                isSingleTapFired = true;
            }
            else
            {
                isSingleTapFired = false;
            }
        }
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            gestureDetector.OnTouchEvent(ev);
            return base.DispatchTouchEvent(ev);
        }
        #endregion

        #region "Description toggle window"
        //void FnDescriptionWindowToggle()
        //{
        //    if (txtDescription.IsShown)
        //    {
        //        txtDescription.Visibility = ViewStates.Gone;
        //        txtDescription.Animation = new TranslateAnimation(0f, 0f, 0f, txtDescription.MeasuredHeight);
        //        txtDescription.Animation.Duration = 300;
        //        btnDescExpander.SetImageResource(Resource.Drawable.up_arrow);
        //    }
        //    else
        //    {
        //        txtDescription.Visibility = ViewStates.Visible;
        //        txtDescription.RequestFocus();
        //        txtDescription.Animation = new TranslateAnimation(0f, 0f, txtDescription.MeasuredHeight, 0f);
        //        txtDescription.Animation.Duration = 300;
        //        btnDescExpander.SetImageResource(Resource.Drawable.down_arrow);
        //    }
        //}
        #endregion
    }
    #region " Menu list adapter"
    public class MenuListAdapterClass1 : BaseAdapter<string>
    {
        Activity _context;
        string[] _mnuText;
        int[] _mnuUrl;
        internal event Action<string> actionMenuSelected;
        public MenuListAdapterClass1(Activity context, string[] strMnu, int[] intImage)
        {
            _context = context;
            _mnuText = strMnu;
            _mnuUrl = intImage;
        }
        public override string this[int position]
        {
            get { return this._mnuText[position]; }
        }

        public override int Count
        {
            get { return this._mnuText.Count(); }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            MenuListViewHolderClass1 objMenuListViewHolderClass1;
            View view;
            view = convertView;
            if (view == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.MenuCustomeLayoutHelp, parent, false);
                objMenuListViewHolderClass1 = new MenuListViewHolderClass1();

                objMenuListViewHolderClass1.txtMnuText = view.FindViewById<TextView>(Resource.Id.txtMnuText);
                objMenuListViewHolderClass1.ivMenuImg = view.FindViewById<ImageView>(Resource.Id.ivMenuImg);

                objMenuListViewHolderClass1.initialize(view);
                view.Tag = objMenuListViewHolderClass1;
            }
            else
            {
                objMenuListViewHolderClass1 = (MenuListViewHolderClass1)view.Tag;
            }
            objMenuListViewHolderClass1.viewClicked = () =>
            {
                if (actionMenuSelected != null)
                {
                    actionMenuSelected(_mnuText[position]);
                }
            };
            objMenuListViewHolderClass1.txtMnuText.Text = _mnuText[position];
            objMenuListViewHolderClass1.ivMenuImg.SetImageResource(_mnuUrl[position]);
            return view;
        }
    }

    internal class MenuListViewHolderClass1 : Java.Lang.Object
    {
        internal Action viewClicked { get; set; }
        internal TextView txtMnuText;
        internal ImageView ivMenuImg;
        public void initialize(View view)
        {
            view.Click += delegate
            {
                viewClicked();
            };
        }
    }
    #endregion

}
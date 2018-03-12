using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Views.Animations;

namespace OptimizingFoodPurchasedCostApp
{
    [Activity(Label = "HomeActivity")]
    public class HomeActivity : Activity
    {
        GestureDetector gestureDetector;
        GestureListener gestureListener;

        ListView menuListView;
        MenuListAdapterClass objAdapterMenu;
        ImageView menuIconImageView;
        int intDisplayWidth;
        bool isSingleTapFired = false;
        TextView txtActionBarText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.HomePage);

            FnInitialization();
            TapEvent();
            FnBindMenu(); //find definition in below steps        
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
            objAdapterMenu = new MenuListAdapterClass(this, strMnuText, strMnuUrl);
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
    }

    #region " Menu list adapter"
    public class MenuListAdapterClass : BaseAdapter<string>
    {
        Activity _context;
        string[] _mnuText;
        int[] _mnuUrl;
        internal event Action<string> actionMenuSelected;
        public MenuListAdapterClass(Activity context, string[] strMnu, int[] intImage)
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
            MenuListViewHolderClass objMenuListViewHolderClass;
            View view;
            view = convertView;
            if (view == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.MenuCustomLayout, parent, false);
                objMenuListViewHolderClass = new MenuListViewHolderClass();

                objMenuListViewHolderClass.txtMnuText = view.FindViewById<TextView>(Resource.Id.txtMnuText);
                objMenuListViewHolderClass.ivMenuImg = view.FindViewById<ImageView>(Resource.Id.ivMenuImg);

                objMenuListViewHolderClass.initialize(view);
                view.Tag = objMenuListViewHolderClass;
            }
            else
            {
                objMenuListViewHolderClass = (MenuListViewHolderClass)view.Tag;
            }
            objMenuListViewHolderClass.viewClicked = () =>
            {
                if (actionMenuSelected != null)
                {
                    actionMenuSelected(_mnuText[position]);
                }
            };
            objMenuListViewHolderClass.txtMnuText.Text = _mnuText[position];
            objMenuListViewHolderClass.ivMenuImg.SetImageResource(_mnuUrl[position]);
            return view;
        }
    }

    internal class MenuListViewHolderClass : Java.Lang.Object
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


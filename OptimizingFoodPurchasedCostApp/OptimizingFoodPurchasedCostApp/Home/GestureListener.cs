using System;
using Android.Views;

namespace OptimizingFoodPurchasedCostApp
{
    class GestureListener : Java.Lang.Object, GestureDetector.IOnGestureListener
    {
        public event Action LeftEvent;
        public event Action RightEvent;
        public event Action SingleTapEvent;
        private static int SWIPE_MAX_OFF_PATH = 250;
        private static int SWIPE_MIN_DISTANCE = 100;
        private static int SWIPE_THRESHOLD_VELOCITY = 200;

        public GestureListener()
        {
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            try
            {
                if (Math.Abs(e1.GetY() - e2.GetY()) > SWIPE_MAX_OFF_PATH)
                    return false;
                // right to left swipe
                if (e1.GetX() - e2.GetX() > SWIPE_MIN_DISTANCE && Math.Abs(velocityX) > SWIPE_THRESHOLD_VELOCITY && LeftEvent != null)
                {
                    RightEvent();

                }
                else if (e2.GetX() - e1.GetX() > SWIPE_MIN_DISTANCE && Math.Abs(velocityX) > SWIPE_THRESHOLD_VELOCITY && RightEvent != null)
                {
                    if (e1.GetX() < 100)
                    {
                        Console.WriteLine("e1.GetX() : " + e1.GetX());
                        Console.WriteLine("e2.GetX() : " + e2.GetX());
                        LeftEvent();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Gesture listener didn't worked properly..." + e.Message);
            }
            return false;
        }

        public bool OnDown(MotionEvent e)
        {
            return true;
        }
        public void OnLongPress(MotionEvent e) { }
        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return true;
        }
        public void OnShowPress(MotionEvent e)
        {

        }
        public bool OnSingleTapUp(MotionEvent e)
        {
            SingleTapEvent();
            Console.WriteLine("Single tap up");
            return true;
        }
    }
}
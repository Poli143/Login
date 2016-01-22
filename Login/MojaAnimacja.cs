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
using Android.Views.Animations;

namespace Login
{
    class MojaAnimacja : Animation
    {
        private View mView;          // vidok ktory animujemy
        private int mOriginalHeight; //sledzi waysokosc przed animacja
        private int mTargetHeight;   //wysokosc na ktora musi pojsc
        private int mGrowBy;         //wysokosc do jakiej przejdzie po uruchomieniu animacji

        public MojaAnimacja(View view, int targetHeight)
        {
            mView = view;
            mOriginalHeight = view.Height;
            mTargetHeight = targetHeight;
            mGrowBy = mTargetHeight - mOriginalHeight;

        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            mView.LayoutParameters.Height = (int)(mOriginalHeight + (mGrowBy * interpolatedTime));
            mView.RequestLayout();

        }
        public override bool WillChangeBounds()
        {
            return true;
        }
    }
}
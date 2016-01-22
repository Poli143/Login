using System;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Util;

namespace Login
{
    public class SlidingTabScrollView :HorizontalScrollView
    {
        private const int TITLE_OFFSET_DIPS = 24;
        private const int TAB_VIEW_PADDING_DIPS = 16;// odstep
        private const int TAB_VIEW_TEXT_SIZE_SP = 12; //

        private int mTitleOffset;

        private int mTabViewLayoutID;
        private int mTabViewTextViewID;

        private ViewPager mViewPager;
        private ViewPager.IOnPageChangeListener mViewPagerPageChangeListener;

        private static SlideTabStrip mTabStrip;

        private int mScrollState;

        public interface TabColorizer
        {
            int GetIndicatorColor(int position);
            int GetDividerColor(int position);
        }

        public SlidingTabScrollView (Context context) : this(context, null) { }

        public SlidingTabScrollView (Context context, IAttributeSet attrs) : this(context, attrs, 0) { }

        public SlidingTabScrollView(Context context, IAttributeSet attrs, int deflautStyle) : base(context, attrs, deflautStyle)
        {
            // wylaczenie scrollbara
            HorizontalScrollBarEnabled = false;

            //upewnienie sie ze tab strip wypelni pole
            FillViewport = true;
            this.SetBackgroundColor(Android.Graphics.Color.Rgb(0xE5, 0xE5, 0xE5)); //szary kolor

            mTitleOffset = (int)(TITLE_OFFSET_DIPS * Resources.DisplayMetrics.Density);

            mTabStrip = new SlideTabStrip(context);
            this.AddView(mTabStrip, LayoutParams.MatchParent, LayoutParams.MatchParent);


        }
        public TabColorizer CustomTabColorizer
        {
            set{mTabStrip.CustomTabColorizer = value;}
        }
        public int[] SelectedIndicatorColor
        {
            set { mTabStrip.SelectedIndicatorColors = value; }
        }
        public int[] DividerColors
        {
            set { mTabStrip.DividerColors = value; }
        }

        public ViewPager.IOnPageChangeListener OnPageListener
        {
            set { mViewPagerPageChangeListener = value; }
        }

        public ViewPager ViewPager
        {
            set
            {
                mTabStrip.RemoveAllViews();

                mViewPager = value;
                if (value != null)
                {
                    value.PageSelected += value_PageSelected;                           //zmiana widokow
                    value.PageScrollStateChanged += value_PageScrollStateChanged;
                    value.PageScrolled += value_PageScrolled;
                    PopulateTabStrip();
                }
            }
        }

        private void value_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)  //kiedy strona jest scrollowana
        {
            int tabCount = mTabStrip.ChildCount;

            if ((tabCount == 0) || (e.Position < 0) || (e.Position >= tabCount))
            {
                //jesli jakis z warunkow zostanie spe³niony nie scrolluje dalej
                return;
            }
            mTabStrip.OnViewPagerPageChanged(e.Position, e.PositionOffset);

            View selectedTitle = mTabStrip.GetChildAt(e.Position);
            int extraOffset = (selectedTitle != null ? (int)(e.Position * selectedTitle.Width) : 0); // sprawdza ile jeszcze mozna scrollowac

            //Scroluje do danego okna
            ScrollToTab(e.Position, extraOffset);
            //sprawdzanie czy uzytkownik daje nam swoj wlasny listener
            if (mViewPagerPageChangeListener!=null)
            {
                mViewPagerPageChangeListener.OnPageScrolled(e.Position, e.PositionOffset, e.PositionOffsetPixels);
            }


        }

        private void value_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            mScrollState = e.State;
            if (mViewPagerPageChangeListener != null)
            {
                mViewPagerPageChangeListener.OnPageScrollStateChanged(e.State);
            }

        }

        private void value_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if (mScrollState==ViewPager.ScrollStateIdle)
            {
                mTabStrip.OnViewPagerPageChanged(e.Position, 0f);
                ScrollToTab(e.Position, 0);
            }
            if (mViewPagerPageChangeListener!=null)
            {
                mViewPagerPageChangeListener.OnPageSelected(e.Position);
            }
        }

        private void PopulateTabStrip()
        {
            PagerAdapter adapter = mViewPager.Adapter;
            for (int i = 0; i < adapter.Count; i++)
            {
                TextView tabView = CreateDeflautTabView(Context);
                tabView.Text = ((SlidingTabsFragment.SamplePagerAdapter)adapter).GetHeaderTitle(i);
                tabView.SetTextColor(Android.Graphics.Color.Black);
                tabView.Tag = i;                                    //jesli klikniesz na dana zak³adke przeniesie widok do niej
                tabView.Click += tabView_Click;
                mTabStrip.AddView(tabView);
            }
        }

        private void tabView_Click(object sender, EventArgs e)
        {
            TextView clickTab = (TextView)sender;
            int pageToScrollTo = (int)clickTab.Tag;
            mViewPager.CurrentItem = pageToScrollTo;
        }

        private TextView CreateDeflautTabView(Context context)
        {
            TextView textView = new TextView(context);
            textView.Gravity = GravityFlags.Center;
            textView.SetTextSize(ComplexUnitType.Sp, TAB_VIEW_TEXT_SIZE_SP);
            textView.Typeface = Android.Graphics.Typeface.DefaultBold;

            //sprawdzanie wersji SDK androida 
            if (Build.VERSION.SdkInt>=Android.OS.BuildVersionCodes.Honeycomb)
            {
                TypedValue outValue = new TypedValue();
                Context.Theme.ResolveAttribute(Android.Resource.Attribute.SelectableItemBackground, outValue, false);
                textView.SetBackgroundResource(outValue.ResourceId);
            }
            if (Build.VERSION.SdkInt>=Android.OS.BuildVersionCodes.IceCreamSandwich)
            {
                textView.SetAllCaps(true);
            }

            int padding = (int)(TAB_VIEW_PADDING_DIPS * Resources.DisplayMetrics.Density);
            textView.SetPadding(padding, padding, padding, padding);
            return textView;
        }

        //
        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (mViewPager!=null)
            {
                ScrollToTab(mViewPager.CurrentItem, 0);
            }
        }

        private void ScrollToTab(int tabIndex, int extraOffset)
        {
            int tabCount = mTabStrip.ChildCount;
            if (tabCount==0||tabIndex<0||tabIndex>=tabCount)
            {
                // nie scrolluje dalej
                return;
            }
            View selectedChild = mTabStrip.GetChildAt(tabIndex);
            if (selectedChild == null)
            {
                int scrollAmountX = selectedChild.Left + extraOffset;

                if (tabIndex >0||extraOffset>0)
                {
                    scrollAmountX -= mTitleOffset;
                }
                this.ScrollTo(scrollAmountX, 0);
            }
        }
    }
}
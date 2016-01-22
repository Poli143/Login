
using Android.App;
using Android.Support.V4.App;
using Android.Support.V4.Widget;

namespace Login
{
    class MyActionBarDrawerToggle : ActionBarDrawerToggle
    {
        Activity mActivity;

        public MyActionBarDrawerToggle(Activity activity, DrawerLayout drawerLayout, int imageResource, int openDrawerDesc, int closeDrawerDesc)
            : base(activity, drawerLayout, imageResource, openDrawerDesc, closeDrawerDesc)
        {
            mActivity = activity;
        }
        //otwarcie menu opcji
        public override void OnDrawerOpened(Android.Views.View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                //Left Drawer
                base.OnDrawerOpened(drawerView);
                mActivity.ActionBar.Title = "";
            }
        }
        //zamniecie menu opcji
        public override void OnDrawerClosed(Android.Views.View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                //Left Drawer
                base.OnDrawerClosed(drawerView);
                mActivity.ActionBar.Title = "";
            }
        }
        public override void OnDrawerSlide(Android.Views.View drawerView, float slideOffset)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                //Left Drawer
                base.OnDrawerSlide(drawerView, slideOffset);
            }

        }
    }
}
    
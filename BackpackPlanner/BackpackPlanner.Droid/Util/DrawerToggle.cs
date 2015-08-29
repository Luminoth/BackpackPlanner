using System;

using Android.App;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public class DrawerToggle : ActionBarDrawerToggle
    {
#region Events
        public event EventHandler<DrawerToggleEventArgs> DrawerClosedEvent;
        public event EventHandler<DrawerToggleEventArgs> DrawerOpenedEvent;
        public event EventHandler<DrawerToggleEventArgs> DrawerSlideEvent;
        public event EventHandler<DrawerToggleEventArgs> DrawerStateChangedEvent;
#endregion

        public DrawerToggle(Activity activity, DrawerLayout drawerLayout, int openDrawerContentDescRes, int closeDrawerContentDescRes)
            : base(activity, drawerLayout, openDrawerContentDescRes, closeDrawerContentDescRes)
        {
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            DrawerClosedEvent?.Invoke(this, new DrawerToggleEventArgs { DrawerView = drawerView });
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            DrawerOpenedEvent?.Invoke(this, new DrawerToggleEventArgs { DrawerView = drawerView });
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
            DrawerSlideEvent?.Invoke(this, new DrawerToggleEventArgs { DrawerView = drawerView, SlideOffset = slideOffset });
        }

        public override void OnDrawerStateChanged(int newState)
        {
            base.OnDrawerStateChanged(newState);
            DrawerStateChangedEvent?.Invoke(this, new DrawerToggleEventArgs { NewState = newState });
        }
    }
}

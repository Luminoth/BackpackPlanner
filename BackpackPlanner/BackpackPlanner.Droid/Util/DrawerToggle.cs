/*
   Copyright 2015 Shane Lillie

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;

using Android.App;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
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

        public DrawerToggle(Activity activity, DrawerLayout drawerLayout, Toolbar toolbar, int openDrawerContentDescRes, int closeDrawerContentDescRes)
            : base(activity, drawerLayout, toolbar, openDrawerContentDescRes, closeDrawerContentDescRes)
        {
        }

        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
            DrawerOpenedEvent?.Invoke(this, new DrawerToggleEventArgs { DrawerView = drawerView });
        }

        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
            DrawerClosedEvent?.Invoke(this, new DrawerToggleEventArgs { DrawerView = drawerView });
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

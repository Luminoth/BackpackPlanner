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

using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

using Java.Lang;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://github.com/android/platform_development/blob/master/samples/Support7Demos/src/com/example/android/supportv7/app/AppCompatPreferenceActivity.java
    /// </remarks>
    public class AppCompatPreferenceActivity : PreferenceActivity
    {
        private AppCompatDelegate _delegate;

        private AppCompatDelegate Delegate => _delegate ?? (_delegate = AppCompatDelegate.Create(this, null));

        public Android.Support.V7.App.ActionBar SupportActionBar => Delegate.SupportActionBar;

        public override MenuInflater MenuInflater => Delegate.MenuInflater;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Delegate.InstallViewFactory();
            Delegate.OnCreate(savedInstanceState);

            base.OnCreate(savedInstanceState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            Delegate.OnPostCreate(savedInstanceState);
        }

        public override void SetContentView(int layoutResID)
        {
            Delegate.SetContentView(layoutResID);
        }

        public override void SetContentView(View view)
        {
            Delegate.SetContentView(view);
        }

        public override void SetContentView(View view, ViewGroup.LayoutParams lp)
        {
            Delegate.SetContentView(view, lp);
        }

        public override void AddContentView(View view, ViewGroup.LayoutParams lp)
        {
            Delegate.AddContentView(view, lp);
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();

            Delegate.OnPostResume();
        }

        protected override void OnTitleChanged(ICharSequence title, Color color)
        {
            base.OnTitleChanged(title, color);

            Delegate.SetTitle(title);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            Delegate.OnConfigurationChanged(newConfig);
        }

        protected override void OnStop()
        {
            base.OnStop();

            Delegate.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Delegate.OnDestroy();
        }

        public override void InvalidateOptionsMenu()
        {
            Delegate.InvalidateOptionsMenu();
        }

        public void SetSupportActionBar(Toolbar toolbar)
        {
            Delegate.SetSupportActionBar(toolbar);
        }
    }
}

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
using Android.OS;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.FTUE;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    [Activity(Label = "@string/app_name")]
    // ReSharper disable once InconsistentNaming
    public sealed class FTUEActivity : BaseActivity
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(FTUEActivity));

        // ReSharper disable once InconsistentNaming
        private sealed class FTUEPagerAdapter : Android.Support.V4.App.FragmentPagerAdapter
        {
            public override int Count => 5;

            public FTUEPagerAdapter(Android.Support.V4.App.FragmentManager fragmentManager) : base(fragmentManager)
            {
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                switch(position)
                {
                case 0:
                    return new WelcomeFragment();
                case 1:
                    return new GearFragment();
                case 2:
                    return new MealsFragment();
                case 3:
                    return new TripsFragment();
                case 4:
                    return new FinishFragment();
                default:
                    throw new ArgumentException("Invalid position", nameof(position));
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_ftue);

            InitToolbar();

            Android.Support.V4.View.ViewPager viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.ftue_pager);
            viewPager.Adapter = new FTUEPagerAdapter(SupportFragmentManager);

            IViewPagerIndicator pageIndicator = FindViewById<CircleViewPagerIndicator>(Resource.Id.view_pager_indicator);
            pageIndicator.SetViewPager(viewPager);

            Title = Resources.GetString(Resource.String.app_name);
        }

        /// <summary>
        /// Finishes the ftue.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public void FinishFTUE()
        {
            Logger.Debug("Finishing FTUE...");

            // prompt the user to connect to Google Play Services
            DialogUtil.ShowYesNoDialog(this, Resource.String.label_connect_google_play_services, Resource.String.title_connect_google_play_services,
                (sender, args) => {
                    Logger.Debug("User accepted Google Play Services");
                    BackpackPlannerState.Settings.ConnectGooglePlayServices = true;

                    // try to connect now to get all of the confirmations out of the way
                    ProgressDialog dialog = DialogUtil.ShowProgressDialog(this, Resource.String.label_connecting_google_play_services, false);
                    BackpackPlannerState.PlatformPlayServicesManager.PlayServicesConnectedEvent += (s, a) => {
                        Logger.Debug($"Google Play Services connected (success: {a.IsSuccess}), finishing activity...");
                        dialog.Dismiss();

                        StartActivity(typeof(BackpackPlannerActivity));
                        Finish();
                    };
                    BackpackPlannerState.PlatformPlayServicesManager.ConnectAsync().Wait();
                },
                (sender, args) => {
                    Logger.Debug("User declined Google Play Services");
                    BackpackPlannerState.Settings.ConnectGooglePlayServices = false;

                    StartActivity(typeof(BackpackPlannerActivity));
                    Finish();
                }
            );
        }
    }
}

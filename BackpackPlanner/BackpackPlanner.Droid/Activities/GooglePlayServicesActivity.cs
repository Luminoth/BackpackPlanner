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

using Android.App;
using Android.OS;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Activities
{
    [Activity(Label = "@string/app_name")]
    public sealed class GooglePlayServicesActivity : BaseActivity
    {
        public GooglePlayServicesActivity() : base(true)
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            BackpackPlannerState.Instance.PlatformPlayServices.PlayServicesConnectedEvent += (sender, args) => {
                StartActivity(typeof(BackpackPlannerActivity));
                Finish();
            };

			SetContentView(Resource.Layout.activity_google_play_services);

            Title = Resources.GetString(Resource.String.app_name);

            FragmentTransitionUtil.Transition(this, SupportFragmentManager.BeginTransaction(), Resource.Id.frame_content, new GooglePlayServicesFragment());
        }
    }
}

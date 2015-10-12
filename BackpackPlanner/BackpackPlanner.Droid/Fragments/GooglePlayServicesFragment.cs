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
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public sealed class GooglePlayServicesFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_google_play_services, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            BackpackPlannerState.Instance.Settings.MetaSettings.AskedConnectGooglePlayServices = true;

            Button connectGooglePlayServicesButton = view.FindViewById<Button>(Resource.Id.button_connect_google_play_services);
            connectGooglePlayServicesButton.Click += (sender, args) => {
                BackpackPlannerState.Instance.Settings.ConnectGooglePlayServices = true;

                ProgressDialog dialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_connecting_google_play_services);
                BackpackPlannerState.Instance.PlatformPlayServices.PlayServicesConnectedEvent += (s, a) => {
                    dialog.Dismiss();

                    Activity.StartActivity(typeof(BackpackPlannerActivity));
                    Activity.Finish();
                };

                BackpackPlannerState.Instance.PlatformPlayServices.Connect();
            };

            Button notConnectGooglePlayServicesButton = view.FindViewById<Button>(Resource.Id.button_not_connect_google_play_services);
            notConnectGooglePlayServicesButton.Click += (sender, args) => {
                BackpackPlannerState.Instance.Settings.ConnectGooglePlayServices = false;

                Activity.StartActivity(typeof(BackpackPlannerActivity));
                Activity.Finish();
            };
        }
    }
}

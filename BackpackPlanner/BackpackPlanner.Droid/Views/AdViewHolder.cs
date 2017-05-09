/*
   Copyright 2017 Shane Lillie

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

using Android.Gms.Ads;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;

namespace EnergonSoftware.BackpackPlanner.Droid.Views
{
    public sealed class AdViewHolder<T> : BaseRecyclerViewHolder<T>
        where T: class
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(AdViewHolder<T>));

        private readonly ViewGroup _adCardView;

        public AdViewHolder(View view, BaseRecyclerListAdapter<T> adapter)
            : base(view, adapter)
        {
            _adCardView = view.FindViewById<Android.Support.V7.Widget.CardView>(Resource.Id.view_ad);
        }

        public override void UpdateView(T item)
        {
            //base.UpdateView(item);

            _adCardView.RemoveAllViews();

            if(!Adapter.AdViews.TryGetValue(AdapterPosition, out NativeExpressAdView adView)) {
                adView = CreateAdView(Adapter.Fragment);
                Adapter.AddAdView(AdapterPosition, adView);
            }
            _adCardView.AddView(adView);
        }

        private NativeExpressAdView CreateAdView(RecyclerFragment fragment)
        {
            NativeExpressAdView adView = new NativeExpressAdView(fragment.Context)
            {
            #if DISTRIBUTION
                AdUnitId = Fragment.GetString(Resource.String.native_ad_unit_id)
            #else
                AdUnitId = fragment.GetString(Resource.String.test_native_ad_unit_id)
            #endif
            };

            // NOTE: this works because we create the view *after* the recycler has been sized
            float density = fragment.Resources.DisplayMetrics.Density;
            adView.AdSize = new AdSize((int)(fragment.Layout.Width / density), 150);

            AdRequest.Builder builder = new AdRequest.Builder();
            Activity.AdManager.AddTestDevices(builder);
            Activity.AdManager.SetGender(fragment.BaseActivity.BackpackPlannerState.PersonalInformation, builder);
            AdRequest adRequest = builder.Build();

            Logger.Debug($"Loading ad, is test device: {adRequest.IsTestDevice(fragment.Context)}");
            adView.LoadAd(adRequest);

            return adView;
        }
    }
}

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

using System.Collections.Generic;

using Android.Gms.Ads;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseRecyclerListAdapterWithAds<T> : BaseRecyclerListAdapter<T> where T: class
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseRecyclerListAdapterWithAds<T>));

#if DEBUG
        private const int AdFrequency = 2;
#else
        private const int AdFrequency = 10;
#endif

        protected const int AdViewType = 1;

        private sealed class AdViewHolder : BaseViewHolder
        {
            public AdViewHolder(View itemView, BaseRecyclerListAdapterWithAds<T> adapter)
                : base(itemView, adapter)
            {
            }
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            switch(viewType)
            {
            case AdViewType:
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_ad, parent, false);
                return new AdViewHolder(itemView, this);
            case ListItemViewType:
            default:
                return base.OnCreateViewHolder(parent, viewType);
            }
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            int viewType = GetItemViewType(position);
            switch(viewType)
            {
            case AdViewType:
                BindAdViewHolder(holder);
                break;
            case ListItemViewType:
            default:
                base.OnBindViewHolder(holder, position);
                break;
            }
        }

        private void BindAdViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder)
        {
            AdViewHolder adHolder = (AdViewHolder)holder;
            ViewGroup adCardView = (ViewGroup)adHolder.ItemView;
            adCardView.RemoveAllViews();

            // TODO: probably not the best idea to create this every time we view the thing
            // TODO: and this might be a bigger problem for the app as a whole
            // where we are creating views every time we see something
            // rather than holding references to the views as the "list items"
            NativeExpressAdView adView = CreateAdView();
            adCardView.AddView(adView);
        }

        private NativeExpressAdView CreateAdView()
        {
            NativeExpressAdView adView = new NativeExpressAdView(Fragment.Context)
            {
            #if USE_REAL_ADS
                AdUnitId = Fragment.GetString(Resource.String.native_ad_unit_id)
            #else
                AdUnitId = Fragment.GetString(Resource.String.test_native_ad_unit_id)
            #endif
            };

            // NOTE: this works because we create the view *after*
            // the recycler has been sized (I think)
            float density = Fragment.Resources.DisplayMetrics.Density;
            adView.AdSize = new AdSize((int)(Fragment.Layout.Width / density), 150);

            AdRequest.Builder builder = new AdRequest.Builder();
            TestDevices.AddTestDevices(builder);
            TestDevices.SetGender(Fragment.BaseActivity.BackpackPlannerState.PersonalInformation, builder);
            AdRequest adRequest = builder.Build();

            Logger.Debug($"Loading ad, is test device: {adRequest.IsTestDevice(Fragment.Context)}");
            adView.LoadAd(adRequest);

            return adView;
        }

        private bool IsAdPosition(int position)
        {
            return 0 != position && 0 == (position + 1) % AdFrequency;
        }

        public override int GetItemViewType(int position)
        {
            bool isAd = IsAdPosition(position);
            return isAd ? AdViewType : base.GetItemViewType(position);
        }

        public override void SetItems(IReadOnlyCollection<T> items)
        {
            var newItemList = new List<T>(items);
            for(int i=AdFrequency-1; i<newItemList.Count; i+=AdFrequency) {
                newItemList.Insert(i, null);
            }
            base.SetItems(newItemList);
        }

        protected BaseRecyclerListAdapterWithAds(RecyclerFragment fragment)
            : base(fragment)
        {
        }
    }
}

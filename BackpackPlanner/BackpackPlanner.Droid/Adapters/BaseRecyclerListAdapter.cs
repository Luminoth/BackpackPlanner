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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

using Android.Gms.Ads;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;
using EnergonSoftware.BackpackPlanner.Droid.Views;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseRecyclerListAdapter<T> : Android.Support.V7.Widget.RecyclerView.Adapter
        where T: class
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseRecyclerListAdapter<T>));

#region View Types
        private const int ViewTypeListItem = 0;
        private const int ViewTypeAdItem = 1;
#endregion

#if DEBUG
        private const int AdFrequency = 5;
#else
        private const int AdFrequency = 10;
#endif
        private const int HalfAdFrequencey = AdFrequency / 2;

#region View Holders
        protected abstract class BaseRecyclerViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder, IViewHolder<T>
        {
            protected BaseActivity BaseActivity => Adapter.Fragment.BaseActivity;

            protected RecyclerFragment Fragment => Adapter.Fragment;

            protected BaseRecyclerListAdapter<T> Adapter { get; }

            protected T Item { get; private set; }

            public virtual void UpdateView(T item)
            {
                Item = item;
            }

            protected BaseRecyclerViewHolder(View view, BaseRecyclerListAdapter<T> adapter)
                : base(view)
            {
                Adapter = adapter;
            }
        }

        private sealed class AdViewHolder : BaseRecyclerViewHolder
        {
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

                if(!Adapter._adViews.TryGetValue(AdapterPosition, out NativeExpressAdView adView)) {
                    adView = CreateAdView(Adapter.Fragment);
                    Adapter._adViews.Add(AdapterPosition, adView);
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
                BaseActivity.AdManager.AddTestDevices(builder);
                BaseActivity.AdManager.SetGender(fragment.BaseActivity.BackpackPlannerState.PersonalInformation, builder);
                AdRequest adRequest = builder.Build();

                Logger.Debug($"Loading ad, is test device: {adRequest.IsTestDevice(fragment.Context)}");
                adView.LoadAd(adRequest);

                return adView;
            }
        }
#endregion

        protected RecyclerFragment Fragment { get; }

        protected abstract int LayoutResource { get; }

        public override int ItemCount => ProcessedListItems.Count;

        [NotNull]
        private List<T> _fullListItems = new List<T>();

        public IReadOnlyCollection<T> FullListItems
        {
            get => _fullListItems;

            set
            {
                _fullListItems = null == value ? new List<T>() : new List<T>(value);

                ProcessItems();
            }
        }

        [NotNull]
        private List<T> _processedListItems = new List<T>();

        protected IReadOnlyCollection<T> ProcessedListItems
        {
            get => _processedListItems;

            set 
            {
                _processedListItems = null == value ? new List<T>(FullListItems) : new List<T>(value);

                NotifyDataSetChanged();
            }
        }

        private readonly Dictionary<int, NativeExpressAdView> _adViews = new Dictionary<int, NativeExpressAdView>();

#region ViewHolder
        private BaseRecyclerViewHolder CreateAdViewHolder(ViewGroup parent)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_ad, parent, false);
            return new AdViewHolder(view, this);
        }

        protected abstract BaseRecyclerViewHolder CreateViewHolder(View view, BaseRecyclerListAdapter<T> adapter);

        private BaseRecyclerViewHolder CreateItemViewHolder(ViewGroup parent)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(LayoutResource, parent, false);
            return CreateViewHolder(view, this);
        }

        public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            switch(viewType)
            {
            case ViewTypeAdItem:
                return CreateAdViewHolder(parent);
            case ViewTypeListItem:
            default:
                return CreateItemViewHolder(parent);
            }
        }

        private void BindAdViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder)
        {
            BaseRecyclerViewHolder baseViewHolder = (BaseRecyclerViewHolder)holder;
            baseViewHolder.UpdateView(null);
        }

        private void BindItemViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            BaseRecyclerViewHolder baseViewHolder = (BaseRecyclerViewHolder)holder;
            T item = ProcessedListItems.ElementAt(position);
            baseViewHolder.UpdateView(item);
        }

        public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            int viewType = GetItemViewType(position);
            switch(viewType)
            {
            case ViewTypeAdItem:
                BindAdViewHolder(holder);
                break;
            case ViewTypeListItem:
            default:
                BindItemViewHolder(holder, position);
                break;
            }
        }
#endregion

#region Add/Remove items
        // used when undoing deleting an item
        public void AddItem(T item)
        {
            _fullListItems.Add(item);
            ProcessItems();
        }

        // used when deleting an item
        public void RemoveItem(T item)
        {
            _fullListItems.Remove(item);
            ProcessItems();
        }
#endregion

        // do NOT call this if setting the ProcessedListItems
        // or NotifyDataSetChanged() will get over-called
        protected virtual void ProcessItems()
        {
            ProcessedListItems = null;
        }

        [Conditional("ENABLE_ADS")]
        protected void InjectAds()
        {
            // make sure we actually need to inject any ads
            int adCount = ProcessedListItems.Count / AdFrequency;
            if(0 == adCount) {
                return;
            }

            int loopEnd = ProcessedListItems.Count;

            // make sure we don't have a final ad too close to the end
            // (it looks really bad when that happens)
            int itemsAfterFinalAd = ProcessedListItems.Count % adCount;
            if(itemsAfterFinalAd < HalfAdFrequencey) {
                loopEnd -= HalfAdFrequencey + 1;
            }

            for(int i=AdFrequency-1; i<=loopEnd; i+=AdFrequency, ++loopEnd) {
                _processedListItems.Insert(i, null);
            }

            NotifyDataSetChanged();
        }

        private bool IsAdPosition(int position)
        {
#if ENABLE_ADS
            return null == ProcessedListItems.ElementAt(position);
#else
            return false;
#endif
        }

        public override int GetItemViewType(int position)
        {
            return IsAdPosition(position) ? ViewTypeAdItem : ViewTypeListItem;
        }

        protected BaseRecyclerListAdapter(RecyclerFragment fragment)
        {
            Fragment = fragment;
        }
    }
}

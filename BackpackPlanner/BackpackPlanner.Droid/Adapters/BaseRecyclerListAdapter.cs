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
using System.Linq;

using Android.Gms.Ads;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;

using JetBrains.Annotations;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseRecyclerListAdapter<T> : Android.Support.V7.Widget.RecyclerView.Adapter where T: class
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseRecyclerListAdapter<T>));

#region View Types
        private const int ViewTypeListItem = 0;
        private const int ViewTypeAdItem = 1;
#endregion

#if DEBUG
        private const int AdFrequency = 2;
#else
        private const int AdFrequency = 10;
#endif

        protected abstract class BaseViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
        {
            protected BaseRecyclerListAdapter<T> Adapter { get; }

            private T _listItem;

            [CanBeNull]
            public T ListItem
            {
                get => _listItem;

                set
                {
                    _listItem = value;
                    UpdateView();
                }
            }

            protected virtual void UpdateView()
            {
            }

            protected BaseViewHolder(View itemView, BaseRecyclerListAdapter<T> adapter)
                : base(itemView)
            {
                Adapter = adapter;
            }
        }

        private sealed class AdViewHolder : BaseViewHolder
        {
            public AdViewHolder(View itemView, BaseRecyclerListAdapter<T> adapter)
                : base(itemView, adapter)
            {
            }
        }

        public RecyclerFragment Fragment { get; }

        public abstract int LayoutResource { get; }

        public override int ItemCount => ListItems.Count;

        [NotNull]
        private List<T> _listItems = new List<T>();

        [NotNull]
        public IReadOnlyCollection<T> ListItems => _listItems;

#region ViewHolder
        private BaseViewHolder CreateAdViewHolder(ViewGroup parent)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_ad, parent, false);
            return new AdViewHolder(itemView, this);
        }

        protected abstract BaseViewHolder CreateViewHolder(View itemView);

        private BaseViewHolder CreateItemViewHolder(ViewGroup parent)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(LayoutResource, parent, false);
            return CreateViewHolder(itemView);
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

            // NOTE: this works because we create the view *after* the recycler has been sized
            float density = Fragment.Resources.DisplayMetrics.Density;
            adView.AdSize = new AdSize((int)(Fragment.Layout.Width / density), 150);

            // TODO: do we have to keep re-creating this?
            AdRequest.Builder builder = new AdRequest.Builder();
            TestDevices.AddTestDevices(builder);
            TestDevices.SetGender(Fragment.BaseActivity.BackpackPlannerState.PersonalInformation, builder);
            AdRequest adRequest = builder.Build();

            Logger.Debug($"Loading ad, is test device: {adRequest.IsTestDevice(Fragment.Context)}");
            adView.LoadAd(adRequest);

            return adView;
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

        private void BindItemViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
        {
            BaseViewHolder baseViewHolder = (BaseViewHolder)holder;
            T item = ListItems.ElementAt(position);
            baseViewHolder.ListItem = item;
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
        public virtual void SetItems(IReadOnlyCollection<T> items)
        {
            var newItemList = new List<T>(items);
#if ENABLE_ADS
            for(int i=AdFrequency-1; i<newItemList.Count; i+=AdFrequency) {
                newItemList.Insert(i, null);
            }
#endif
            _listItems = newItemList;
        }

        // used when undoing deleting an item
        public virtual void AddItem(T item)
        {
            _listItems.Add(item);
        }

        // used when deleting an item
        public virtual void RemoveItem(T item)
        {
            _listItems.Remove(item);
        }
#endregion

        private bool IsAdPosition(int position)
        {
#if ENABLE_ADS
            return 0 != position && 0 == (position + 1) % AdFrequency;
#else
            return false;
#endif
        }

        public override int GetItemViewType(int position)
        {
            bool isAd = IsAdPosition(position);
            return isAd ? ViewTypeAdItem : ViewTypeListItem;
        }

        protected BaseRecyclerListAdapter(RecyclerFragment fragment)
        {
            Fragment = fragment;
        }
    }
}

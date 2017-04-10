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
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data listing fragments
    /// </summary>
    public abstract class ListItemsFragment<T> : RecyclerFragment where T: BaseModel, IBackpackPlannerItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(ListItemsFragment<T>));

        private sealed class ListAdapterDataObserver : Android.Support.V7.Widget.RecyclerView.AdapterDataObserver
        {
            private readonly ListItemsFragment<T> _fragment;

            public ListAdapterDataObserver(ListItemsFragment<T> fragment)
            {
                _fragment = fragment;
            }

            public override void OnChanged()
            {
                base.OnChanged();

                _fragment.UpdateView();
            }
        }

        protected abstract int WhatIsAnItemButtonResource { get; }

        protected abstract int WhatIsAnItemTitleResource { get; }

        protected abstract int WhatIsAnItemTextResource { get; }

        protected abstract int DeleteItemConfirmationTextResource { get; }

        protected abstract int DeleteItemConfirmationTitleResource { get; }

        protected abstract int NoItemsResource { get; }

        protected abstract int SortItemsResource { get; }

        protected abstract int AddItemResource { get; }

        protected override bool HasSearchView => true;

        protected override bool CanExport => true;

        protected BaseModelRecyclerListAdapter<T> Adapter { get; private set; } 

#region Controls
        private TextView _noItemsTextView;

        public Spinner SortItemsSpinner { get; private set; }
#endregion

        protected abstract Android.Support.V4.App.Fragment CreateAddItemFragment();

        protected abstract BaseModelRecyclerListAdapter<T> CreateAdapter();

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Adapter = CreateAdapter();
            Layout.SetAdapter(Adapter);

            Adapter.RegisterAdapterDataObserver(new ListAdapterDataObserver(this));

            _noItemsTextView = View.FindViewById<TextView>(NoItemsResource);

            SortItemsSpinner = View.FindViewById<Spinner>(SortItemsResource);
            SortItemsSpinner.ItemSelected += Adapter.SortByItemSelectedEventHander;

            Button whatIsAButton = view.FindViewById<Button>(WhatIsAnItemButtonResource);
            whatIsAButton.Click += (sender, args) => {
                DialogUtil.ShowOkAlert(Activity, WhatIsAnItemTextResource, WhatIsAnItemTitleResource);
            };

            Android.Support.Design.Widget.FloatingActionButton addItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(AddItemResource);
            addItemButton.Click += (sender, args) => {
                TransitionToFragment(Resource.Id.frame_content, CreateAddItemFragment(), null);
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            PopulateList();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            FilterView.QueryTextChange += Adapter.FilterItemsEventHandler;
        }

        protected abstract Task<IReadOnlyCollection<T>> GetItemsAsync(DatabaseContext dbContext);

        protected void PopulateList()
        {
            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            Task.Run(async () =>
                {
                    IReadOnlyCollection<T> items;
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        try {
                            items = await GetItemsAsync(dbContext).ConfigureAwait(false);
                        } catch(Exception e) {
                            Logger.Error($"Error loading {typeof(T)}s: {e.Message}", e);

                            items = null;
                        }
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        if(null == items) {
                            DialogUtil.ShowOkAlert(Activity, Resource.String.message_error_reading_items, Resource.String.title_error_reading_items);
                            return;
                        }

                        Logger.Debug($"Read {items.Count} {typeof(T)}s...");
                        Adapter.FullListItems = items;
                    });
                }
            );
        }

        public void DeleteItem(T item)
        {
            DialogUtil.ShowOkCancelAlert(Activity, DeleteItemConfirmationTextResource, DeleteItemConfirmationTitleResource,
                (sender, args) => DeleteItemEventHandler(sender, args, item));
        }

        private void DeleteItemEventHandler(object sender, DialogClickEventArgs args, T item)
        {
            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_deleting_item, false, true);

            Task.Run(async () =>
                {
                    int count;
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        try {
                            item.IsDeleted = true;
                            count = await dbContext.SaveChangesAsync().ConfigureAwait(false);
                            item.OnRemove();
                        } catch(Exception e) {
                            Logger.Error($"Error deleting {typeof(T)}: {e.Message}", e);

                            item.IsDeleted = false;
                            count = -1;
                        }
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        if(count < 1) {
                            DialogUtil.ShowOkAlert(Activity, Resource.String.message_error_deleting_item, Resource.String.title_error_deleting_item);
                            return;
                        }

                        Adapter.RemoveItem(item);

                        SnackbarUtil.ShowUndoSnackbar(View, Resource.String.label_deleted_item, Android.Support.Design.Widget.Snackbar.LengthLong,
                            view => UndoDeleteItemEventHandler(view, item));
                    });
                }
            );
        }

        private void UndoDeleteItemEventHandler(View view, T item)
        {
            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_deleted_item_undoing, false, true);

            Task.Run(async () =>
                {
                    int count;
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        try {
                            item.IsDeleted = false;
                            count = await dbContext.SaveChangesAsync().ConfigureAwait(false);
                            // TODO: re-hook any PropertyChanged listeners
                        } catch(Exception e) {
                            Logger.Error($"Error restoring {typeof(T)}: {e.Message}", e);

                            item.IsDeleted = true;
                            count = -1;
                        }
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                            if(count < 1) {
                                DialogUtil.ShowOkAlert(Activity, Resource.String.message_error_restoring_item, Resource.String.title_error_restoring_item);
                                return;
                            }

                        Adapter.AddItem(item);

                        SnackbarUtil.ShowSnackbar(view, Resource.String.label_deleted_item_undone, Android.Support.Design.Widget.Snackbar.LengthShort);
                    });
                }
            );
        }

        private void UpdateView()
        {
            bool hasItems = Adapter.ItemCount > 0;

            _noItemsTextView.Visibility = hasItems ? ViewStates.Gone : ViewStates.Visible;

            if(null != SortItemsSpinner) {
                SortItemsSpinner.Visibility = hasItems ? ViewStates.Visible : ViewStates.Gone;
            }

            Layout.Visibility = hasItems ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}

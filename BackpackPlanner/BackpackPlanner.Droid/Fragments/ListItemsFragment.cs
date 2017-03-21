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

using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Commands;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Models;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data listing fragments
    /// </summary>
    public abstract class ListItemsFragment<T> : RecyclerFragment where T: DatabaseItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(ListItemsFragment<T>));

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

        protected BaseListAdapter<T> Adapter { get; private set; } 

#region Controls
        private TextView _noItemsTextView;

        public Spinner SortItemsSpinner { get; private set; }
#endregion

        protected abstract Android.Support.V4.App.Fragment CreateAddItemFragment();

        protected abstract BaseListAdapter<T> CreateAdapter();

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Adapter = CreateAdapter();
            Layout.SetAdapter(Adapter);

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

        protected void PopulateList()
        {
            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            new GetItemsCommand<T>().DoActionInBackground(DroidState.Instance.BackpackPlannerState,
                command =>
                {
                    Logger.Debug($"Read {command.Items.Count} items...");

                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        Adapter.ListItems = command.Items;

                        UpdateView();
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

            new DeleteItemCommand<T>(item).DoActionInBackground(DroidState.Instance.BackpackPlannerState,
                command =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        Adapter.RemoveItem(item);

                        UpdateView();

                        SnackbarUtil.ShowUndoSnackbar(View, Resource.String.label_deleted_item, Android.Support.Design.Widget.Snackbar.LengthLong,
                            view => UndoDeleteItemEventHandler(view, command));
                    });
                }
            );
        }

        private void UndoDeleteItemEventHandler(View view, DeleteItemCommand<T> command)
        {
            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_deleted_item_undoing, false, true);

            command.UndoActionInBackground(DroidState.Instance.BackpackPlannerState,
                a =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        Adapter.AddItem(a.Item);

                        UpdateView();

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

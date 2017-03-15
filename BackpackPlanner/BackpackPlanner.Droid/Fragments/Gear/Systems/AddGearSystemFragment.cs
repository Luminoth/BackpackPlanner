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
using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Commands;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems
{
    public sealed class AddGearSystemFragment : AddItemFragment<GearSystem>
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(AddGearSystemFragment));

        protected override int LayoutResource => Resource.Layout.fragment_add_gear_system;

        protected override int TitleResource => Resource.String.title_add_gear_system;

        protected override int AddItemResource => Resource.Id.button_add_gear_system;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _gearSystemNameEditText;

        private TextView _noGearItemsTextView;
        private TextView _noGearItemsAddedTextView;
        private ListView _gearItemsListView;
        private Android.Support.Design.Widget.FloatingActionButton _addGearItemButton;

        private Android.Support.Design.Widget.TextInputLayout _gearSystemNoteEditText;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearSystemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_name);
            _gearSystemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_note);

            _noGearItemsTextView = View.FindViewById<TextView>(Resource.Id.no_gear_items);
            _noGearItemsAddedTextView = View.FindViewById<TextView>(Resource.Id.no_gear_items_added);

            _gearItemsListView = View.FindViewById<ListView>(Resource.Id.gear_items_list);

            _addGearItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_add_gear_item);
            _addGearItemButton.Click += AddGearItemButtonClickEventHandler;
        }

        public override void OnResume()
        {
            base.OnResume();

            new CountItemsCommand<GearItem>().DoActionInBackground(DroidState.Instance.BackpackPlannerState,
                command =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        bool hasItems = command.Count > 0;

                        ViewStates noItemsState = hasItems ? ViewStates.Gone : ViewStates.Visible;
                        ViewStates hasItemsState = hasItems ? ViewStates.Visible : ViewStates.Gone;

                        _noGearItemsTextView.Visibility = noItemsState;
                        _noGearItemsAddedTextView.Visibility =  hasItemsState;
                        _gearItemsListView.Visibility = hasItemsState;
                        _addGearItemButton.Visibility = hasItemsState;
                    });
                }
            );
        }

        private void AddGearItemButtonClickEventHandler(object sender, EventArgs args)
        {
            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            new GetItemsCommand<GearItem>().DoActionInBackground(DroidState.Instance.BackpackPlannerState,
                command =>
                {
                    Logger.Debug($"Read {command.Items.Count} items...");

                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        var items = (from x in command.Items select x.Name).ToArray();

                        var selectedItems = new bool[items.Length];
                        // TODO: set this based on what's already selected

// TODO: add filtering

// http://androidcodeon.blogspot.com/2016/04/custom-listview-alertdialog-with-filter.html
// http://stackoverflow.com/questions/25077998/custom-listview-in-an-alert-dialog-with-buttons
// http://stackoverflow.com/questions/6263464/how-to-filter-text-in-alert-dialog-box
// http://stackoverflow.com/questions/4040999/searching-within-alertdialog
// http://www.androidbegin.com/tutorial/android-search-listview-using-filter/

                        DialogUtil.ShowMultiChoiceAlert(Activity, Resource.String.label_add_gear_items, items, selectedItems,
                            (a, b) =>
                            {
                                selectedItems[b.Which] = b.IsChecked;
                                // TODO: update the adapter
                            });
                    });
                }
            );
        }

        protected override void OnDoDataExchange()
        {
            Item = new GearSystem(DroidState.Instance.BackpackPlannerState.Settings)
            {
                Name = _gearSystemNameEditText.EditText.Text,
                Note = _gearSystemNoteEditText.EditText.Text
            };
        }

        protected override bool OnValidate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_gearSystemNameEditText.EditText.Text)) {
                _gearSystemNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }
    }
}

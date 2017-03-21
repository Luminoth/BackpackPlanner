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
using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Commands;
using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear;
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

        protected override int AddItemResource => Resource.Id.fab_add_gear_system;

        protected override int ResetItemResource => Resource.Id.fab_reset_gear_system;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _gearSystemNameEditText;

        private TextView _noGearItemsTextView;
        private TextView _noGearItemsAddedTextView;
        private ListView _gearItemsListView;
        private Android.Support.Design.Widget.FloatingActionButton _addGearItemButton;

        private Android.Support.Design.Widget.TextInputLayout _gearSystemNoteEditText;
#endregion

#region Gear Items
        private GearItem[] _gearItems;

        private string[] _gearItemsNames;

        private bool[] _selectedGearItems;

        private GearSystemGearItemListAdapter _gearItemListAdapter;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearSystemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_name);
            _gearSystemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_note);

            _noGearItemsTextView = View.FindViewById<TextView>(Resource.Id.no_gear_items);
            _noGearItemsAddedTextView = View.FindViewById<TextView>(Resource.Id.no_gear_items_added);

            _gearItemListAdapter = new GearSystemGearItemListAdapter(this);

            _gearItemsListView = View.FindViewById<ListView>(Resource.Id.gear_items_list);
            _gearItemsListView.Adapter = _gearItemListAdapter;

            _addGearItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_add_gear_item);
            _addGearItemButton.Click += AddGearItemButtonClickEventHandler;
        }

        public override void OnResume()
        {
            base.OnResume();

            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            new GetItemsCommand<GearItem>().DoActionInBackground(DroidState.Instance.BackpackPlannerState,
                command =>
                {
                    Logger.Debug($"Read {command.Items.Count} items...");

                    _gearItems = command.Items.ToArray();
                    _gearItemsNames = (from x in _gearItems select x.Name).ToArray();
                    _selectedGearItems = new bool[_gearItems.Length];

                    OnItemsLoaded(progressDialog);
                }
            );
        }

        private void OnItemsLoaded(ProgressDialog progressDialog)
        {
            if(null == _gearItems) {
                return;
            }

            Activity.RunOnUiThread(() =>
            {
                progressDialog.Dismiss();

                UpdateView();
            });
        }

        private void UpdateView()
        {
            bool hasGearItems = null != _gearItems && _gearItems.Any();
            bool hasGearItemsAdded = _gearItemListAdapter.Count > 0;

            _noGearItemsTextView.Visibility = hasGearItems ? ViewStates.Gone : ViewStates.Visible;
            _noGearItemsAddedTextView.Visibility =  hasGearItemsAdded ? ViewStates.Gone : ViewStates.Visible;
            _gearItemsListView.Visibility = hasGearItemsAdded ? ViewStates.Visible : ViewStates.Gone;
            _addGearItemButton.Visibility = hasGearItems ? ViewStates.Visible : ViewStates.Gone;
        }

        private void AddGearItemButtonClickEventHandler(object sender, EventArgs args)
        {
// TODO: add filtering

            DialogUtil.ShowMultiChoiceAlert(Activity, Resource.String.label_add_gear_items, _gearItemsNames, _selectedGearItems,
                (a, b) =>
                {
                    UpdateGearItemList(b.Which, b.IsChecked);
                }
            );
        }

        private void UpdateGearItemList(int index, bool isSelected)
        {
            _selectedGearItems[index] = isSelected;

            GearItem gearItem = _gearItems[index];
            if(isSelected) {
                GearSystemGearItem gearSystemGearItem = new GearSystemGearItem(Item, gearItem, DroidState.Instance.BackpackPlannerState.Settings)
                {
                    Count = 1
                };
                _gearItemListAdapter.AddItem(gearItem, gearSystemGearItem);
                // TODO: sort (requires the item to extend Java.Lang.Object :\)
            } else {
                _gearItemListAdapter.RemoveItem(gearItem);
            }

            UpdateView();
        }

        protected override GearSystem CreateItem()
        {
            return new GearSystem(DroidState.Instance.BackpackPlannerState.Settings);
        }

        protected override void OnDoDataExchange()
        {
            Item.Name = _gearSystemNameEditText.EditText.Text;
            Item.Note = _gearSystemNoteEditText.EditText.Text;
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

        protected override void OnReset()
        {
// TODO
        }
    }
}

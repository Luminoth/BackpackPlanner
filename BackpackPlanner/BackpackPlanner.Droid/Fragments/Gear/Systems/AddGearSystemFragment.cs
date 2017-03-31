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
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear;
using EnergonSoftware.BackpackPlanner.Droid.Util;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems
{
    public sealed class AddGearSystemFragment : AddItemFragment<GearSystem>
    {
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

        private GearItemEntryListAdapter _gearItemListAdapter;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearSystemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_name);
            _gearSystemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_note);

            _noGearItemsTextView = View.FindViewById<TextView>(Resource.Id.no_gear_items);
            _noGearItemsAddedTextView = View.FindViewById<TextView>(Resource.Id.no_gear_items_added);

            _gearItemListAdapter = new GearItemEntryListAdapter(this);

            _gearItemsListView = View.FindViewById<ListView>(Resource.Id.gear_items_list);
            _gearItemsListView.Adapter = _gearItemListAdapter;

            _addGearItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_add_gear_item);
            _addGearItemButton.Click += AddGearItemButtonClickEventHandler;
        }

        public override void OnResume()
        {
            base.OnResume();

            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            Task.Run(async () =>
                {
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        _gearItems = await dbContext.GearItems.ToArrayAsync().ConfigureAwait(false);
                        _gearItemsNames = (from x in _gearItems select x.Name).ToArray();
                        _selectedGearItems = new bool[_gearItems.Length];
                    }

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
            DialogUtil.ShowMultiChoiceAlertWithSearch(Activity, Resource.String.label_add_gear_items, _gearItemsNames, _selectedGearItems,
                (a, b) =>
                {
                    _gearItemListAdapter.Filter.InvokeFilter(b.NewText, new FilterListener<GearItemEntry>(_gearItemListAdapter));
                },
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
                GearItemEntry gearItemEntry = new GearItemEntry(gearItem)
                {
                    Count = 1
                };
                _gearItemListAdapter.AddItem(gearItemEntry);
            } else {
                _gearItemListAdapter.RemoveItem(gearItem);
            }

            // TODO: this may be unnecessary
            _gearItemListAdapter.NotifyDataSetChanged();

            UpdateView();
        }

        protected override GearSystem CreateItem()
        {
            return new GearSystem();
        }

        protected override async Task AddItemAsync(DatabaseContext dbContext)
        {
            await dbContext.GearSystems.AddAsync(Item).ConfigureAwait(false);
        }

        protected override void OnDoDataExchange()
        {
            Item.Name = _gearSystemNameEditText.EditText.Text;
            Item.AddGearItems(_gearItemListAdapter.Items);
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

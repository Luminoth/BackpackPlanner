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
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.DAL.Gear;
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

        private GearSystemGearItemEntries _gearItemEntries;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _gearItemEntries = new GearSystemGearItemEntries(Item);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearSystemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_name);
            _gearSystemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_note);

            _noGearItemsTextView = View.FindViewById<TextView>(Resource.Id.no_gear_items);
            _noGearItemsAddedTextView = View.FindViewById<TextView>(Resource.Id.no_gear_items_added);

            _gearItemEntries.ItemListAdapter = new GearItemEntryListAdapter(this);

            _gearItemsListView = View.FindViewById<ListView>(Resource.Id.gear_items_list);
            _gearItemsListView.Adapter = _gearItemEntries.ItemListAdapter;

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
                        _gearItemEntries.Items = await dbContext.GearItems.ToArrayAsync().ConfigureAwait(false);
                    }

                    OnItemsLoaded(progressDialog);
                }
            );
        }

        private void OnItemsLoaded(ProgressDialog progressDialog)
        {
            if(null == _gearItemEntries.Items) {
                return;
            }

            Activity.RunOnUiThread(() =>
            {
                progressDialog.Dismiss();

                UpdateView();
            });
        }

        protected override void UpdateView()
        {
            bool hasGearItems = null != _gearItemEntries.Items && _gearItemEntries.Items.Any();
            bool hasGearItemsAdded = _gearItemEntries.EntryCount > 0;

            _noGearItemsTextView.Visibility = hasGearItems ? ViewStates.Gone : ViewStates.Visible;
            _noGearItemsAddedTextView.Visibility =  hasGearItemsAdded ? ViewStates.Gone : ViewStates.Visible;
            _gearItemsListView.Visibility = hasGearItemsAdded ? ViewStates.Visible : ViewStates.Gone;
            _addGearItemButton.Visibility = hasGearItems ? ViewStates.Visible : ViewStates.Gone;
        }

        private void AddGearItemButtonClickEventHandler(object sender, EventArgs args)
        {
            DialogUtil.ShowMultiChoiceAlertWithSearch(Activity, Resource.String.label_add_gear_items,
                _gearItemEntries.ItemNames, _gearItemEntries.SelectedItems,
                (a, b) =>
                {
                    _gearItemEntries.ItemListAdapter?.Filter.InvokeFilter(b.NewText, new FilterListener<GearItemEntry>(_gearItemEntries.ItemListAdapter));
                },
                (a, b) =>
                {
                    UpdateItemEntryList(Item, _gearItemEntries, b.Which, b.IsChecked);
                }
            );
        }

        protected override GearSystem CreateItem()
        {
            return new GearSystem();
        }

        protected override async Task AddItemAsync(DatabaseContext dbContext)
        {
            await dbContext.GearSystems.AddAsync(Item).ConfigureAwait(false);
        }

        protected override async Task OnDoDataExchange(DatabaseContext dbContext)
        {
            Item.Name = _gearSystemNameEditText.EditText.Text;
            Item.SetGearItems(dbContext, _gearItemEntries.ItemListAdapter?.Items);
            Item.Note = _gearSystemNoteEditText.EditText.Text;

            await Task.Delay(0).ConfigureAwait(false);
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

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
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Items;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.DAL.Gear;
using EnergonSoftware.BackpackPlanner.Droid.Util;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections
{
    public sealed class ViewGearCollectionFragment : ViewItemFragment<GearCollection>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_gear_collection;

        protected override int TitleResource => Resource.String.title_view_gear_collection;

        protected override int SaveItemResource => Resource.Id.fab_save_gear_collection;

        protected override int ResetItemResource => Resource.Id.fab_reset_gear_collection;

        protected override int DeleteItemResource => Resource.Id.fab_delete_gear_collection;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _gearCollectionNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearCollectionNoteEditText;
#endregion

        private GearCollectionGearSystemEntries _gearSystemEntries;
        private GearCollectionGearItemEntries _gearItemEntries;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _gearSystemEntries = new GearCollectionGearSystemEntries(Item);
            _gearItemEntries = new GearCollectionGearItemEntries(Item);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearCollectionNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_collection_name);
            _gearCollectionNameEditText.EditText.Text = Item.Name;

            _gearCollectionNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_gear_collection_note);
            _gearCollectionNoteEditText.EditText.Text = Item.Note;
        }

        public override void OnResume()
        {
            base.OnResume();

            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            Task.Run(async () =>
                {
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        _gearSystemEntries.Items = await dbContext.GearSystems.ToArrayAsync().ConfigureAwait(false);
                        _gearItemEntries.Items = await dbContext.GearItems.ToArrayAsync().ConfigureAwait(false);
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        SetItemEntryList(Item, _gearSystemEntries);
                        SetItemEntryList(Item, _gearItemEntries);

                        progressDialog.Dismiss();

                        UpdateView();
                    });
                }
            );
        }

        protected override void UpdateView()
        {
        }

        private void AddGearSystemButtonClickEventHandler(object sender, EventArgs args)
        {
            DialogUtil.ShowMultiChoiceAlertWithSearch(Activity, Resource.String.label_add_gear_systems,
                _gearSystemEntries.ItemNames, _gearSystemEntries.SelectedItems,
                (a, b) =>
                {
                    _gearSystemEntries.ItemListAdapter?.Filter.InvokeFilter(b.NewText, new FilterListener<GearSystemEntry>(_gearSystemEntries.ItemListAdapter));
                },
                (a, b) =>
                {
                    UpdateItemEntryList(Item, _gearSystemEntries, b.Which, b.IsChecked);
                }
            );
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

        protected override async Task OnDoDataExchange(DatabaseContext dbContext)
        {
            Item.Name = _gearCollectionNameEditText.EditText.Text;
            Item.SetGearSystems(dbContext, _gearSystemEntries.ItemListAdapter?.Items);
            Item.SetGearItems(dbContext, _gearItemEntries.ItemListAdapter?.Items);
            Item.Note = _gearCollectionNoteEditText.EditText.Text;

            await Task.Delay(0).ConfigureAwait(false);
        }

        protected override bool OnValidate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_gearCollectionNameEditText.EditText.Text)) {
                _gearCollectionNameEditText.EditText.Error = "A name is required!";
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

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

using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.DAL.Gear;
using EnergonSoftware.BackpackPlanner.Droid.Util;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections
{
    public sealed class ViewGearCollectionFragment : ViewItemFragment<GearCollection>
    {
        protected override int LayoutResource => Resource.Layout.fragment_view_gear_collection;

        protected override int TitleResource => Resource.String.title_view_gear_collection;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _gearCollectionNameEditText;

        private GearCollectionGearSystemEntries.GearCollectionGearSystemEntryViewHolder _gearSystemEntryViewHolder;
        private GearCollectionGearItemEntries.GearCollectionGearItemEntryViewHolder _gearItemEntryViewHolder;

        private Android.Support.Design.Widget.TextInputLayout _gearCollectionNoteEditText;
#endregion

        private GearCollectionGearSystemEntries _gearSystemEntries;
        private GearCollectionGearItemEntries _gearItemEntries;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _gearSystemEntries = new GearCollectionGearSystemEntries(Item);
            _gearSystemEntryViewHolder = new GearCollectionGearSystemEntries.GearCollectionGearSystemEntryViewHolder(this, Item, _gearSystemEntries);

            _gearItemEntries = new GearCollectionGearItemEntries(Item);
            _gearItemEntryViewHolder = new GearCollectionGearItemEntries.GearCollectionGearItemEntryViewHolder(this, Item, _gearItemEntries);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearCollectionNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_collection_name);
            _gearCollectionNameEditText.EditText.Text = Item.Name;

            _gearCollectionNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_collection_note);
            _gearCollectionNoteEditText.EditText.Text = Item.Note;

            _gearSystemEntries.ItemListAdapter = new GearSystemEntryListAdapter(this);
            _gearSystemEntryViewHolder.OnViewCreated(view, _gearSystemEntries.ItemListAdapter);

            _gearItemEntries.ItemListAdapter = new GearItemEntryListAdapter(this);
            _gearItemEntryViewHolder.OnViewCreated(view, _gearItemEntries.ItemListAdapter);
        }

        public override void OnResume()
        {
            base.OnResume();

            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            Task.Run(async () =>
                {
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        _gearSystemEntries.Items = await dbContext.GearSystems.ToListAsync().ConfigureAwait(false);
                        _gearItemEntries.Items = await dbContext.GearItems.ToListAsync().ConfigureAwait(false);
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
            _gearSystemEntryViewHolder.UpdateView();
            _gearItemEntryViewHolder.UpdateView();
        }

        protected override async Task DoDataExchange(DatabaseContext dbContext)
        {
            Item.Name = _gearCollectionNameEditText.EditText.Text;
            Item.SetGearSystems(dbContext, _gearSystemEntries.ItemListAdapter?.Items);
            Item.SetGearItems(dbContext, _gearItemEntries.ItemListAdapter?.Items);
            Item.Note = _gearCollectionNoteEditText.EditText.Text;

            await Task.Delay(0).ConfigureAwait(false);
        }

        protected override bool Validate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_gearCollectionNameEditText.EditText.Text)) {
                _gearCollectionNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        protected override void Reset()
        {
// TODO
        }
    }
}

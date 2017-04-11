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

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _gearSystemNameEditText;

        private GearSystemGearItemEntries.GearSystemGearItemEntryViewHolder _gearItemEntryViewHolder;

        private Android.Support.Design.Widget.TextInputLayout _gearSystemNoteEditText;
#endregion

        private GearSystemGearItemEntries _gearItemEntries;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _gearItemEntries = new GearSystemGearItemEntries(Item);
            _gearItemEntryViewHolder = new GearSystemGearItemEntries.GearSystemGearItemEntryViewHolder(this, Item, _gearItemEntries);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearSystemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_name);
            _gearSystemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_system_note);

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
                        _gearItemEntries.Items = await dbContext.GearItems.ToListAsync().ConfigureAwait(false);
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        UpdateView();
                    });
                }
            );
        }

        protected override void UpdateView()
        {
            _gearItemEntryViewHolder.UpdateView();
        }

        protected override GearSystem CreateItem()
        {
            return new GearSystem();
        }

        protected override async Task AddItemAsync(DatabaseContext dbContext)
        {
            await dbContext.GearSystems.AddAsync(Item).ConfigureAwait(false);
        }

        protected override async Task DoDataExchange(DatabaseContext dbContext)
        {
            Item.Name = _gearSystemNameEditText.EditText.Text;
            Item.SetGearItems(dbContext, _gearItemEntries.ItemListAdapter?.Items);
            Item.Note = _gearSystemNoteEditText.EditText.Text;

            await Task.Delay(0).ConfigureAwait(false);
        }

        protected override bool Validate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_gearSystemNameEditText.EditText.Text)) {
                _gearSystemNameEditText.EditText.Error = "A name is required!";
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

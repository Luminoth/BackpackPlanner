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
using System.Globalization;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Trips.Plans;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Items;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals;
using EnergonSoftware.BackpackPlanner.Droid.DAL.Gear;
using EnergonSoftware.BackpackPlanner.Droid.Fragments.Util;
using EnergonSoftware.BackpackPlanner.Droid.Util;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public sealed class ViewTripPlanFragment : ViewItemFragment<TripPlan>
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(ViewTripPlanFragment));

        protected override int LayoutResource => Resource.Layout.fragment_view_trip_plan;

        protected override int TitleResource => Resource.String.title_view_trip_plan;

        protected override int SaveItemResource => Resource.Id.fab_save_trip_plan;

        protected override int ResetItemResource => Resource.Id.fab_reset_trip_plan;

        protected override int DeleteItemResource => Resource.Id.fab_delete_trip_plan;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _tripPlanNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _tripPlanStartDateText;
        private Android.Support.Design.Widget.TextInputLayout _tripPlanEndDateText;

        private TripPlanGearCollectionEntries.TripPlanGearCollectionEntryViewHolder _gearCollectionEntryViewHolder;
        private TripPlanGearSystemEntries.TripPlanGearSystemEntryViewHolder _gearSystemEntryViewHolder;
        private TripPlanGearItemEntries.TripPlanGearItemEntryViewHolder _gearItemEntryViewHolder;
        private TripPlanMealEntries.TripPlanMealEntryViewHolder _mealEntryViewHolder;

        private Android.Support.Design.Widget.TextInputLayout _tripPlanNoteEditText;
#endregion

        private TripPlanGearCollectionEntries _gearCollectionEntries;
        private TripPlanGearSystemEntries _gearSystemEntries;
        private TripPlanGearItemEntries _gearItemEntries;
        private TripPlanMealEntries _mealEntries;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _gearCollectionEntries = new TripPlanGearCollectionEntries(Item);
            _gearCollectionEntryViewHolder = new TripPlanGearCollectionEntries.TripPlanGearCollectionEntryViewHolder(this, Item, _gearCollectionEntries);

            _gearSystemEntries = new TripPlanGearSystemEntries(Item);
            _gearSystemEntryViewHolder = new TripPlanGearSystemEntries.TripPlanGearSystemEntryViewHolder(this, Item, _gearSystemEntries);

            _gearItemEntries = new TripPlanGearItemEntries(Item);
            _gearItemEntryViewHolder = new TripPlanGearItemEntries.TripPlanGearItemEntryViewHolder(this, Item, _gearItemEntries);

            _mealEntries = new TripPlanMealEntries(Item);
            _mealEntryViewHolder = new TripPlanMealEntries.TripPlanMealEntryViewHolder(this, Item, _mealEntries);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _tripPlanNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_trip_plan_name);
            _tripPlanNameEditText.EditText.Text = Item.Name;

            _tripPlanStartDateText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_trip_plan_startdate);
            _tripPlanStartDateText.EditText.Text = Item.StartDate.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
            _tripPlanStartDateText.EditText.Click += (sender, args) => {
                DateTime dateTime = DateTime.Now;
                try {
                    dateTime = Convert.ToDateTime(_tripPlanStartDateText.EditText.Text);
                } catch(FormatException) {
                }

                DatePickerFragment picker = new DatePickerFragment(dateTime);
                picker.DateSetEvent += (s, a) => {
                    _tripPlanStartDateText.EditText.Text = a.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                };
                picker.Show(Activity.SupportFragmentManager, null);
            };

            _tripPlanEndDateText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_trip_plan_enddate);
            _tripPlanEndDateText.EditText.Text = Item.EndDate.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
            _tripPlanEndDateText.EditText.Click += (sender, args) => {
                DateTime dateTime = DateTime.Now;
                try {
                    dateTime = Convert.ToDateTime(_tripPlanEndDateText.EditText.Text);
                } catch(FormatException) {
                }

                DatePickerFragment picker = new DatePickerFragment(dateTime);
                picker.DateSetEvent += (s, a) => {
                    _tripPlanEndDateText.EditText.Text = a.Date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                };
                picker.Show(Activity.SupportFragmentManager, null);
            };

            _gearCollectionEntries.ItemListAdapter = new GearCollectionEntryListAdapter(this);
            _gearCollectionEntryViewHolder.OnViewCreated(view, _gearCollectionEntries.ItemListAdapter);

            _gearSystemEntries.ItemListAdapter = new GearSystemEntryListAdapter(this);
            _gearSystemEntryViewHolder.OnViewCreated(view, _gearSystemEntries.ItemListAdapter);

            _gearItemEntries.ItemListAdapter = new GearItemEntryListAdapter(this);
            _gearItemEntryViewHolder.OnViewCreated(view, _gearItemEntries.ItemListAdapter);

            _mealEntries.ItemListAdapter = new MealEntryListAdapter(this);
            _mealEntryViewHolder.OnViewCreated(view, _mealEntries.ItemListAdapter);

            _tripPlanNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_trip_plan_note);
            _tripPlanNoteEditText.EditText.Text = Item.Note;
        }

        public override void OnResume()
        {
            base.OnResume();

            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_loading_items, false, true);

            Task.Run(async () =>
                {
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        _gearCollectionEntries.Items = await dbContext.GearCollections.ToListAsync().ConfigureAwait(false);
                        _gearSystemEntries.Items = await dbContext.GearSystems.ToListAsync().ConfigureAwait(false);
                        _gearItemEntries.Items = await dbContext.GearItems.ToListAsync().ConfigureAwait(false);
                        _mealEntries.Items = await dbContext.Meals.ToListAsync().ConfigureAwait(false);
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        SetItemEntryList(Item, _gearCollectionEntries);
                        SetItemEntryList(Item, _gearSystemEntries);
                        SetItemEntryList(Item, _gearItemEntries);
                        SetItemEntryList(Item, _mealEntries);

                        progressDialog.Dismiss();

                        UpdateView();
                    });
                }
            );
        }

        protected override void UpdateView()
        {
            _gearCollectionEntryViewHolder.UpdateView();
            _gearSystemEntryViewHolder.UpdateView();
            _gearItemEntryViewHolder.UpdateView();
            _mealEntryViewHolder.UpdateView();
        }

        protected override async Task DoDataExchange(DatabaseContext dbContext)
        {
            Item.Name = _tripPlanNameEditText.EditText.Text;

            try {
                Item.StartDate = Convert.ToDateTime(_tripPlanStartDateText.EditText.Text);
            } catch(FormatException) {
                Logger.Error("Error parsing start date!");
            }

            try {
                Item.EndDate = Convert.ToDateTime(_tripPlanEndDateText.EditText.Text);
            } catch(FormatException) {
                Logger.Error("Error parsing end date!");
            }

            Item.SetGearCollections(dbContext, _gearCollectionEntries.ItemListAdapter?.Items);
            Item.SetGearSystems(dbContext, _gearSystemEntries.ItemListAdapter?.Items);
            Item.SetGearItems(dbContext, _gearItemEntries.ItemListAdapter?.Items);
            Item.SetMeals(dbContext, _mealEntries.ItemListAdapter?.Items);

            Item.Note = _tripPlanNoteEditText.EditText.Text;

            await Task.Delay(0).ConfigureAwait(false);
        }

        protected override bool Validate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_tripPlanNameEditText.EditText.Text)) {
                _tripPlanNameEditText.EditText.Error = "A name is required!";
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

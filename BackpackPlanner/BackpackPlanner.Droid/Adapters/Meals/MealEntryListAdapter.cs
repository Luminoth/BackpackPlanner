/*
   Copyright 2017 Shane Lillie

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

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.DAL.Models.Meals;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;
using EnergonSoftware.BackpackPlanner.Units.Units;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals
{
    public sealed class MealEntryListAdapter<T> : BaseModelEntryListViewAdapter<MealEntry<T>, T, Meal>
        where T: BaseModel<T>, new()
    {
        private sealed class MealEntryViewHolder : BaseModelEntryViewHolder<MealEntry<T>, T, Meal>
        {
            private readonly TextView _textViewName;
            private readonly TextView _textViewTotalWeight;
            private readonly Android.Support.Design.Widget.TextInputLayout _editTextQuantity;

            public MealEntryViewHolder(View view, BaseActivity activity)
                : base(activity)
            {
                _textViewName = view.FindViewById<TextView>(Resource.Id.view_meal_name);
                _textViewTotalWeight = view.FindViewById<TextView>(Resource.Id.view_meal_total_weight);

                _editTextQuantity = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.view_meal_quantity);
                _editTextQuantity.EditText.AfterTextChanged += (sender, args) =>
                {
                    UpdateTotalWeight(Item);
                    NotifyPropertyChanged("Quantity");
                };
            }

            public override void UpdateView(MealEntry<T> item)
            {
                base.UpdateView(item);

                _textViewName.Text = item.Model.Name;
                _editTextQuantity.EditText.Text = item.Count.ToString();

                UpdateTotalWeight(item);
            }

            private void UpdateTotalWeight(MealEntry<T> item)
            {
                item.Count = string.IsNullOrWhiteSpace(_editTextQuantity.EditText.Text)
                    ? 0
                    : Convert.ToInt32(_editTextQuantity.EditText.Text);

                int totalWeightInUnits = (int)item.GetTotalWeightInUnits(BaseActivity.BackpackPlannerState.Settings);
                _textViewTotalWeight.Text = Java.Lang.String.Format(BaseActivity.Resources.GetString(Resource.String.label_view_gear_item_total_weight),
                    totalWeightInUnits, BaseActivity.BackpackPlannerState.Settings.Units.GetSmallWeightString(totalWeightInUnits != 1)
                );
            }
        }

        public override int LayoutResource => Resource.Layout.view_meal_entry;

        public MealEntryListAdapter(BaseActivity activity)
            : base(activity)
        {
        }

        public MealEntryListAdapter(BaseActivity activity, MealEntry<T>[] items)
            : base(activity, items)
        {
        }

        protected override BaseViewHolder<MealEntry<T>> CreateViewHolder(View view)
        {
            MealEntryViewHolder viewHolder = new MealEntryViewHolder(view, BaseActivity);
            viewHolder.PropertyChanged += (sender, args) =>
            {
                NotifyPropertyChanged(args.PropertyName);
            };
            return viewHolder;
        }
    }
}

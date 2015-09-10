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

using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Adapters.Meals;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals
{
    public class MealsFragment : RecyclerFragment
    {
        public override int LayoutResource => Resource.Layout.fragment_meals;

        public override int TitleResource => Resource.String.title_meals;

        private MealListAdapter _adapter;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            InitLayout(view, Resource.Id.meals_layout);
            //Layout.SetAdapter(_adapter);

            TextView noMealsTextView = view.FindViewById<TextView>(Resource.Id.no_meals);

            Android.Support.Design.Widget.FloatingActionButton addMealButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_add_meal);
            addMealButton.Click += (sender, args) => {
                // TODO
            };
        }
    }
}

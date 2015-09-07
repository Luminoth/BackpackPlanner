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
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public class GearItemsFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_gear_items, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            TextView noGearItemsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_items);
            // TODO

            ViewGroup gearItemsLayout = view.FindViewById<LinearLayout>(Resource.Id.gear_items_layout);
            gearItemsLayout.Visibility = ViewStates.Gone;

            FloatingActionButton addGearItemButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_add_gear_item);
            addGearItemButton.Click += (sender, args) => {
                Android.Support.V4.App.FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.frame_content, new AddGearItemFragment());
                fragmentTransaction.AddToBackStack(null);
                fragmentTransaction.Commit();
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(Resource.String.title_gear_items);
        }
    }
}

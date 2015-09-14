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

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data listing fragments
    /// </summary>
    public abstract class ListItemsFragment<T> : RecyclerFragment
    {
        protected abstract int NoItemsResource { get; }

        protected abstract int SortItemsResource { get; }

        protected abstract int ItemCount { get; }

        protected abstract int AddItemResource { get; }

        protected abstract Android.Support.V4.App.Fragment CreateAddItemFragment();

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Android.Support.Design.Widget.FloatingActionButton addItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(AddItemResource);
            addItemButton.Click += (sender, args) => {
                TransitionToFragment(Resource.Id.frame_content, CreateAddItemFragment(), null);
            };

            if(ItemCount > 0) {
                TextView noItemsTextView = view.FindViewById<TextView>(NoItemsResource);
                noItemsTextView.Visibility = ViewStates.Gone;

                Spinner sortItemsSpinner = view.FindViewById<Spinner>(SortItemsResource);
                if(null != sortItemsSpinner) {
                    sortItemsSpinner.Visibility = ViewStates.Visible;
                }

                Layout.Visibility = ViewStates.Visible;
            }
        }
    }
}

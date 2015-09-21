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

using System.Collections.Generic;

using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Adapters;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data listing fragments
    /// </summary>
    public abstract class ListItemsFragment<T> : RecyclerFragment
    {
        protected abstract int NoItemsResource { get; }

        protected abstract int SortItemsResource { get; }

        protected abstract int AddItemResource { get; }

        protected readonly List<T> ListItems = new List<T>();

        protected BaseListAdapter<T> Adapter { get; private set; } 

#region Controls
        protected Spinner SortItemsSpinner { get; private set; }
#endregion

        protected abstract Android.Support.V4.App.Fragment CreateAddItemFragment();

        protected abstract BaseListAdapter<T> CreateAdapter();

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Android.Support.Design.Widget.FloatingActionButton addItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(AddItemResource);
            addItemButton.Click += (sender, args) => {
                TransitionToFragment(Resource.Id.frame_content, CreateAddItemFragment(), null);
            };

            if(ListItems.Count > 0) {
                TextView noItemsTextView = view.FindViewById<TextView>(NoItemsResource);
                noItemsTextView.Visibility = ViewStates.Gone;

                Adapter = CreateAdapter();
                Layout.SetAdapter(Adapter);

                SortItemsSpinner = view.FindViewById<Spinner>(SortItemsResource);
                if(null != SortItemsSpinner) {
                    SortItemsSpinner.Visibility = ViewStates.Visible;
                    SortItemsSpinner.ItemSelected += Adapter.SortByItemSelectedEventHander;
                }

                Layout.Visibility = ViewStates.Visible;
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            FilterView.QueryTextChange += Adapter.FilterItems;
        }
    }
}

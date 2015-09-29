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
using EnergonSoftware.BackpackPlanner.Logging;
using EnergonSoftware.BackpackPlanner.Models;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data listing fragments
    /// </summary>
    public abstract class ListItemsFragment<T> : RecyclerFragment where T: DatabaseItem
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(ListItemsFragment<T>));

        protected abstract int NoItemsResource { get; }

        protected abstract int SortItemsResource { get; }

        protected abstract int AddItemResource { get; }

        public int ItemCount => ListItems.Count;

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

            Adapter = CreateAdapter();
            Layout.SetAdapter(Adapter);

            Android.Support.Design.Widget.FloatingActionButton addItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(AddItemResource);
            addItemButton.Click += (sender, args) => {
                TransitionToFragment(Resource.Id.frame_content, CreateAddItemFragment(), null);
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            ListItems.AddRange(DatabaseItem.GetItemsAsync<T>().Result);
            Logger.Debug($"Read {ItemCount} items...");
            Adapter.ListItems = ListItems;

            if(ListItems.Count > 0) {
                TextView noItemsTextView = View.FindViewById<TextView>(NoItemsResource);
                noItemsTextView.Visibility = ViewStates.Gone;

                SortItemsSpinner = View.FindViewById<Spinner>(SortItemsResource);
                if(null != SortItemsSpinner) {
                    SortItemsSpinner.Visibility = ViewStates.Visible;
                    SortItemsSpinner.ItemSelected += Adapter.SortByItemSelectedEventHander;
                }

                Layout.Visibility = ViewStates.Visible;
            }
        }

        public override void OnPause()
        {
            base.OnPause();

            Logger.Debug("Clearing item list for pause...");
            ListItems.Clear();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            FilterView.QueryTextChange += Adapter.FilterItems;
        }
    }
}

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
using Android.Runtime;
using Android.Util;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for fragment creation
    /// </summary>
    public abstract class BaseFragment : Android.Support.V4.App.Fragment
    {
        protected abstract int LayoutResource { get; }

        protected abstract int TitleResource { get; }

        protected abstract bool HasSearchView { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            return inflater.Inflate(LayoutResource, container, false);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            inflater.Inflate(HasSearchView ? Resource.Menu.options_menu : Resource.Menu.options_menu_nosearch, menu);

            IMenuItem searchItem = menu.FindItem(Resource.Id.action_search);
            if(null == searchItem) {
                return;
            }

            View actionView = Android.Support.V4.View.MenuItemCompat.GetActionView(searchItem);
            Android.Support.V7.Widget.SearchView searchView = actionView.JavaCast<Android.Support.V7.Widget.SearchView>();
            searchView.QueryHint = Resources.GetString(Resource.String.search_hint);

// http://stackoverflow.com/questions/30398247/how-to-filter-a-recyclerview-with-a-searchview

            searchView.QueryTextChange += (sender, args) => {
Log.Debug(MainActivity.LogTag, "changed");
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(TitleResource);
        }

        public void TransitionToFragment(int frameResId, Android.Support.V4.App.Fragment fragment, string tags)
        {
            FragmentTransitionUtil.StackTransition(Activity, FragmentManager.BeginTransaction(), frameResId, fragment, tags);
        }
    }
}

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

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the recycler fragments
    /// </summary>
    public abstract class RecyclerFragment : BaseFragment
    {
        protected abstract int ListLayoutResource { get; }

        public Android.Support.V7.Widget.RecyclerView Layout { get; protected set; }

        protected Android.Support.V7.Widget.RecyclerView.LayoutManager LayoutManager { get; set; }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Layout = view.FindViewById<Android.Support.V7.Widget.RecyclerView>(ListLayoutResource);

            LayoutManager = new Android.Support.V7.Widget.LinearLayoutManager(Activity);
            Layout.SetLayoutManager(LayoutManager);
        }

        protected void AttachSwipeHandler(Android.Support.V7.Widget.Helper.ItemTouchHelper.SimpleCallback callback)
        {
            Android.Support.V7.Widget.Helper.ItemTouchHelper touchHelper = new Android.Support.V7.Widget.Helper.ItemTouchHelper(callback);
            touchHelper.AttachToRecyclerView(Layout);
        }
    }
}

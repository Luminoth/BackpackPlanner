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

using Android.Views;

using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;

namespace EnergonSoftware.BackpackPlanner.Droid.Views
{
    public abstract class BaseRecyclerViewHolder<T> : Android.Support.V7.Widget.RecyclerView.ViewHolder, IViewHolder<T>
        where T: class
    {
        public BaseActivity Activity => Adapter.Fragment.BaseActivity;

        protected RecyclerFragment Fragment => Adapter.Fragment;

        protected BaseRecyclerListAdapter<T> Adapter { get; }

        public T Item { get; private set; }

        public virtual void UpdateView(T item)
        {
            Item = item;
        }

        protected BaseRecyclerViewHolder(View view, BaseRecyclerListAdapter<T> adapter)
            : base(view)
        {
            Adapter = adapter;
        }
    }
}

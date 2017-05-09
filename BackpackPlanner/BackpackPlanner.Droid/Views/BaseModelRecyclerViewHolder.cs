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

using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.Fragments;

namespace EnergonSoftware.BackpackPlanner.Droid.Views
{
    public abstract class BaseModelRecyclerViewHolder<T> : BaseRecyclerViewHolder<T>, Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
        where T: BaseModel<T>, IBackpackPlannerItem, new()
    {
        protected ListItemsFragment<T> ListItemsFragment => (ListItemsFragment<T>)Fragment;

        protected abstract int ToolbarResourceId { get; }

        protected abstract int MenuResourceId { get; }

        protected abstract int DeleteActionResourceId { get; }

        private Android.Support.V7.Widget.Toolbar _toolbar;

        protected abstract ViewItemFragment<T> CreateViewItemFragment();

        protected BaseModelRecyclerViewHolder(View view, BaseRecyclerListAdapter<T> adapter)
            : base(view, adapter)
        {
            InitToolbar();

            view.Click += (sender, args) =>
            {
                ViewItemFragment<T> viewItemFragment = CreateViewItemFragment();
                viewItemFragment.SetItem(Item);

                Fragment.TransitionToFragment(Resource.Id.frame_content, viewItemFragment, null);
            };
        }

        private void InitToolbar()
        {
            _toolbar = ItemView.FindViewById<Android.Support.V7.Widget.Toolbar>(ToolbarResourceId);
            _toolbar.InflateMenu(MenuResourceId);
            _toolbar.SetOnMenuItemClickListener(this);
        }

        public override void UpdateView(T item)
        {
            base.UpdateView(item);

            _toolbar.Title = Item.Name;
        }

        public virtual bool OnMenuItemClick(IMenuItem menuItem)
        {
            if(DeleteActionResourceId == menuItem.ItemId) {
                ListItemsFragment.DeleteItem(Item);
                return true;
            }
            return false;
        }
    }
}
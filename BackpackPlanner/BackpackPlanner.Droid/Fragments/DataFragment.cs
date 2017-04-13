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

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data entry fragments
    /// </summary>
    public abstract class DataFragment<T> : BaseFragment
        where T: BaseModel<T>, new()
    {
        public abstract T Item { get; protected set; }

        private BaseModelViewHolder<T> _viewHolder;

        protected abstract BaseModelViewHolder<T> CreateViewHolder(BaseActivity activity, View view);

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _viewHolder = CreateViewHolder(BaseActivity, view);
        }

        protected override void UpdateView()
        {
            base.UpdateView();

            _viewHolder.UpdateView(Item);
        }

        protected virtual bool Validate()
        {
            return _viewHolder.Validate();
        }

        protected virtual void DoDataExchange(DatabaseContext dbContext)
        {
            _viewHolder.DoDataExchange(Item, dbContext);
        }
    }
}

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

using System.Threading.Tasks;

using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;

using Microsoft.EntityFrameworkCore;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public abstract class ViewItemFragment<T> : DataFragment<T> where T: BaseModel<T>, new()
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(ViewItemFragment<T>));

        protected override bool HasSearchView => false;

        protected override bool CanExport => false;

        private T _item, _workingItem;

        public override T Item
        {
            get => _workingItem;

            protected set
            {
                _item = value;
                _workingItem = _item?.DeepCopy();
            }
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Android.Support.Design.Widget.FloatingActionButton saveItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_save);
            saveItemButton.Click += (sender, args) =>
            {
                Save(() =>
                    {
                        Activity.SupportFragmentManager.PopBackStack();
                    }
                );
            };

            Android.Support.Design.Widget.FloatingActionButton resetItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_reset);
            resetItemButton.Click += (sender, args) =>
            {
                Reset();
            };

            Android.Support.Design.Widget.FloatingActionButton deleteItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_delete);
            deleteItemButton.Click += (sender, args) =>
            {
            };
        }

        protected override async Task<bool> DoSave(DatabaseContext dbContext)
        {
            if(!await base.DoSave(dbContext).ConfigureAwait(false)) {
                return false;
            }

            Logger.Info($"Saving {typeof(T)}...");
            dbContext.Entry(Item).State = EntityState.Modified;
            return await dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;    // TODO: should be >= ?
        }

        protected override void Reset()
        {
            _workingItem = _item?.DeepCopy();

            base.Reset();
        }

        protected ViewItemFragment(T item)
        {
            Item = item;
        }
    }
}

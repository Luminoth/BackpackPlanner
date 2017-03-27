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

using Android.App;
using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public abstract class ViewItemFragment<T> : DataFragment where T: BaseModel
    {
        protected abstract int SaveItemResource { get; }

        protected abstract int ResetItemResource { get; }

        protected abstract int DeleteItemResource { get; }

        protected override bool HasSearchView => false;

        protected override bool CanExport => false;

        public T Item { get; set; }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Android.Support.Design.Widget.FloatingActionButton saveItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(SaveItemResource);
            saveItemButton.Click += (sender, args) => {
                if(!DoDataExchange()) {
                    return;
                }

                ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_saving_item, false, true);

                Task.Run(async () =>
                    {
                        using(DatabaseContext dbContext = DroidState.Instance.BackpackPlannerState.DatabaseState.CreateContext()) {
                            await dbContext.SaveChangesAsync().ConfigureAwait(false);
                        }

                        Activity.RunOnUiThread(() =>
                        {
                            progressDialog.Dismiss();

                            SnackbarUtil.ShowSnackbar(View, Resource.String.label_saved_item, Android.Support.Design.Widget.Snackbar.LengthShort);

                            Activity.SupportFragmentManager.PopBackStack();
                        });
                    }
                );
            };

            Android.Support.Design.Widget.FloatingActionButton resetItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(ResetItemResource);
            resetItemButton.Click += (sender, args) => {
                OnReset();
            };

            Android.Support.Design.Widget.FloatingActionButton deleteItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(DeleteItemResource);
            deleteItemButton.Click += (sender, args) => {
            };
        }
    }
}

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

using System;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public abstract class AddItemFragment<T> : DataFragment<T> where T: BaseModel
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(AddItemFragment<T>));

        protected override bool HasSearchView => false;

        protected override bool CanExport => false;

        public T Item { get; private set; }

        protected abstract T CreateItem();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Item = CreateItem();
        }

        protected abstract Task AddItemAsync(DatabaseContext dbContext);

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Android.Support.Design.Widget.FloatingActionButton addItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_add);
            addItemButton.Click += (sender, args) => {
                ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_adding_item, false, true);

                Task.Run(async () =>
                    {
                        int count;
                        using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                            if(!await DoDataExchange(dbContext).ConfigureAwait(false)) {
                                return;
                            }

                            try {
                                Logger.Info($"Adding {typeof(T)}...");
                                await AddItemAsync(dbContext).ConfigureAwait(false);
                                count = await dbContext.SaveChangesAsync().ConfigureAwait(false);
                            } catch(Exception e) {
                                Logger.Error($"Error adding {typeof(T)}: {e.Message}", e);
                                count = -1;
                            }
                        }

                        Activity.RunOnUiThread(() =>
                        {
                            progressDialog.Dismiss();

                            if(count < 1) {
                                DialogUtil.ShowOkAlert(Activity, Resource.String.message_error_adding_item, Resource.String.title_error_adding_item);
                                return;
                            }

                            SnackbarUtil.ShowSnackbar(View, Resource.String.label_added_item, Android.Support.Design.Widget.Snackbar.LengthShort);

                            Activity.SupportFragmentManager.PopBackStack();
                        });
                    }
                );
            };

            Android.Support.Design.Widget.FloatingActionButton resetItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.fab_reset);
            resetItemButton.Click += (sender, args) => {
                OnReset();
            };
        }
    }
}

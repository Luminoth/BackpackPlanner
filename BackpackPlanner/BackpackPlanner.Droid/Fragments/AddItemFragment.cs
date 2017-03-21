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

using Android.App;
using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Commands;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Models;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public abstract class AddItemFragment<T> : DataFragment where T: DatabaseItem
    {
        protected abstract int AddItemResource { get; }

        protected abstract int ResetItemResource { get; }

        protected override bool HasSearchView => false;

        protected override bool CanExport => false;

        public T Item { get; private set; }

        protected abstract T CreateItem();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Item = CreateItem();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Android.Support.Design.Widget.FloatingActionButton addItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(AddItemResource);
            addItemButton.Click += (sender, args) => {
                if(!DoDataExchange()) {
                    return;
                }

                ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_adding_item, false, true);

                new AddItemCommand<T>(Item).DoActionInBackground(DroidState.Instance.BackpackPlannerState,
                    command =>
                    {
                        Activity.RunOnUiThread(() =>
                        {
                            progressDialog.Dismiss();

                            SnackbarUtil.ShowSnackbar(View, Resource.String.label_added_item, Android.Support.Design.Widget.Snackbar.LengthShort);

                            Activity.SupportFragmentManager.PopBackStack();
                        });
                    }
                );
            };

            Android.Support.Design.Widget.FloatingActionButton resetItemButton = view.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(ResetItemResource);
            resetItemButton.Click += (sender, args) => {
                OnReset();
            };
        }
    }
}

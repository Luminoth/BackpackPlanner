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
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Droid.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data entry fragments
    /// </summary>
    public abstract class DataFragment<T> : BaseFragment
        where T: BaseModel<T>, new()
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DataFragment<T>));

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

        protected bool Save(Action onSuccess=null, Action onFailure=null)
        {
            if(!Validate()) {
                return false;
            }

            ProgressDialog progressDialog = DialogUtil.ShowProgressDialog(Activity, Resource.String.label_adding_item, false, true);

            Task.Run(async () =>
                {
                    bool success;
                    using(DatabaseContext dbContext = BaseActivity.BackpackPlannerState.DatabaseState.CreateContext()) {
                        try {
                            success = await DoSave(dbContext).ConfigureAwait(false);
                        } catch(Exception e) {
                            Logger.Error($"Error saving {typeof(T)}: {e.Message}", e);
                            success = false;
                        }
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        progressDialog.Dismiss();

                        if(!success) {
                            DialogUtil.ShowOkAlert(Activity, Resource.String.message_error_adding_item, Resource.String.title_error_adding_item);
                            onFailure?.Invoke();
                            return;
                        }

                        IsDirty = false;

                        SnackbarUtil.ShowSnackbar(View, Resource.String.label_added_item, Android.Support.Design.Widget.Snackbar.LengthShort);
                        onSuccess?.Invoke();
                    });
                }
            );

            return true;
        }

        protected virtual void DoDataExchange(DatabaseContext dbContext)
        {
            _viewHolder.DoDataExchange(Item, dbContext);
        }

        protected virtual async Task<bool> DoSave(DatabaseContext dbContext)
        {
            DoDataExchange(dbContext);
await Task.Delay(0).ConfigureAwait(false);

            return true;
        }

        protected virtual void Reset()
        {
            IsDirty = false;

            UpdateView();
        }
    }
}

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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Core.Util;
using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Adapters;
using EnergonSoftware.BackpackPlanner.Droid.DAL;
using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Droid.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data entry fragments
    /// </summary>
    public abstract class DataFragment<T> : BaseFragment, IBackPressedListener, IDirtyMarker
        where T: BaseModel<T>, new()
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DataFragment<T>));

        private sealed class FilterListener<TE> : Java.Lang.Object, Filter.IFilterListener
        {
            private readonly BaseListViewAdapter<TE> _adapter;

            public FilterListener(BaseListViewAdapter<TE> adapter)
            {
                _adapter = adapter;
            }

            public void OnFilterComplete(int count)
            {
                // TODO: need a method to build an IComparator
                //_adapter.Sort();
            }
        }

#region Events
        public event PropertyChangedEventHandler PropertyChanged;
#endregion

        public abstract T Item { get; protected set; }

        private bool _isDirty;

        public bool IsDirty
        {
            get => _isDirty;

            set
            {
                _isDirty = value;
                NotifyPropertyChanged();
            }
        }

        protected abstract int CleanTitleResource { get; }

        protected abstract int DirtyTitleResource { get; }

        protected override int TitleResource => IsDirty ? DirtyTitleResource : CleanTitleResource;

        private BaseModelViewHolder<T> _viewHolder;

        protected abstract BaseModelViewHolder<T> CreateViewHolder(BaseActivity activity, View view);

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            BaseActivity.RegisterBackPressedListener(this);
        }

        public override void OnDestroy()
        {
            BaseActivity.DeregisterBackPressedListener(this);

            base.OnDestroy();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _viewHolder = CreateViewHolder(BaseActivity, view);
            _viewHolder.PropertyChanged += (sender, args) =>
            {
                IsDirty = true;
            };
        }

        public bool OnBackPressed()
        {
            if(!IsDirty) {
                return true;
            }

            DialogUtil.ShowYesNoCancelAlert(BaseActivity, Resource.String.message_unsaved_changes, Resource.String.title_unsaved_changes,
                (sender, args) =>
                {
                    Save(() =>
                        {
                            BaseActivity.OnBackPressed();
                        }
                    );
                },
                (sender, args) =>
                {
                    Reset();

                    BaseActivity.OnBackPressed();
                }
            );
            return false;
        }

        protected override void UpdateView()
        {
            base.UpdateView();

            _viewHolder.PropertyChangedNotificationEnabled = false;
            _viewHolder.UpdateView(Item);
            _viewHolder.PropertyChangedNotificationEnabled = true;
        }

        protected virtual bool Validate()
        {
            return _viewHolder.Validate();
        }

#region Model Entries
        protected void SetItemEntryList<TI, TIE>(ItemEntries<T, TI, TIE> itemEntries, BaseModelEntryViewHolder<T, TI, TIE> itemEntryViewHolder)
            where TI: BaseModel<TI>, IBackpackPlannerItem, new()
            where TIE: BaseModelEntry<TIE, T, TI>, new()
        {
            for(int i=0; i<itemEntries.ItemCount; ++i) {
                TI item = itemEntries.Items?[i];
                if(null == item) {
                    Logger.Error($"Found null item at index {i} while setting item entries!");
                    continue;
                }

                TIE entry = itemEntries.GetItemEntry(item);
                if(null != entry) {
                    itemEntries.SelectItem(i, true);
                    itemEntries.ItemListAdapter?.AddItem(entry);
                }
            }

            itemEntryViewHolder.UpdateView(itemEntries);
        }

        private void UpdateItemEntryList<TI, TIE>(ItemEntries<T, TI, TIE> itemEntries, BaseModelEntryViewHolder<T, TI, TIE> itemEntryViewHolder, int index, bool isSelected)
            where TI: BaseModel<TI>, IBackpackPlannerItem, new()
            where TIE: BaseModelEntry<TIE, T, TI>, new()
        {
            TI item = itemEntries.Items?[index];
            if(null == item) {
                Logger.Error($"Found null item at index {index} while updating item entries!");
                return;
            }

            itemEntries.SelectItem(index, isSelected);
            if(isSelected) {
                TIE entry = new TIE
                {
                    Count = 1
                };
                entry.SetModel(item);

                itemEntries.ItemListAdapter?.AddItem(entry);
            } else {
                itemEntries.ItemListAdapter?.RemoveItem(item);
            }

            itemEntryViewHolder.UpdateView(itemEntries);
        }

        protected void AddItemEntry<TI, TIE>(int addItemDialogTitleResource, ItemEntries<T, TI, TIE> itemEntries, BaseModelEntryViewHolder<T, TI, TIE> itemEntryViewHolder)
            where TI: BaseModel<TI>, IBackpackPlannerItem, new()
            where TIE: BaseModelEntry<TIE, T, TI>, new()
        {
            DialogUtil.ShowMultiChoiceAlertWithSearch(BaseActivity, addItemDialogTitleResource,
                itemEntries.ItemNames, itemEntries.SelectedItems,
                (a, b) =>
                {
                    itemEntries.ItemListAdapter?.Filter.InvokeFilter(b.NewText, new FilterListener<TIE>(itemEntries.ItemListAdapter));
                },
                (a, b) =>
                {
                    UpdateItemEntryList(itemEntries, itemEntryViewHolder, b.Which, b.IsChecked);
                    IsDirty = true;
                }
            );
        }
#endregion

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

                        SnackbarUtil.ShowSnackbar(View, Resource.String.label_added_item, Android.Support.Design.Widget.Snackbar.LengthShort);
                        onSuccess?.Invoke();

                        Clean();
                    });
                }
            );

            return true;
        }

        public void Clean()
        {
            IsDirty = false;
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
            UpdateView();

            Clean();
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            Activity.Title = Resources.GetString(TitleResource);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

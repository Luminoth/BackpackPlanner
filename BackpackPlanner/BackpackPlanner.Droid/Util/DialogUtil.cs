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

using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public static class DialogUtil
    {
        public static ProgressDialog ShowProgressDialog(Activity activity, int messageResId, /*int titleResId,*/ bool allowCancel, bool indeterminate, ProgressDialogStyle style=ProgressDialogStyle.Spinner)
        {
            ProgressDialog dialog = new ProgressDialog(activity, Resource.Style.AppTheme_AlertDialog);
            dialog.SetProgressStyle(style);
            //dialog.SetTitle(titleResId);
            dialog.SetMessage(TextUtil.GetHtmlString(activity, messageResId));
            dialog.SetCancelable(allowCancel);
            dialog.SetCanceledOnTouchOutside(allowCancel);
            dialog.Indeterminate = indeterminate;
            dialog.Show();
            return dialog;
        }

        public static Android.Support.V7.App.AlertDialog ShowAlert(Activity activity, int messageResId, int titleResId)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_AlertDialog);
            return builder.SetTitle(titleResId)
                .SetMessage(TextUtil.GetHtmlString(activity, messageResId))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowOkAlert(Activity activity, int messageResId, int titleResId, EventHandler<DialogClickEventArgs> okEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_AlertDialog);
            return builder.SetTitle(titleResId)
                .SetMessage(TextUtil.GetHtmlString(activity, messageResId))
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowOkCancelAlert(Activity activity, int messageResId, int titleResId, EventHandler<DialogClickEventArgs> okEventHandler=null, EventHandler<DialogClickEventArgs> cancelEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_AlertDialog);
            return builder.SetTitle(titleResId)
                .SetMessage(TextUtil.GetHtmlString(activity, messageResId))
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .SetNegativeButton(Android.Resource.String.Cancel, cancelEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowYesNoAlert(Activity activity, int messageResId, int titleResId, EventHandler<DialogClickEventArgs> yesEventHandler=null, EventHandler<DialogClickEventArgs> noEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_AlertDialog);
            return builder.SetTitle(titleResId)
                .SetMessage(TextUtil.GetHtmlString(activity, messageResId))
                .SetPositiveButton(Resource.String.label_yes, yesEventHandler ?? ((sender, args) => { }))
                .SetNegativeButton(Resource.String.label_no, noEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowListViewAlert(Activity activity, int titleResId, IListAdapter adapter, EventHandler<DialogClickEventArgs> itemClickEventHandler=null, EventHandler<DialogClickEventArgs> okEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_AlertDialog);
            return builder.SetTitle(titleResId)
                .SetAdapter(adapter, itemClickEventHandler ?? ((sender, args) => { }))
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowSingleChoiceAlert(Activity activity, int titleResId, string[] items, int checkedItem, EventHandler<DialogClickEventArgs> itemClickEventHandler=null, EventHandler<DialogClickEventArgs> okEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_AlertDialog);
            return builder.SetTitle(titleResId)
                .SetSingleChoiceItems(items, checkedItem, itemClickEventHandler ?? ((sender, args) => { }))
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowMultiChoiceAlert(Activity activity, int titleResId, string[] items, bool[] checkedItems, EventHandler<DialogMultiChoiceClickEventArgs> itemClickEventHandler=null, EventHandler<DialogClickEventArgs> okEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_AlertDialog);
            return builder.SetTitle(titleResId)
                .SetMultiChoiceItems(items, checkedItems, itemClickEventHandler ?? ((sender, args) => { }))
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowMultiChoiceAlertWithSearch(Activity activity, int titleResId, string[] items, bool[] checkedItems, EventHandler<Android.Support.V7.Widget.SearchView.QueryTextChangeEventArgs> queryTextChangedEventHandler=null, EventHandler<DialogMultiChoiceClickEventArgs> itemClickEventHandler=null, EventHandler<DialogClickEventArgs> okEventHandler=null)
        {
            View titleView = LayoutInflater.From(activity).Inflate(Resource.Layout.alert_dialog_filter, null);

            TextView titleTextView = titleView.FindViewById<TextView>(Resource.Id.alert_dialog_title);
            titleTextView.SetText(titleResId);

            Android.Support.V7.Widget.SearchView searchView = titleView.FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.alert_dialog_search);
            searchView.QueryHint = activity.Resources.GetString(Resource.String.search_hint);
            searchView.QueryTextChange += queryTextChangedEventHandler;

            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_AlertDialog);
            Android.Support.V7.App.AlertDialog dialog = builder.SetCustomTitle(titleView)
                .SetMultiChoiceItems(items, checkedItems,
                    (sender, args) =>
                    {
                        searchView.ClearFocus();
                        itemClickEventHandler?.Invoke(sender, args);
                    }
                )
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .Show();

            // fix focus/keyboard issues
            dialog.ListView.RequestFocus();
            searchView.FocusChange += (sender, args) =>
            {
                if(args.HasFocus) {
                    dialog.Window.SetSoftInputMode(SoftInput.StateAlwaysVisible | SoftInput.AdjustResize);
                }
            };

// TODO: can't seem to get the keyboard to show!
// so for now I guess we'll just kill search
searchView.Visibility = ViewStates.Gone;

            return dialog;
        }
    }
}

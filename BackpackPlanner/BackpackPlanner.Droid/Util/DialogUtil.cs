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
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public static class DialogUtil
    {
        public static ProgressDialog ShowProgressDialog(Activity activity, int messageResId, /*int titleResId,*/ bool allowCancel, bool indeterminate, ProgressDialogStyle style=ProgressDialogStyle.Spinner)
        {
            ProgressDialog dialog = new ProgressDialog(activity);
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
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetTitle(titleResId)
                .SetMessage(TextUtil.GetHtmlString(activity, messageResId))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowOkAlert(Activity activity, int messageResId, int titleResId, EventHandler<DialogClickEventArgs> okEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetTitle(titleResId)
                .SetMessage(TextUtil.GetHtmlString(activity, messageResId))
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowOkCancelAlert(Activity activity, int messageResId, int titleResId, EventHandler<DialogClickEventArgs> okEventHandler=null, EventHandler<DialogClickEventArgs> cancelEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetTitle(titleResId)
                .SetMessage(TextUtil.GetHtmlString(activity, messageResId))
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .SetNegativeButton(Android.Resource.String.Cancel, cancelEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowYesNoAlert(Activity activity, int messageResId, int titleResId, EventHandler<DialogClickEventArgs> yesEventHandler=null, EventHandler<DialogClickEventArgs> noEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetTitle(titleResId)
                .SetMessage(TextUtil.GetHtmlString(activity, messageResId))
                .SetPositiveButton(Resource.String.label_yes, yesEventHandler ?? ((sender, args) => { }))
                .SetNegativeButton(Resource.String.label_no, noEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowListViewAlert(Activity activity, int titleResId, IListAdapter adapter, EventHandler<DialogClickEventArgs> itemClickEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetTitle(titleResId)
                .SetAdapter(adapter, itemClickEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowSingleChoiceAlert(Activity activity, int titleResId, string[] items, int checkedItem, EventHandler<DialogClickEventArgs> itemClickEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetTitle(titleResId)
                .SetSingleChoiceItems(items, checkedItem, itemClickEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowMultiChoiceAlert(Activity activity, int titleResId, string[] items, bool[] checkedItems, EventHandler<DialogMultiChoiceClickEventArgs> itemClickEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetTitle(titleResId)
                .SetMultiChoiceItems(items, checkedItems, itemClickEventHandler ?? ((sender, args) => { }))
                .Show();
        }
    }
}

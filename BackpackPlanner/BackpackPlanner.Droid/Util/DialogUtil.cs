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

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public static class DialogUtil
    {
        public static Android.Support.V7.App.AlertDialog ShowDialog(Activity activity, int messageResId, int titleResId)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetMessage(messageResId)
                .SetTitle(titleResId)
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowOkDialog(Activity activity, int messageResId, int titleResId, EventHandler<DialogClickEventArgs> okEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetMessage(messageResId)
                .SetTitle(titleResId)
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .Show();
        }

        public static Android.Support.V7.App.AlertDialog ShowOkCancelDialog(Activity activity, int messageResId, int titleResId, EventHandler<DialogClickEventArgs> okEventHandler=null, EventHandler<DialogClickEventArgs> cancelEventHandler=null)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(activity);
            return builder.SetMessage(messageResId)
                .SetTitle(titleResId)
                .SetPositiveButton(Android.Resource.String.Ok, okEventHandler ?? ((sender, args) => { }))
                .SetNegativeButton(Android.Resource.String.Cancel, cancelEventHandler ?? ((sender, args) => { }))
                .Show();
        }
    }
}

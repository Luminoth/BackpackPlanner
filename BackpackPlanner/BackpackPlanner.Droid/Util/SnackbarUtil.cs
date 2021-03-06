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

using Android.Graphics;
using Android.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public static class SnackbarUtil
    {
        public static void ShowSnackbar(View view, int textResId, int duration)
        {
            Android.Support.Design.Widget.Snackbar.Make(view, textResId, duration).Show();
        }

        public static void ShowUndoSnackbar(View view, int textResId, int duration, Action<View> undoAction)
        {
            Android.Support.Design.Widget.Snackbar.Make(view, textResId, duration)
                .SetAction(Resource.String.label_undo, undoAction)
                .SetActionTextColor(Color.White)
                .Show();
        }
    }
}

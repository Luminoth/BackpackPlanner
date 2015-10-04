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

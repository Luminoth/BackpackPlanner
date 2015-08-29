using System;

using Android.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    // TODO: break this up into separate sets of arguments for each event type
    public sealed class DrawerToggleEventArgs : EventArgs
    {
        public View DrawerView { get; set; }

        public float SlideOffset { get; set; }

        public int NewState { get; set; }
    }
}
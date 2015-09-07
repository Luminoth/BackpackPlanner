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

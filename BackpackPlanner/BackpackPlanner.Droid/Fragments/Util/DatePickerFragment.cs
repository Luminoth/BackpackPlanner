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
using Android.OS;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Util
{
    // TODO: why does this exist? it's not used and DatePickerDialog does the correct thing, no?
    public class DatePickerFragment : Android.Support.V4.App.DialogFragment
    {
        // TODO: this may not be the "best" way to do this
        public DateTime Date { get; set; }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new DatePickerDialog(Activity, OnDateSet, Date.Year, Date.Month - 1, Date.Day);
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs args)
        {
            Date = args.Date;
            // TODO: this should probably notify the creator or something?
        }
    }
}

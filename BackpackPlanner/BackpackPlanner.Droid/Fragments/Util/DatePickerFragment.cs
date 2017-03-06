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
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Util
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://developer.android.com/guide/topics/ui/controls/pickers.html#DatePicker
    /// </remarks>
    public sealed class DatePickerFragment : Android.Support.V4.App.DialogFragment, DatePickerDialog.IOnDateSetListener
    {
#region Events
        public event EventHandler<DateSetEventArgs> DateSetEvent;
#endregion

        private DateTime _initialDate;

        public DatePickerFragment(DateTime initialDate)
        {
            _initialDate = initialDate;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new DatePickerDialog(Activity, this, _initialDate.Year, _initialDate.Month - 1, _initialDate.Day);
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            DateSetEvent?.Invoke(this, new DateSetEventArgs
                {
                    Date = new DateTime(year, monthOfYear + 1, dayOfMonth)
                }
            );
        }
    }
}

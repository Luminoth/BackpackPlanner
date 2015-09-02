using System;
using Android.App;
using Android.OS;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Util
{
    public class DatePickerFragment : Android.Support.V4.App.DialogFragment
    {
        // TODO: this may not be the "best" way to do this
        public DateTime Date { get; set; } = new DateTime();

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

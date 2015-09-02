using Android.App;
using Android.OS;

using EnergonSoftware.BackpackPlanner.Droid.Util;

// http://stackoverflow.com/questions/30114730/how-to-use-appcompatpreferenceactivity
// http://stackoverflow.com/questions/26439139/getactionbar-returns-null-in-preferenceactivity-appcompat-v7-21
// http://stackoverflow.com/questions/17849193/how-to-add-action-bar-from-support-library-into-preferenceactivity
// https://code.google.com/p/android/issues/detail?id=78819

namespace EnergonSoftware.BackpackPlanner.Droid
{
    [Activity(Label = "@string/title_settings")]
    public class SettingsActivity : AppCompatPreferenceActivity
    {
        private Android.Support.V7.Widget.Toolbar _toolBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // TODO: this causes a crash
            //InitToolBar();

            AddPreferencesFromResource(Resource.Xml.settings);
        }

        private void InitToolBar()
        {
            _toolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolBar);
        }
    }
}

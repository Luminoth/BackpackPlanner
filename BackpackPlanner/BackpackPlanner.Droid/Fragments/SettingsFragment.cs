using Android.OS;
using Android.Preferences;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public class SettingsFragment : PreferenceFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AddPreferencesFromResource(Resource.Xml.settings);
        }
    }
}

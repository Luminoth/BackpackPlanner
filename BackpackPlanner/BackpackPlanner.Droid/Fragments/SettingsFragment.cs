using Android.OS;
using Android.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public class SettingsFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_settings, container, false);
        }
    }
}
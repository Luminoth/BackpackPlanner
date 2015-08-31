using Android.OS;
using Android.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public class TripPlansFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_trip_plans, container, false);
        }
    }
}
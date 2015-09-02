using Android.OS;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Plans
{
    public class TripPlansFragment : Android.Support.V4.App.Fragment
    {
        TextView _noTripPlansTextView;
        ViewGroup _tripPlansLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_trip_plans, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _noTripPlansTextView = view.FindViewById<TextView>(Resource.Id.no_trip_plans);

            _tripPlansLayout = view.FindViewById<LinearLayout>(Resource.Id.trip_plans_layout);
            _tripPlansLayout.Visibility = ViewStates.Gone;
        }
    }
}
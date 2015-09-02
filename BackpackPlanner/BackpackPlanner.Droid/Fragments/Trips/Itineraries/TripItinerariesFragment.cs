using Android.OS;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries
{
    public class TripItinerariesFragment : Android.Support.V4.App.Fragment
    {
        TextView _noTripItinerariesTextView;
        ViewGroup _tripItinerariesLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_trip_itineraries, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _noTripItinerariesTextView = view.FindViewById<TextView>(Resource.Id.no_trip_itineraries);

            _tripItinerariesLayout = view.FindViewById<LinearLayout>(Resource.Id.trip_itineraries_layout);
            _tripItinerariesLayout.Visibility = ViewStates.Gone;
        }
    }
}
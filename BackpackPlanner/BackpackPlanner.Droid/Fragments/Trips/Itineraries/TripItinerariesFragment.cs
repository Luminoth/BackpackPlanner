using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Trips.Itineraries
{
    public class TripItinerariesFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_trip_itineraries, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            TextView noTripItinerariesTextView = view.FindViewById<TextView>(Resource.Id.no_trip_itineraries);

            ViewGroup tripItinerariesLayout = view.FindViewById<LinearLayout>(Resource.Id.trip_itineraries_layout);
            tripItinerariesLayout.Visibility = ViewStates.Gone;

            FloatingActionButton addTripItineraryButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_add_trip_itinerary);
            addTripItineraryButton.Click += (sender, args) => {
                // TODO
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(Resource.String.title_trip_itineraries);
        }
    }
}

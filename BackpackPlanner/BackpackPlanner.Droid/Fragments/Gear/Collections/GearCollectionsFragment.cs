using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections
{
    public class GearCollectionsFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_gear_collections, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            TextView noGearCollectionsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_collections);
            // TODO

            ViewGroup gearCollectionsLayout = view.FindViewById<LinearLayout>(Resource.Id.gear_collections_layout);
            gearCollectionsLayout.Visibility = ViewStates.Gone;

            FloatingActionButton addGearCollectionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_add_gear_collection);
            addGearCollectionButton.Click += (sender, args) => {
                // TODO
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(Resource.String.title_gear_collections);
        }
    }
}

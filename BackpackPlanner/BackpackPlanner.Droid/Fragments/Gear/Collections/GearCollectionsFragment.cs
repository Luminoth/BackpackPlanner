using Android.OS;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections
{
    public class GearCollectionsFragment : Android.Support.V4.App.Fragment
    {
        TextView _noGearCollectionsTextView;
        ViewGroup _gearCollectionsLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_gear_collections, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _noGearCollectionsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_collections);

            _gearCollectionsLayout = view.FindViewById<LinearLayout>(Resource.Id.gear_collections_layout);
            _gearCollectionsLayout.Visibility = ViewStates.Gone;
        }
    }
}

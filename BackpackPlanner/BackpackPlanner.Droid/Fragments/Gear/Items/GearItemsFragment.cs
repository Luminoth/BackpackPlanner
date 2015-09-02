using Android.OS;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public class GearItemsFragment : Android.Support.V4.App.Fragment
    {
        TextView _noGearItemsTextView;
        ViewGroup _gearItemsLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_gear_items, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _noGearItemsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_items);

            _gearItemsLayout = view.FindViewById<LinearLayout>(Resource.Id.gear_items_layout);
            _gearItemsLayout.Visibility = ViewStates.Gone;
        }
    }
}

using Android.OS;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems
{
    public class GearSystemsFragment : Android.Support.V4.App.Fragment
    {
        TextView _noGearSystemsTextView;
        ViewGroup _gearSystemsLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_gear_systems, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _noGearSystemsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_systems);

            _gearSystemsLayout = view.FindViewById<LinearLayout>(Resource.Id.gear_systems_layout);
            _gearSystemsLayout.Visibility = ViewStates.Gone;
        }
    }
}

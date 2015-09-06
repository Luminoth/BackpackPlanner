using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Systems
{
    public class GearSystemsFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_gear_systems, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            TextView noGearSystemsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_systems);

            ViewGroup gearSystemsLayout = view.FindViewById<LinearLayout>(Resource.Id.gear_systems_layout);
            gearSystemsLayout.Visibility = ViewStates.Gone;

            FloatingActionButton addGearSystemButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_add_gear_system);
            addGearSystemButton.Click += (sender, args) => {
                // TODO
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(Resource.String.title_gear_systems);
        }
    }
}

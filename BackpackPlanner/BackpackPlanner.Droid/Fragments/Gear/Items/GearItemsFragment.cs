using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Items
{
    public class GearItemsFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_gear_items, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            TextView noGearItemsTextView = view.FindViewById<TextView>(Resource.Id.no_gear_items);
            // TODO

            ViewGroup gearItemsLayout = view.FindViewById<LinearLayout>(Resource.Id.gear_items_layout);
            gearItemsLayout.Visibility = ViewStates.Gone;

            FloatingActionButton addGearItemButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab_add_gear_item);
            addGearItemButton.Click += (sender, args) => {
                Android.Support.V4.App.FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
                fragmentTransaction.Replace(Resource.Id.frame_content, new AddGearItemFragment());
                fragmentTransaction.AddToBackStack(null);
                fragmentTransaction.Commit();
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(Resource.String.title_gear_items);
        }
    }
}

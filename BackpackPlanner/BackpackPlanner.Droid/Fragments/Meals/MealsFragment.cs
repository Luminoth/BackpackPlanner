using Android.OS;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Meals
{
    public class MealsFragment : Android.Support.V4.App.Fragment
    {
        TextView _noMealsTextView;
        ViewGroup _mealsLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_meals, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _noMealsTextView = view.FindViewById<TextView>(Resource.Id.no_meals);

            _mealsLayout = view.FindViewById<LinearLayout>(Resource.Id.meals_layout);
            _mealsLayout.Visibility = ViewStates.Gone;
        }
    }
}

using Android.OS;
using Android.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public abstract class BaseFragment : Android.Support.V4.App.Fragment
    {
        public abstract int LayoutResource { get; }

        public abstract int TitleResource { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(LayoutResource, container, false);
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(TitleResource);
        }
    }
}

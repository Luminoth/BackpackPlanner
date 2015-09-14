using Android.App;
using Android.OS;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public static class FragmentTransitionUtil
    {
        private static void InitTransaction(Android.Support.V4.App.FragmentTransaction fragmentTransaction)
        {
            // TODO: why can't we slide in right and slide out left? wtf....
            fragmentTransaction.SetCustomAnimations(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight,
                Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
        }

        public static void Transition(Activity activity, Android.Support.V4.App.FragmentTransaction fragmentTransaction, int frameResId, Android.Support.V4.App.Fragment fragment)
        {
            InitTransaction(fragmentTransaction);

            fragmentTransaction.Replace(frameResId, fragment);
            fragmentTransaction.Commit();

            if(Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb) {
                activity.InvalidateOptionsMenu();
            }
        }

        public static void StackTransition(Activity activity, Android.Support.V4.App.FragmentTransaction fragmentTransaction, int frameResId, Android.Support.V4.App.Fragment fragment, string tags)
        {
            InitTransaction(fragmentTransaction);

            fragmentTransaction.Replace(frameResId, fragment);
            fragmentTransaction.AddToBackStack(tags);
            fragmentTransaction.Commit();

            // TODO: is this if necessary here?
            if(Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb) {
                activity.InvalidateOptionsMenu();
            }
        }
    }
}

using Android.App;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public static class FragmentTransitionUtil
    {
        private static void InitTransaction(Android.Support.V4.App.FragmentTransaction fragmentTransaction)
        {
            // TODO: this fade is annoying, but should definitely revisit this another time
            /*fragmentTransaction.SetCustomAnimations(
                Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut,
                Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);*/
        }

        public static void Transition(Activity activity, Android.Support.V4.App.FragmentTransaction fragmentTransaction, int frameResId, Android.Support.V4.App.Fragment fragment)
        {
            InitTransaction(fragmentTransaction);

            fragmentTransaction.Replace(frameResId, fragment);
            fragmentTransaction.Commit();
        }

        public static void StackTransition(Activity activity, Android.Support.V4.App.FragmentTransaction fragmentTransaction, int frameResId, Android.Support.V4.App.Fragment fragment, string tags)
        {
            InitTransaction(fragmentTransaction);

            fragmentTransaction.Replace(frameResId, fragment);
            fragmentTransaction.AddToBackStack(tags);
            fragmentTransaction.Commit();
        }
    }
}

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

        public static void Transition(Android.Support.V4.App.FragmentTransaction fragmentTransaction, int frameResId, Android.Support.V4.App.Fragment fragment)
        {
            InitTransaction(fragmentTransaction);

            fragmentTransaction.Replace(frameResId, fragment);
            fragmentTransaction.Commit();
        }

        public static void StackTransition(Android.Support.V4.App.FragmentTransaction fragmentTransaction, int frameResId, Android.Support.V4.App.Fragment fragment)
        {
            InitTransaction(fragmentTransaction);

            fragmentTransaction.Replace(frameResId, fragment);
            fragmentTransaction.AddToBackStack(null);
            fragmentTransaction.Commit();
        }
    }
}
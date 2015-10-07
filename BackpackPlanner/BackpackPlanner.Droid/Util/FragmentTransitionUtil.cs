using System;
using System.Linq;

using Android.App;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public static class FragmentTransitionUtil
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(FragmentTransitionUtil));

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

        public static void ReloadCurrentFragment(Android.Support.V4.App.FragmentManager fragmentManager)
        {
            Android.Support.V4.App.Fragment currentFragment = GetCurrentFragment(fragmentManager);
            if(null == currentFragment) {
                return;
            }

            Logger.Debug("Reloading current fragment...");

            Android.Support.V4.App.FragmentTransaction fragmentTransaction = fragmentManager.BeginTransaction();
            fragmentTransaction.Detach(currentFragment);
            fragmentTransaction.Attach(currentFragment);
            fragmentTransaction.Commit();
        }

        public static Android.Support.V4.App.Fragment GetCurrentFragment(Android.Support.V4.App.FragmentManager fragmentManager)
        {
            if(null == fragmentManager) {
                throw new ArgumentNullException(nameof(fragmentManager));
            }
            return fragmentManager.BackStackEntryCount <= 0 ? null : fragmentManager.Fragments.ElementAt(fragmentManager.BackStackEntryCount - 1);
        }
    }
}

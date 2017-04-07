/*
   Copyright 2015 Shane Lillie

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

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

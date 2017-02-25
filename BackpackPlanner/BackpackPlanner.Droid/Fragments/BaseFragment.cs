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

#define DEBUG_LIFECYCLE

using System.Diagnostics;

using Android.OS;
using Android.Runtime;
using Android.Views;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Activities;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for fragment creation
    /// </summary>
    public abstract class BaseFragment : Android.Support.V4.App.Fragment
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(BaseFragment));

#if DEBUG_LIFECYCLE
        private readonly Stopwatch _startupStopwatch = new Stopwatch();
#endif

        public BaseActivity BaseActivity => (BaseActivity)Activity;

        protected abstract int LayoutResource { get; }

        protected abstract int TitleResource { get; }

        protected abstract bool HasSearchView { get; }

        public Android.Support.V7.Widget.SearchView FilterView { get; private set; }

#region Fragment Lifecycle
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

#if DEBUG_LIFECYCLE
            Logger.Debug($"OnCreate - {GetType()}");

            _startupStopwatch.Start();
#endif
        }

        public override void OnDestroy()
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnDestroy - {GetType()}");
#endif

            base.OnDestroy();
        }

        public override void OnStart()
        {
            base.OnStart();

#if DEBUG_LIFECYCLE
            Logger.Debug($"OnStart - {GetType()}");

            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Fragment.Start(): {_startupStopwatch.ElapsedMilliseconds}ms");
            }
#endif
        }

        public override void OnStop()
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnStop - {GetType()}");
#endif

            base.OnStop();
        }

        public override void OnResume()
        {
            base.OnResume();

#if DEBUG_LIFECYCLE
            if(_startupStopwatch.IsRunning) {
                Logger.Debug($"Time to Fragment.OnResume(): {_startupStopwatch.ElapsedMilliseconds}ms");
            }
            _startupStopwatch.Stop();
#endif

            Activity.Title = Resources.GetString(TitleResource);
        }

        public override void OnPause()
        {
#if DEBUG_LIFECYCLE
            Logger.Debug($"OnPause - {GetType()}");
#endif

            base.OnPause();
        }
#endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            return inflater.Inflate(LayoutResource, container, false);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            inflater.Inflate(HasSearchView ? Resource.Menu.options_menu : Resource.Menu.options_menu_nosearch, menu);

            IMenuItem searchItem = menu.FindItem(Resource.Id.action_search);
            if(null == searchItem) {
                return;
            }

            View actionView = Android.Support.V4.View.MenuItemCompat.GetActionView(searchItem);
            FilterView = actionView.JavaCast<Android.Support.V7.Widget.SearchView>();

            // TODO: let the subclasses set this up
            FilterView.QueryHint = Resources.GetString(Resource.String.search_hint);
        }

        public void TransitionToFragment(int frameResId, Android.Support.V4.App.Fragment fragment, string tags)
        {
            FragmentTransitionUtil.StackTransition(Activity, FragmentManager.BeginTransaction(), frameResId, fragment, tags);
        }
    }
}

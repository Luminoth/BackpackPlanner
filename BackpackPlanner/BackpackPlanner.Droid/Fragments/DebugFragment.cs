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

using System.Linq;

using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Util;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public sealed class DebugFragment : BaseFragment
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DebugFragment));

        protected override int LayoutResource => Resource.Layout.fragment_debug;

        protected override int TitleResource => Resource.String.title_debug;

        protected override bool HasSearchView => false;

        protected override bool CanExport => false;

#region Controls
        private ListView _logTextListView;
        private ArrayAdapter<LogMessageEventArgs> _logTextAdapter;
#endregion

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Button resetFTUEButton = view.FindViewById<Button>(Resource.Id.button_reset_ftue);
            resetFTUEButton.Click += (sender, args) => {
                Logger.Debug("Resetting FTUE state");
                BaseActivity.BackpackPlannerState.Settings.MetaSettings.FirstRun = true;
            };

            Button resetDatabaseButton = view.FindViewById<Button>(Resource.Id.button_reset_database);
            resetDatabaseButton.Click += (sender, args) => {
                Logger.Debug("Resetting database");
                DialogUtil.ShowOkAlert(Activity, "TODO", "Database reset not implemented!");
            };

            _logTextAdapter = new ArrayAdapter<LogMessageEventArgs>(Context, Android.Resource.Layout.SimpleListItem1,
                CustomLogger.LogMessages.ToArray());

            _logTextListView = view.FindViewById<ListView>(Resource.Id.log_text_list);
            _logTextListView.Adapter = _logTextAdapter;

            CustomLogger.LogMessageEvent += LogMessageEventHandler;
        }

        public override void OnDestroyView()
        {
            CustomLogger.LogMessageEvent -= LogMessageEventHandler;

            base.OnDestroyView();
        }

        private void LogMessageEventHandler(object sender, LogMessageEventArgs args)
        {
            // this is definitely not efficient
            _logTextAdapter.Insert(args, 0);
        }
    }
}

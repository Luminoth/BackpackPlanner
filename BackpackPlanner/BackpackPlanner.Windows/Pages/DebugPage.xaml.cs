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

using Windows.UI.Xaml;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Windows.Pages
{
    public sealed partial class DebugPage
    {
        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(DebugPage));

        public DebugPage()
        {
            InitializeComponent();
        }

        private void ResetFTUE_Click(object sender, RoutedEventArgs e)
        {
            Logger.Debug("Resetting FTUE state");
            App.CurrentApp.BackpackPlannerState.Settings.MetaSettings.FirstRun = true;
        }

        private void ResetDatabase_Click(object sender, RoutedEventArgs e)
        {
            Logger.Debug("Resetting database");
            // TODO
        }
    }
}

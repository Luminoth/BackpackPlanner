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

using System.Windows;

using log4net.Config;

namespace EnergonSoftware.BackpackPlanner.DriveFileExplorer
{
    public partial class App
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            BasicConfigurator.Configure();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            DriveFileExplorer.Properties.Settings.Default.Save();
        }
    }
}

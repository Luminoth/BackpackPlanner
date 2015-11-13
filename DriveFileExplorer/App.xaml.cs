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

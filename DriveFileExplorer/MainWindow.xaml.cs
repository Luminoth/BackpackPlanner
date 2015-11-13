using System.Windows;

using log4net;

namespace EnergonSoftware.BackpackPlanner.DriveFileExplorer
{
    public partial class MainWindow
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainWindow));

        public PlayServicesManager PlayServicesManager { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            PlayServicesManager = new PlayServicesManager();
            DataContext = PlayServicesManager;
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void MenuFileConnect_Click(object sender, RoutedEventArgs e)
        {
            await PlayServicesManager.ConnectAsync();
        }

        private async void MenuFileDisconnect_Click(object sender, RoutedEventArgs e)
        {
            await PlayServicesManager.DisconnectAsync();
        }
    }
}

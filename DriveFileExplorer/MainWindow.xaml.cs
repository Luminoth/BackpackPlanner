using System.Windows;

namespace EnergonSoftware.BackpackPlanner.DriveFileExplorer
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

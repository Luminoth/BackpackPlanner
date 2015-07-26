using Microsoft.Phone.Controls;

namespace EnergonSoftware.BackpackPlanner.WinPhone
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            Xamarin.Forms.Forms.Init();
            LoadApplication(new BackpackPlanner.App());
        }
    }
}

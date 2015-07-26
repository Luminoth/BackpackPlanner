using EnergonSoftware.BackpackPlanner.WinPhone.Resources;

namespace EnergonSoftware.BackpackPlanner.WinPhone
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources
        { get { return _localizedResources; } }
    }
}

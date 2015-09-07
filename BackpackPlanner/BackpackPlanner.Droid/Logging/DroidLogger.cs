using Android.Util;

using EnergonSoftware.BackpackPlanner.Logging;

namespace EnergonSoftware.BackpackPlanner.Droid.Logging
{
    internal class DroidLogger : ILogger
    {
        public void Debug(string message)
        {
            Log.Debug(MainActivity.LogTag, message);
        }

        public void Info(string message)
        {
            Log.Info(MainActivity.LogTag, message);
        }

        public void Warn(string message)
        {
            Log.Warn(MainActivity.LogTag, message);
        }

        public void Error(string message)
        {
            Log.Error(MainActivity.LogTag, message);
        }
    }
}

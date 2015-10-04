using System;

using Android.Util;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Droid.Activities;

namespace EnergonSoftware.BackpackPlanner.Droid.Logging
{
    internal sealed class DroidLogger : ILogger
    {
        public void Debug(string message)
        {
            Log.Debug(MainActivity.LogTag, message);
        }

        public void Debug(string message, Exception ex)
        {
            Log.Debug(MainActivity.LogTag, message, ex);
        }


        public void Info(string message)
        {
            Log.Info(MainActivity.LogTag, message);
        }

        public void Info(string message, Exception ex)
        {
            Log.Info(MainActivity.LogTag, message, ex);
        }

        public void Warn(string message)
        {
            Log.Warn(MainActivity.LogTag, message);
        }

        public void Warn(string message, Exception ex)
        {
            Log.Warn(MainActivity.LogTag, message, ex);
        }

        public void Error(string message)
        {
            Log.Error(MainActivity.LogTag, message);
        }

        public void Error(string message, Exception ex)
        {
            Log.Error(MainActivity.LogTag, message, ex);
        }
    }
}

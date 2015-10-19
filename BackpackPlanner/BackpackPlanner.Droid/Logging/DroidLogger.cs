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

using System;

using Android.Util;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Droid.Logging
{
    internal sealed class DroidLogger : ILogger
    {
        public const string LogTag = "BackpackPlanner.Droid";

        public void Debug(string message)
        {
            Log.Debug(LogTag, message);
        }

        public void Debug(string message, Exception ex)
        {
            Log.Debug(LogTag, message, ex);
        }


        public void Info(string message)
        {
            Log.Info(LogTag, message);
        }

        public void Info(string message, Exception ex)
        {
            Log.Info(LogTag, message, ex);
        }

        public void Warn(string message)
        {
            Log.Warn(LogTag, message);
        }

        public void Warn(string message, Exception ex)
        {
            Log.Warn(LogTag, message, ex);
        }

        public void Error(string message)
        {
            Log.Error(LogTag, message);
        }

        public void Error(string message, Exception ex)
        {
            Log.Error(LogTag, message, ex);
        }
    }
}

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

using Windows.Foundation.Diagnostics;

using EnergonSoftware.BackpackPlanner.Core.Logging;

namespace EnergonSoftware.BackpackPlanner.Windows.Logging
{
    internal sealed class WindowsLogger : ILogger, IDisposable
    {
        private readonly ILoggingChannel _channel;
        private readonly ILoggingSession _session;

        public WindowsLogger()
        {
            _channel = new LoggingChannel("BackpackPlannerChannel", null);
            _session = new LoggingSession("BackpackPlannerSession");

            _session.AddLoggingChannel(_channel);
        }

#region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(disposing) {
                _channel.Dispose();
                _session.Dispose();
            }
        }
#endregion

        public void Debug(string message)
        {
            _channel.LogMessage(message, LoggingLevel.Verbose);
        }

        public void Debug(string message, Exception ex)
        {
            // TODO
            _channel.LogMessage(message, LoggingLevel.Verbose);
        }

        public void Info(string message)
        {
            _channel.LogMessage(message, LoggingLevel.Information);
        }

        public void Info(string message, Exception ex)
        {
            // TODO
            _channel.LogMessage(message, LoggingLevel.Information);
        }

        public void Warn(string message)
        {
            _channel.LogMessage(message, LoggingLevel.Warning);
        }

        public void Warn(string message, Exception ex)
        {
            // TODO
            _channel.LogMessage(message, LoggingLevel.Warning);
        }

        public void Error(string message)
        {
            _channel.LogMessage(message, LoggingLevel.Error);
        }

        public void Error(string message, Exception ex)
        {
            // TODO
            _channel.LogMessage(message, LoggingLevel.Error);
        }
    }
}

﻿/*
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
using System.Collections.Generic;

namespace EnergonSoftware.BackpackPlanner.Core.Logging
{
    /// <summary>
    /// Cross-platform logger interface.
    /// </summary>
    /// <remarks>
    /// Implement this on each platform to enable library logging.
    /// </remarks>
    public interface ILogger
    {
        /// <summary>
        /// Debug entry.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        void Debug(string message, Exception ex);

        /// <summary>
        /// Info entry.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        void Info(string message, Exception ex);

        /// <summary>
        /// Warning entry.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(string message);

        void Warn(string message, Exception ex);

        /// <summary>
        /// Error entry.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        void Error(string message, Exception ex);
    }

    /// <summary>
    /// log4net-style logger class
    /// </summary>
    public sealed class CustomLogger : ILogger
    {
        private static readonly object CacheLock = new object();
        
        private static readonly Dictionary<Type, CustomLogger> LoggerCache = new Dictionary<Type, CustomLogger>();

        /// <summary>
        /// Gets a logger for the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A logger for the given type</returns>
        public static ILogger GetLogger(Type type)
        {
            lock(CacheLock) {
                CustomLogger logger;
                if(LoggerCache.TryGetValue(type, out logger)) {
                    return logger;
                }

                logger = new CustomLogger();
                LoggerCache.Add(type, logger);
                return logger;
            }
        }

        private static string BuildMessage(string level, string message)
        {
            return $"{DateTime.Now} {level}: {message}";
        }

        public void Debug(string message)
        {
            BackpackPlannerState.Instance.PlatformLogger.Debug(BuildMessage("DEBUG", message));
        }

        public void Debug(string message, Exception ex)
        {
            BackpackPlannerState.Instance.PlatformLogger.Debug(BuildMessage("DEBUG", message), ex);
        }

        public void Info(string message)
        {
            BackpackPlannerState.Instance.PlatformLogger.Debug(BuildMessage("INFO", message));
        }

        public void Info(string message, Exception ex)
        {
            BackpackPlannerState.Instance.PlatformLogger.Debug(BuildMessage("INFO", message), ex);
        }

        public void Warn(string message)
        {
            BackpackPlannerState.Instance.PlatformLogger.Debug(BuildMessage("WARNING", message));
        }

        public void Warn(string message, Exception ex)
        {
            BackpackPlannerState.Instance.PlatformLogger.Debug(BuildMessage("WARNING", message), ex);
        }

        public void Error(string message)
        {
            BackpackPlannerState.Instance.PlatformLogger.Debug(BuildMessage("ERROR", message));
        }

        public void Error(string message, Exception ex)
        {
            BackpackPlannerState.Instance.PlatformLogger.Debug(BuildMessage("ERROR", message), ex);
        }
    }
}

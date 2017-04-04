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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Core.Util;

namespace EnergonSoftware.BackpackPlanner.Core.Logging
{
    /// <summary>
    /// Cross-platform logger interface.
    /// </summary>
    /// <remarks>
    /// Implement this on each platform to enable library logging.
    /// </remarks>
    // TODO: if we make this an abstract class, we can make the Debug()
    // methods Conditional, and thus avoid the #if DEBUG in the code
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
        public enum Level
        {
            [Description("DEBUG")]
            Debug,

            [Description("INFO")]
            Info,

            [Description("WARNING")]
            Warning,

            [Description("ERROR")]
            Error
        }

        private const int MaxLogBuffer = 500;

        private static ILogger _platformLogger = new DiagnosticsLogger();

#region Events
        public static event EventHandler<LogMessageEventArgs> LogMessageEvent;
#endregion

        /// <summary>
        /// Gets the platform logger.
        /// </summary>
        /// <value>
        /// The platform logger.
        /// </value>
        public static ILogger PlatformLogger
        {
            get { return _platformLogger; }
            set { _platformLogger = value ?? new DiagnosticsLogger(); }
        }

        private static LinkedList<LogMessageEventArgs> _logMessages = new LinkedList<LogMessageEventArgs>();

        public static IReadOnlyCollection<LogMessageEventArgs> LogMessages => _logMessages;

        private static bool _reverseLogBufferDirection;

        public static bool ReverseLogBufferDirection
        {
            get { return _reverseLogBufferDirection; }

            set
            {
                _reverseLogBufferDirection = value;

                _logMessages = new LinkedList<LogMessageEventArgs>(_logMessages.Reverse());
            }
        }

        private static void PruneLogBuffer()
        {
            while(_logMessages.Count > MaxLogBuffer) {
                if(ReverseLogBufferDirection) {
                    _logMessages.RemoveLast();
                } else {
                    _logMessages.RemoveFirst();
                }
            }
        }

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
                if(LoggerCache.TryGetValue(type, out CustomLogger logger)) {
                    return logger;
                }

                logger = new CustomLogger(type);
                LoggerCache.Add(type, logger);
                return logger;
            }
        }

        private static string BuildMessage(Type type, string level, string message)
        {
            return $"{DateTime.Now} [{TaskScheduler.Current.Id}] {type.Name} {level}: {message}";
        }

        [Conditional("DEBUG")]
        private static void AddLog(Level level, string message)
        {
            LogMessageEventArgs messageEvent = new LogMessageEventArgs
            {
                Level = level,
                Message = message
            };

            if(ReverseLogBufferDirection) {
                _logMessages.AddFirst(messageEvent);
            } else {
                _logMessages.AddLast(messageEvent);
            }
            PruneLogBuffer();

            LogMessageEvent?.Invoke(null, messageEvent);
        }

        [Conditional("DEBUG")]
        private static void AddLog(Level level, string message, Exception ex)
        {
            LogMessageEventArgs messageEvent = new LogMessageEventArgs
            {
                Level = level,
                Message = message,
                Exception = ex
            };

            if(ReverseLogBufferDirection) {
                _logMessages.AddFirst(messageEvent);
            } else {
                _logMessages.AddLast(messageEvent);
            }
            PruneLogBuffer();

            LogMessageEvent?.Invoke(null, messageEvent);
        }

        private readonly Type _type;

        public void Debug(string message)
        {
#if DEBUG
            message = BuildMessage(_type, EnumDescription.GetDescriptionFromEnumValue(Level.Debug), message);
            PlatformLogger.Debug(message);
            AddLog(Level.Debug, message);
#endif
        }

        public void Debug(string message, Exception ex)
        {
#if DEBUG
            message = BuildMessage(_type, EnumDescription.GetDescriptionFromEnumValue(Level.Debug), message);
            PlatformLogger.Debug(message, ex);
            AddLog(Level.Debug, message, ex);
#endif
        }

        public void Info(string message)
        {
            message = BuildMessage(_type, EnumDescription.GetDescriptionFromEnumValue(Level.Info), message);
            PlatformLogger.Info(message);
            AddLog(Level.Info, message);
        }

        public void Info(string message, Exception ex)
        {
            message = BuildMessage(_type, EnumDescription.GetDescriptionFromEnumValue(Level.Info), message);
            PlatformLogger.Info(message, ex);
            AddLog(Level.Info, message, ex);
        }

        public void Warn(string message)
        {
            message = BuildMessage(_type, EnumDescription.GetDescriptionFromEnumValue(Level.Warning), message);
            PlatformLogger.Warn(message);
            AddLog(Level.Warning, message);
        }

        public void Warn(string message, Exception ex)
        {
            message = BuildMessage(_type, EnumDescription.GetDescriptionFromEnumValue(Level.Warning), message);
            PlatformLogger.Warn(message, ex);
            AddLog(Level.Warning, message, ex);
        }

        public void Error(string message)
        {
            message = BuildMessage(_type, EnumDescription.GetDescriptionFromEnumValue(Level.Error), message);
            PlatformLogger.Error(message);
            AddLog(Level.Error, message);
        }

        public void Error(string message, Exception ex)
        {
            message = BuildMessage(_type, EnumDescription.GetDescriptionFromEnumValue(Level.Error), message);
            PlatformLogger.Error(message, ex);
            AddLog(Level.Error, message, ex);
        }

        public CustomLogger(Type type)
        {
            _type = type;
        }
    }
}

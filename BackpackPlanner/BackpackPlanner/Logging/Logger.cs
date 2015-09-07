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

namespace EnergonSoftware.BackpackPlanner.Logging
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

        /// <summary>
        /// Info entry.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Warning entry.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(string message);

        /// <summary>
        /// Error entry.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);
    }
}

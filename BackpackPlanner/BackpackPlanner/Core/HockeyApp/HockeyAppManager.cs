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

using System.Threading.Tasks;

namespace EnergonSoftware.BackpackPlanner.Core.HockeyApp
{
    /// <summary>
    /// Platform interface to HockeyApp
    /// </summary>
    public interface IHockeyAppManager
    {
        /// <summary>
        /// Gets the application id.
        /// </summary>
        /// <value>
        /// The application id.
        /// </value>
        string AppId { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        bool IsInitialized { get; }

        /// <summary>
        /// Initializes HockeyApp.
        /// </summary>
        Task InitAsync();

        /// <summary>
        /// 
        /// </summary>
        Task DestroyAsync();

        /// <summary>
        /// Shows the feedback form.
        /// </summary>
        void ShowFeedback();
    }
}

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

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using EnergonSoftware.BackpackPlanner.Core.Logging;
using EnergonSoftware.BackpackPlanner.Windows.Logging;
using EnergonSoftware.BackpackPlanner.Windows.Pages;

using SQLite.Net.Platform.WinRT;

namespace EnergonSoftware.BackpackPlanner.Windows
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : IDisposable
    {
        public static App CurrentApp => (App)Current;

        private static readonly ILogger Logger = CustomLogger.GetLogger(typeof(App));

        public BackpackPlannerState BackpackPlannerState { get; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            InitializeComponent();

            Suspending += OnSuspending;

            CustomLogger.PlatformLogger = new WindowsLogger();

            BackpackPlannerState = new BackpackPlannerState(
                new HockeyAppManager(),
                new WindowsSettingsManager(),
                new PlayServicesManager(),
                new SQLitePlatformWinRT()
            );
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
                BackpackPlannerState.Dispose();
            }
        }
#endregion

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached) {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            InitWindow(e);

            await BackpackPlannerState.InitAsync().ConfigureAwait(false);

            await BackpackPlannerState.DatabaseState.ConnectAsync(ApplicationData.Current.LocalFolder.Path, DatabaseState.DatabaseName).ConfigureAwait(false);
            await BackpackPlannerState.DatabaseState.InitDatabaseAsync(BackpackPlannerState.Settings).ConfigureAwait(false);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity

            deferral.Complete();
        }

        private void InitWindow(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame ?? InitNewInstance(e);
            if(null == rootFrame.Content) {
                if(CurrentApp.BackpackPlannerState.Settings.MetaSettings.FirstRun) {
                    Logger.Debug("Starting FTUE...");
                    rootFrame.Navigate(typeof(FTUEPage), e.Arguments);
                } else {
                    Logger.Debug("Starting main activity...");
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                BackpackPlannerState.Settings.MetaSettings.FirstRun = false;
            }
            Window.Current.Activate();
        }

        private Frame InitNewInstance(LaunchActivatedEventArgs e)
        {
            Logger.Debug("Initializing new instance...");

            Frame rootFrame = new Frame();
            rootFrame.NavigationFailed += OnNavigationFailed;

            if(e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                //TODO: Load state from previously suspended application
            }

            // Place the frame in the current Window
            Window.Current.Content = rootFrame;

            LoadPreferences();

            return rootFrame;
        }

        private void LoadPreferences()
        {
            Logger.Debug("Loading preferences...");
            BackpackPlannerState.PlatformSettingsManager.Load(BackpackPlannerState.Settings, BackpackPlannerState.PersonalInformation);
        }
    }
}

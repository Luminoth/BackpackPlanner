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
using System.Threading.Tasks;
using EnergonSoftware.BackpackPlanner.Core.Database;
using Foundation;
using HockeyApp;
using SQLite.Net.Platform.XamarinIOS;
using UIKit;

namespace EnergonSoftware.BackpackPlanner.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
        private const string HockeyAppAppId = "YOUR-HOCKEYAPP-APPID";

		// class-level declarations

		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
            InitHockeyApp();

            BackpackPlannerState.Instance.DatabaseState.ConnectAsync(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseState.DatabaseName).Wait();
            BackpackPlannerState.Instance.DatabaseState.InitDatabaseAsync().Wait();

			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method
			return true;
		}

		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

        private void InitHockeyApp()
        {
            //We MUST wrap our setup in this block to wire up
            // Mono's SIGSEGV and SIGBUS signals
            Setup.EnableCustomCrashReporting(() => {

                //Get the shared instance
                BITHockeyManager manager = BITHockeyManager.SharedHockeyManager;

                //Configure it to use our APP_ID
                manager.Configure(HockeyAppAppId);

                //Start the manager
                manager.StartManager();

                //Authenticate (there are other authentication options)
                manager.Authenticator.AuthenticateInstallation();

                //Rethrow any unhandled .NET exceptions as native iOS 
                // exceptions so the stack traces appear nicely in HockeyApp
                AppDomain.CurrentDomain.UnhandledException += (sender, args) => Setup.ThrowExceptionAsNative(args.ExceptionObject);

                TaskScheduler.UnobservedTaskException += (sender, args) => Setup.ThrowExceptionAsNative(args.Exception);
            });
        }
	}
}

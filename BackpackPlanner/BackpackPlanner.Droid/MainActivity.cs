using System;
using System.Threading.Tasks;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid
{
	[Activity(Label = "Backpacking Planner", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        private const string HockeyAppAppId = "YOUR-HOCKEYAPP-APPID";

		private int _count = 1;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

            InitHockeyApp();

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);
			
			button.Click += (sender, args) => {
				button.Text = $"{_count++} clicks!";
			};
		}

        private void InitHockeyApp()
        {
            // Register the crash manager before Initializing the trace writer
            HockeyApp.CrashManager.Register(this, HockeyAppAppId); 

            // Register to with the Update Manager
            HockeyApp.UpdateManager.Register(this, HockeyAppAppId);

            // Initialize the Trace Writer
            HockeyApp.TraceWriter.Initialize();

            // Wire up Unhandled Expcetion handler from Android
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => {
                // Use the trace writer to log exceptions so HockeyApp finds them
                HockeyApp.TraceWriter.WriteTrace(args.Exception);
                args.Handled = true;
            };

            // Wire up the .NET Unhandled Exception handler
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => HockeyApp.TraceWriter.WriteTrace(args.ExceptionObject);

            // Wire up the unobserved task exception handler
            TaskScheduler.UnobservedTaskException += (sender, args) => HockeyApp.TraceWriter.WriteTrace(args.Exception);
        }
	}
}



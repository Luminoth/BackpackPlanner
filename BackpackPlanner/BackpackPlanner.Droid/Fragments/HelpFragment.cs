using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

using HockeyApp;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public class HelpFragment : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_help, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Button buttonFeedback = view.FindViewById<Button>(Resource.Id.button_feedback);
            buttonFeedback.Click += (sender, args) => {
                Log.Debug(MainActivity.LogTag, "Showing feedback activity");
                FeedbackManager.ShowFeedbackActivity(Activity);
            };
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity.Title = Resources.GetString(Resource.String.title_help);
        }
    }
}

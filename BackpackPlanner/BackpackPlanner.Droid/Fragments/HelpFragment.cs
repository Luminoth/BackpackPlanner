using Android.OS;
using Android.Views;
using Android.Widget;

using HockeyApp;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public class HelpFragment : Android.Support.V4.App.Fragment
    {
        Button _buttonHelp;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_help, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _buttonHelp = view.FindViewById<Button>(Resource.Id.button_feedback);
            _buttonHelp.Click += (sender, args) => {
                FeedbackManager.ShowFeedbackActivity(Activity);
            };
        }
    }
}
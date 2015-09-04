using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Util;
using EnergonSoftware.BackpackPlanner.Models.Personal;

// http://stackoverflow.com/questions/30114730/how-to-use-appcompatpreferenceactivity
// http://developer.android.com/guide/topics/ui/settings.html

namespace EnergonSoftware.BackpackPlanner.Droid
{
    [Activity(Label = "@string/title_settings")]
    public class SettingsActivity : AppCompatPreferenceActivity
    {
        private Android.Support.V7.Widget.Toolbar _toolBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //InitToolBar();

            AddPreferencesFromResource(Resource.Xml.settings);
        }

        private void InitToolBar()
        {
            ViewGroup rootViewGroup = (ViewGroup)FindViewById(Android.Resource.Id.List).Parent.Parent.Parent;
            _toolBar = (Android.Support.V7.Widget.Toolbar)LayoutInflater.From(this).Inflate(Resource.Layout.toolbar, rootViewGroup, false);
            rootViewGroup.AddView(_toolBar, 0);

            SetSupportActionBar(_toolBar);
        }
    }
}

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

using Android.OS;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public sealed class HelpFragment : BaseFragment
    {
        protected override int LayoutResource => Resource.Layout.fragment_help;

        protected override int TitleResource => Resource.String.title_help;

        protected override bool HasSearchView => false;

        protected override bool CanExport => false;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Button buttonFeedback = view.FindViewById<Button>(Resource.Id.button_feedback);
            buttonFeedback.Click += (sender, args) => 
            {
                ((HockeyAppManager)BaseActivity.BackpackPlannerState.PlatformHockeyAppManager).ShowFeedback(Activity);
            };
        }
    }
}

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

using Android.App;
using Android.OS;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Dialogs
{
    public class WhatIsADialog : Android.Support.V4.App.DialogFragment
    {
        private readonly int _messageResourceId;

        private readonly int _titleResourceId;

        public WhatIsADialog(int messageResourceId, int titleResourceId)
        {
            _messageResourceId = messageResourceId;
            _titleResourceId = titleResourceId;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(Activity);
            builder.SetMessage(_messageResourceId)
                .SetTitle(_titleResourceId)
                .SetPositiveButton(Android.Resource.String.Ok, (sender, args) => {
                        // nothing to do
                    }
                );
            return builder.Create();
        }
    }
}
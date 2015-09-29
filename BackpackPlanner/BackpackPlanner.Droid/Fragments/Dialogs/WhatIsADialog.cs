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
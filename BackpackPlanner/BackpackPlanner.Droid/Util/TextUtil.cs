/*
   Copyright 2017 Shane Lillie

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
using Android.Text;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Util
{
    public static class TextUtil
    {
        public static ISpanned GetHtmlString(Activity activity, int stringResId)
        {
            return Html.FromHtml(activity.GetString(stringResId), FromHtmlOptions.ModeLegacy);
        }

        public static TextView GetHtmlText(Activity activity, int stringResId)
        {
            TextView textView = new TextView(activity);
            textView.SetText(GetHtmlString(activity, stringResId), TextView.BufferType.Spannable);
            return textView;
        }
    }
}
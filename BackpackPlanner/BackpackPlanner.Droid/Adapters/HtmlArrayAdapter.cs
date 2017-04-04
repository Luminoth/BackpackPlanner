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

using System.Collections.Generic;

using Android.Content;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public class HtmlArrayAdapter<T> : ArrayAdapter<T>
    {
#region Constructors
        public HtmlArrayAdapter(Context context, int textViewResourceId)
            : base(context, textViewResourceId)
        {
        }

        public HtmlArrayAdapter(Context context, int resource, int textViewResourceId)
            : base(context, resource, textViewResourceId)
        {
        }

        public HtmlArrayAdapter(Context context, int textViewResourceId, T[] objects)
            : base(context, textViewResourceId, objects)
        {
        }

        public HtmlArrayAdapter(Context context, int resource, int textViewResourceId, T[] objects)
            : base(context, resource, textViewResourceId, objects)
        {
        }

        public HtmlArrayAdapter(Context context, int textViewResourceId, IList<T> objects)
            : base(context, textViewResourceId, objects)
        {
        }

        public HtmlArrayAdapter(Context context, int resource, int textViewResourceId, IList<T> objects)
            : base(context, resource, textViewResourceId, objects)
        {
        }
#endregion

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View baseView = base.GetView(position, convertView, parent);
            TextView textView = baseView.FindViewById<TextView>(Android.Resource.Id.Text1);
            textView.SetText(Html.FromHtml(GetItem(position).ToString(), FromHtmlOptions.ModeLegacy), TextView.BufferType.Spannable);
            return baseView;
        }
    }
}
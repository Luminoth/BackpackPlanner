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

using Android.Content;
using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseListViewAdapter<T> : ArrayAdapter<T>
    {
        public abstract int LayoutResource { get; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            BaseViewHolder<T> viewHolder;
            if(convertView == null) {
                convertView = LayoutInflater.From(Context).Inflate(LayoutResource, parent, false);
                viewHolder = CreateViewHolder(convertView);
                convertView.Tag = viewHolder;
            } else {
                viewHolder = (BaseViewHolder<T>)convertView.Tag;
            }

            BindViewHolder(viewHolder, position);
            return convertView;
        }

        protected abstract BaseViewHolder<T> CreateViewHolder(View view);

        protected virtual void BindViewHolder(BaseViewHolder<T> viewHolder, int position)
        {
            T item = GetItem(position);
            viewHolder.UpdateView(item);
        }

        protected BaseListViewAdapter(Context context)
            : base(context, 0)
        {
        }

        protected BaseListViewAdapter(Context context, T[] items)
            : base(context, 0, items)
        {
        }
    }
}

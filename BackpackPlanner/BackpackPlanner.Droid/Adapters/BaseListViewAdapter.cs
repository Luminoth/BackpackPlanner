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

using Android.Views;
using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Fragments;

namespace EnergonSoftware.BackpackPlanner.Droid.Adapters
{
    public abstract class BaseListViewAdapter<T> : ArrayAdapter<T>
    {
        protected abstract class ViewHolder : Java.Lang.Object
        {
            public View View { get; }

            protected BaseListViewAdapter<T> Adapter { get; }

            private T _listItem;

            public T ListItem
            {
                get { return _listItem; }

                set
                {
                    _listItem = value;
                    UpdateView();
                }
            }

            protected abstract void UpdateView();
                
            protected ViewHolder(View itemView, BaseListViewAdapter<T> adapter)
            {
                View = itemView;
                Adapter = adapter;
            }
        }

        public BaseFragment Fragment { get; }

        public abstract int LayoutResource { get; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder;
            if(convertView == null) {
                convertView = LayoutInflater.From(Context).Inflate(LayoutResource, parent, false);
                viewHolder = CreateViewHolder(convertView);
                convertView.Tag = viewHolder;
            } else {
                viewHolder = (ViewHolder)convertView.Tag;
            }

            BindViewHolder(viewHolder, position);
            return convertView;
        }

#region ViewHolder
        protected abstract ViewHolder CreateViewHolder(View itemView);

        private void BindViewHolder(ViewHolder viewHolder, int position)
        {
            T item = GetItem(position);
            viewHolder.ListItem = item;
        }
#endregion

        protected BaseListViewAdapter(BaseFragment fragment)
            : base(fragment.Context, 0)
        {
            Fragment = fragment;
        }

        protected BaseListViewAdapter(BaseFragment fragment, T[] items)
            : base(fragment.Context, 0, items)
        {
            Fragment = fragment;
        }
    }
}

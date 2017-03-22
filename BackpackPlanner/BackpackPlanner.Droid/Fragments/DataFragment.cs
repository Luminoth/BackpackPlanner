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

using Android.Widget;

using EnergonSoftware.BackpackPlanner.Droid.Adapters;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data entry fragments
    /// </summary>
    public abstract class DataFragment : BaseFragment
    {
        protected sealed class FilterListener<T> : Java.Lang.Object, Filter.IFilterListener
        {
            private readonly BaseListViewAdapter<T> _adapter;

            public FilterListener(BaseListViewAdapter<T> adapter)
            {
                _adapter = adapter;
            }

            public void OnFilterComplete(int count)
            {
                // TODO: need a method to build an IComparator
                //_adapter.Sort();
            }
        }

        public bool DoDataExchange()
        {
            if(!OnValidate()) {
                return false;
            }

            OnDoDataExchange();

            return true;
        }

        protected abstract bool OnValidate();

        protected abstract void OnDoDataExchange();

        protected abstract void OnReset();
    }
}

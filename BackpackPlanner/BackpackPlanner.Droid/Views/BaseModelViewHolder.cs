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

using System.ComponentModel;
using System.Runtime.CompilerServices;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models;
using EnergonSoftware.BackpackPlanner.Droid.Activities;

namespace EnergonSoftware.BackpackPlanner.Droid.Views
{
    public abstract class BaseModelViewHolder<T> : BaseViewHolder<T>, INotifyPropertyChanged
        where T: BaseModel<T>, new()
    {
#region Events
        public event PropertyChangedEventHandler PropertyChanged;
#endregion

        public bool PropertyChangedNotificationEnabled { get; set; }

        public virtual bool Validate()
        {
            return true;
        }

        public virtual void DoDataExchange(T item, DatabaseContext dbContext)
        {
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            if(!PropertyChangedNotificationEnabled) {
                return;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected BaseModelViewHolder(BaseActivity activity)
            : base(activity)
        {
        }
    }
}
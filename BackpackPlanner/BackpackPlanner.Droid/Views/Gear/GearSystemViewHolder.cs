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

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Systems;
using EnergonSoftware.BackpackPlanner.Droid.Activities;

namespace EnergonSoftware.BackpackPlanner.Droid.Views.Gear
{
    public sealed class GearSystemViewHolder : BaseModelViewHolder<GearSystem>
    {
        private readonly Android.Support.Design.Widget.TextInputLayout _gearSystemNameEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearSystemNoteEditText;

        public GearSystemViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _gearSystemNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_system_name);
            _gearSystemNameEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Name");
            };

            _gearSystemNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_system_note);
            _gearSystemNoteEditText.EditText.AfterTextChanged += (sender, args) =>
            {
                NotifyPropertyChanged("Note");
            };
        }

        public override void UpdateView(GearSystem gearSystem)
        {
            base.UpdateView(gearSystem);

            _gearSystemNameEditText.EditText.Text = gearSystem.Name;
            _gearSystemNoteEditText.EditText.Text = gearSystem.Note;
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

            if(string.IsNullOrWhiteSpace(_gearSystemNameEditText.EditText.Text)) {
                _gearSystemNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        public override void DoDataExchange(GearSystem gearSystem, DatabaseContext dbContext)
        {
            gearSystem.Name = _gearSystemNameEditText.EditText.Text;
            gearSystem.Note = _gearSystemNoteEditText.EditText.Text;

            base.DoDataExchange(gearSystem, dbContext);
        }
    }
}
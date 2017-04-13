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
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;
using EnergonSoftware.BackpackPlanner.Droid.Activities;

namespace EnergonSoftware.BackpackPlanner.Droid.Views.Gear
{
    public sealed class GearCollectionViewHolder : BaseModelViewHolder<GearCollection>
    {
        private readonly Android.Support.Design.Widget.TextInputLayout _gearCollectionNameEditText;
        private readonly Android.Support.Design.Widget.TextInputLayout _gearCollectionNoteEditText;

        public GearCollectionViewHolder(BaseActivity activity, View view)
            : base(activity)
        {
            _gearCollectionNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_collection_name);
            _gearCollectionNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.gear_collection_note);
        }

        public override void UpdateView(GearCollection gearCollection)
        {
            base.UpdateView(gearCollection);

            _gearCollectionNameEditText.EditText.Text = gearCollection.Name;
            _gearCollectionNoteEditText.EditText.Text = gearCollection.Note;
        }

        public override bool Validate()
        {
            bool valid = base.Validate();

            if(string.IsNullOrWhiteSpace(_gearCollectionNameEditText.EditText.Text)) {
                _gearCollectionNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        public override void DoDataExchange(GearCollection gearCollection, DatabaseContext dbContext)
        {
            gearCollection.Name = _gearCollectionNameEditText.EditText.Text;
            gearCollection.Note = _gearCollectionNoteEditText.EditText.Text;
        }
    }
}
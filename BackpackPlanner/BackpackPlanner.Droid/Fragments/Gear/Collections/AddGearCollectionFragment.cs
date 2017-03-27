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

using System.Threading.Tasks;

using Android.OS;
using Android.Views;

using EnergonSoftware.BackpackPlanner.DAL;
using EnergonSoftware.BackpackPlanner.DAL.Models.Gear.Collections;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments.Gear.Collections
{
    public sealed class AddGearCollectionFragment : AddItemFragment<GearCollection>
    {
        protected override int LayoutResource => Resource.Layout.fragment_add_gear_collection;

        protected override int TitleResource => Resource.String.title_add_gear_collection;

        protected override int AddItemResource => Resource.Id.fab_add_gear_collection;

        protected override int ResetItemResource => Resource.Id.fab_reset_gear_collection;

#region Controls
        private Android.Support.Design.Widget.TextInputLayout _gearCollectionNameEditText;
        private Android.Support.Design.Widget.TextInputLayout _gearCollectionNoteEditText;
#endregion

        protected override GearCollection CreateItem()
        {
            return new GearCollection(DroidState.Instance.BackpackPlannerState.Settings);
        }

        protected override async Task AddItemAsync(DatabaseContext dbContext)
        {
            await dbContext.GearCollections.AddAsync(Item).ConfigureAwait(false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _gearCollectionNameEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_collection_name);
            _gearCollectionNoteEditText = view.FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.add_gear_collection_note);
        }

        protected override void OnDoDataExchange()
        {
            Item.Name = _gearCollectionNameEditText.EditText.Text;
            Item.Note = _gearCollectionNoteEditText.EditText.Text;
        }

        protected override bool OnValidate()
        {
            bool valid = true;

            if(string.IsNullOrWhiteSpace(_gearCollectionNameEditText.EditText.Text)) {
                _gearCollectionNameEditText.EditText.Error = "A name is required!";
                valid = false;                
            }

            return valid;
        }

        protected override void OnReset()
        {
// TODO
        }
    }
}

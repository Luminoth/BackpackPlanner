using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EnergonSoftware.BackpackPlanner.Models.Personal;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    // TODO: move this into the settings page
    public class PersonalInformationFragment : Android.Support.V4.App.Fragment
    {
        EditText _editName;
        Button _buttonSave;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PersonalInformation personalInformation = BackpackPlannerState.Instance.PersonalInformation;

            ISharedPreferences preferences = Activity.GetSharedPreferences("PersonalInformation", FileCreationMode.Private);
            ISharedPreferencesEditor preferencesEditor = preferences.Edit();

            if(!preferences.Contains("Name")) {
                preferencesEditor.PutString("Name", personalInformation.Name);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_personal_information, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            PersonalInformation personalInformation = BackpackPlannerState.Instance.PersonalInformation;

            _editName = view.FindViewById<EditText>(Resource.Id.name);
            _editName.Text = personalInformation.Name;

            _buttonSave = view.FindViewById<Button>(Resource.Id.button_save_personal_information);
        }
    }
}

using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using EnergonSoftware.BackpackPlanner.Models.Personal;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    public class PersonalInformationFragment : Android.Support.V4.App.Fragment
    {
        EditText _editFirstName, _editLastName;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PersonalInformation personalInformation = BackpackPlannerState.Instance.PersonalInformation;

            ISharedPreferences preferences = Activity.GetSharedPreferences("PersonalInformation", FileCreationMode.Private);
            ISharedPreferencesEditor preferencesEditor = preferences.Edit();

            if(!preferences.Contains("FirstName")) {
                preferencesEditor.PutString("FirstName", personalInformation.FirstName);
            }

            if(!preferences.Contains("LastName")) {
                preferencesEditor.PutString("LastName", personalInformation.LastName);
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

            _editFirstName = view.FindViewById<EditText>(Resource.Id.first_name);
            _editFirstName.Text = personalInformation.FirstName;

            _editLastName = view.FindViewById<EditText>(Resource.Id.last_name);
            _editLastName.Text = personalInformation.LastName;
        }
    }
}

using Android.Views;

namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the recycler fragments
    /// </summary>
    public abstract class RecyclerFragment : BaseFragment
    {
        protected Android.Support.V7.Widget.RecyclerView Layout { get; set; }

        protected Android.Support.V7.Widget.RecyclerView.LayoutManager LayoutManager { get; set; }

        protected void InitLayout(View view, int layoutResId)
        {
            Layout = view.FindViewById<Android.Support.V7.Widget.RecyclerView>(layoutResId);
            LayoutManager = new Android.Support.V7.Widget.LinearLayoutManager(Activity);
            Layout.SetLayoutManager(LayoutManager);
        }
    }
}

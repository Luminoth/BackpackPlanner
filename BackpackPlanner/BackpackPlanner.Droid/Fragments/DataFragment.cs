namespace EnergonSoftware.BackpackPlanner.Droid.Fragments
{
    /// <summary>
    /// Helper for the data entry fragments
    /// </summary>
    public abstract class DataFragment : BaseFragment
    {
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
    }
}

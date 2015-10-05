using System;

namespace EnergonSoftware.BackpackPlanner.Settings
{
    public sealed class SettingsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the preference key that changed.
        /// </summary>
        /// <value>
        /// The preference key that changed.
        /// </value>
        public string PreferenceKey { get; set; }

        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        /// <value>
        /// The old value.
        /// </value>
        public string OldValue { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        /// <value>
        /// The new value.
        /// </value>
        public string NewValue { get; set; }
    }
}

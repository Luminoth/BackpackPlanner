using System;

namespace EnergonSoftware.BackpackPlanner.Util
{
    /// <summary>
    /// Imlementation of DescriptionAttribute for PCL
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}

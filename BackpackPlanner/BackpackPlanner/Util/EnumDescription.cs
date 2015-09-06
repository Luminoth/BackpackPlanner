using System;
using System.Linq;
using System.Reflection;

namespace EnergonSoftware.BackpackPlanner.Util
{
    /// <summary>
    /// Enumeration description utilities
    /// </summary>
    /// <remarks>
    /// Taken from http://stackoverflow.com/questions/4367723/get-enum-from-description-attribute
    /// </remarks>
    public static class EnumDescription
    {
        private static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            TypeInfo typeInfo = enumVal.GetType().GetTypeInfo();
            MemberInfo v = typeInfo.DeclaredMembers.First(x => x.Name == enumVal.ToString());
            return v.GetCustomAttribute<T>();
        }

        /// <summary>
        /// Gets the description from an enum value.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description of the enum value</returns>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            if(null == value) {
                throw new ArgumentNullException(nameof(value));
            }

            DescriptionAttribute attribute = value.GetAttributeOfType<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}

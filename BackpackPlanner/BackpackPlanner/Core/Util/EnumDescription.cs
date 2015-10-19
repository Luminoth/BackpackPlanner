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

using System;
using System.Linq;
using System.Reflection;

namespace EnergonSoftware.BackpackPlanner.Core.Util
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

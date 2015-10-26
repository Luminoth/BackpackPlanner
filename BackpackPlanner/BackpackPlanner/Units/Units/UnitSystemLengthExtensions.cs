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

namespace EnergonSoftware.BackpackPlanner.Units.Units
{
    /// <summary>
    /// Length-related extensions for the UnitSystem enumeration
    /// </summary>
    public static class UnitSystemLengthExtensions
    {
        /// <summary>
        /// Gets the small length string for the unit system.
        /// </summary>
        /// <param name="unitSystem">The unit system.</param>
        /// <returns>The small length string for the unit system</returns>
        public static string GetSmallLengthString(this UnitSystem unitSystem)
        {
            switch(unitSystem)
            {
            case UnitSystem.Metric:
                return "centimeters";
            case UnitSystem.UnitedStates:
                return "inches";
            default:
                throw new InvalidOperationException("Invalid unit system!");
            }
        }

        /// <summary>
        /// Converts centimeters to the unit system length.
        /// </summary>
        /// <param name="unitSystem">The unit system.</param>
        /// <param name="centimeters">The centimeters.</param>
        /// <returns>The unit system length from the given centimeters</returns>
        /// <exception cref="System.InvalidOperationException">Invalid unit system!</exception>
        public static float LengthFromCentimeters(this UnitSystem unitSystem, int centimeters)
        {
            switch(unitSystem)
            {
            case UnitSystem.Metric:
                return centimeters;
            case UnitSystem.UnitedStates:
                return UnitConversion.CentimetersToInches(centimeters);
            default:
                throw new InvalidOperationException("Invalid unit system!");
            }
        }

        /// <summary>
        /// Converts the unit system length to centimeters.
        /// </summary>
        /// <param name="unitSystem">The unit system.</param>
        /// <param name="length">The unit system length.</param>
        /// <returns>The centimeters from the given unit system length</returns>
        /// <exception cref="System.InvalidOperationException">Invalid unit system!</exception>
        public static int CentimetersFromLength(this UnitSystem unitSystem, float length)
        {
            switch(unitSystem)
            {
            case UnitSystem.Metric:
                return (int)length;
            case UnitSystem.UnitedStates:
                return UnitConversion.InchesToCentimeters(length);
            default:
                throw new InvalidOperationException("Invalid unit system!");
            }
        }
    }
}

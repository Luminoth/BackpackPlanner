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

namespace EnergonSoftware.BackpackPlanner.Units
{
    /// <summary>
    /// Weight-related extensions for the UnitSystem enumeration
    /// </summary>
    public static class UnitSystemWeightExtensions
    {
        /// <summary>
        /// Gets the small weight string for the unit system.
        /// </summary>
        /// <param name="unitSystem">The unit system.</param>
        /// <returns>The small weight string for the unit system</returns>
        public static string GetSmallWeightString(this UnitSystem unitSystem)
        {
            switch(unitSystem)
            {
            case UnitSystem.Metric:
                return "grams";
            case UnitSystem.UnitedStates:
                return "ounces";
            default:
                throw new InvalidOperationException("Invalid unit system!");
            }
        }

        /// <summary>
        /// Converts grams to the unit system weight.
        /// </summary>
        /// <param name="unitSystem">The unit system.</param>
        /// <param name="grams">The grams.</param>
        /// <returns>The unit system weight from the given grams</returns>
        /// <exception cref="System.InvalidOperationException">Invalid unit system!</exception>
        public static double WeightFromGrams(this UnitSystem unitSystem, int grams)
        {
            switch(unitSystem)
            {
            case UnitSystem.Metric:
                return grams;
            case UnitSystem.UnitedStates:
                return UnitConversion.GramsToOunces(grams);
            default:
                throw new InvalidOperationException("Invalid unit system!");
            }
        }

        /// <summary>
        /// Converts the unit system weight to grams.
        /// </summary>
        /// <param name="unitSystem">The unit system.</param>
        /// <param name="weight">The unit system weight.</param>
        /// <returns>The grams from the given unit system weight</returns>
        /// <exception cref="System.InvalidOperationException">Invalid unit system!</exception>
        public static double GramsFromWeight(this UnitSystem unitSystem, double weight)
        {
            switch(unitSystem)
            {
            case UnitSystem.Metric:
                return weight;
            case UnitSystem.UnitedStates:
                return UnitConversion.OuncesToGrams(weight);
            default:
                throw new InvalidOperationException("Invalid unit system!");
            }
        }
    }
}

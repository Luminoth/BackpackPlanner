﻿/*
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

namespace EnergonSoftware.BackpackPlanner.Units
{
    /// <summary>
    /// Unit conversion utilities
    /// </summary>
    public static class UnitConversion
    {
#region Weight
        /// <summary>
        /// Converts grams to ounces.
        /// </summary>
        /// <param name="grams">The grams.</param>
        /// <returns>The grams in ounces</returns>
        public static double GramsToOunces(int grams)
        {
            return grams * 0.035274;
        }

        /// <summary>
        /// Converts ounces to grams.
        /// </summary>
        /// <param name="ounces">The ounces.</param>
        /// <returns>The ounces in grams</returns>
        public static double OuncesToGrams(double ounces)
        {
            return ounces * 28.3495;
        }
#endregion

#region Height
        /// <summary>
        /// Converts centimeters to inches.
        /// </summary>
        /// <param name="centimeters">The centimeters.</param>
        /// <returns>The centimeters in inches</returns>
        public static double CentimetersToInches(int centimeters)
        {
            return centimeters * 0.393701;
        }

        /// <summary>
        /// Converts inches to centimeters.
        /// </summary>
        /// <param name="inches">The inches.</param>
        /// <returns>The inches in centimeters</returns>
        public static double InchesToCentimeters(double inches)
        {
            return inches * 2.54;
        }
    }
#endregion
}

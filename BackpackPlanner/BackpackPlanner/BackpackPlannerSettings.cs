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

namespace EnergonSoftware.BackpackPlanner
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BackpackPlannerSettings
    {
        /// <summary>
        /// Gets or sets the unit system to use.
        /// </summary>
        /// <value>
        /// The unit system to use.
        /// </value>
        public UnitSystem Units { get; set; } = UnitSystem.Metric;

        /// <summary>
        /// Gets or sets the currency to use.
        /// </summary>
        /// <value>
        /// The currency to use.
        /// </value>
        public Currency Currency { get; set; } = Currency.UnitedStatesDollar;

#region Weight Categories
        /// <summary>
        /// Gets or sets the ultralight weight category maximum weight in grams.
        /// </summary>
        /// <value>
        /// The ultralight weight category maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 225 grams is about 8 ounces.
        /// </remarks>
        public int UltralightWeightCategoryMaxWeightInGrams { get; set; } = 225;

        /// <summary>
        /// Gets or sets the light weight category maximum weight in grams.
        /// </summary>
        /// <value>
        /// The light weight category maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 450 grams is about 16 ounces.
        /// </remarks>
        public int LightWeightCategoryMaxWeightInGrams { get; set; } = 450;

        /// <summary>
        /// Gets or sets the medium weight category maximum weight in grams.
        /// </summary>
        /// <value>
        /// The medium weight category maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 1360 grams is about 3 pounds.
        /// </remarks>
        public int MediumWeightCategoryMaxWeightInGrams { get; set; } = 1360;

        /// <summary>
        /// Gets or sets the heavy weight category maximum weight in grams.
        /// </summary>
        /// <value>
        /// The heavy weight category maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 2270 grams is about 5 pounds.
        /// </remarks>
        public int HeavyWeightCategoryMaxWeightInGrams { get; set; } = 2270;
#endregion

#region Weight Classes
        /// <summary>
        /// Gets or sets the ultralight class maximum weight in grams.
        /// </summary>
        /// <value>
        /// The ultralight class maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 4500 grams is about 10 pounds.
        /// </remarks>
        public int UltralightClassMaxWeightInGrams { get; set; } = 4500;

        /// <summary>
        /// Gets or sets the lightweight class maximum weight in grams.
        /// </summary>
        /// <value>
        /// The lightweight class maximum weight in grams.
        /// </value>
        /// <remarks>
        /// 9000 grams is about 20 pounds.
        /// </remarks>
        public int LightweightClassMaxWeightInGrams { get; set; } = 9000;
#endregion
    }
}

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

using EnergonSoftware.BackpackPlanner.Util;

namespace EnergonSoftware.BackpackPlanner.Models.Meals
{
    /// <summary>
    /// 
    /// </summary>
    public enum MealTime
    {
        /// <summary>
        /// Some other meal type
        /// </summary>
        [Description("Other")]
        Other,

        /// <summary>
        /// 
        /// </summary>
        [Description("Drink")]
        Drink,

        /// <summary>
        /// The breakfast meal time
        /// </summary>
        [Description("Breakfast")]
        Breakfast,

        /// <summary>
        /// The lunch meal time
        /// </summary>
        [Description("Lunch")]
        Lunch,

        /// <summary>
        /// The dinner meal time
        /// </summary>
        [Description("Dinner")]
        Dinner,

        /// <summary>
        /// The snack meal time
        /// </summary>
        [Description("Snack")]
        Snack
    }
}

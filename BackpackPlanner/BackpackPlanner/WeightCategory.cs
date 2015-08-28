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
    public enum WeightCategory
    {
        /// <summary>
        /// No weight category (0 "or less" grams)
        /// </summary>
        None,

        /// <summary>
        /// The ultralight weight category
        /// </summary>
        Ultralight,

        /// <summary>
        /// The light weight category
        /// </summary>
        Light,

        /// <summary>
        /// The medium weight category
        /// </summary>
        Medium,

        /// <summary>
        /// The heavy weight category
        /// </summary>
        Heavy,

        /// <summary>
        /// The extra heavy weight category
        /// </summary>
        ExtraHeavy
    }
}

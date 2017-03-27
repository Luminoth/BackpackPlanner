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

using EnergonSoftware.BackpackPlanner.Core.Util;

namespace EnergonSoftware.BackpackPlanner.DAL.Models.Personal
{
    /// <summary>
    /// 
    /// </summary>
    public enum UserSex
    {
        /// <summary>
        /// The user has decided not to declare their sex
        /// </summary>
        [Description("Undeclared")]
        Undeclared = 0,

        /// <summary>
        /// The user is male
        /// </summary>
        [Description("Male")]
        Male = 1,

        /// <summary>
        /// The user is female
        /// </summary>
        [Description("Female")]
        Female = 2,

        /// <summary>
        /// The user is some other sex
        /// </summary>
        [Description("Other")]
        Other = 3
    }
}

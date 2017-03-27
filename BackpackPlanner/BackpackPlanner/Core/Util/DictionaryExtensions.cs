/*
   Copyright 2016 Shane Lillie

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

using System.Collections.Generic;

namespace EnergonSoftware.BackpackPlanner.Core.Util
{
    public static class DictionaryExtensions
    {
        public static TV GetOrDefault<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, TV defaultValue=default(TV))
        {
            return dictionary.TryGetValue(key, out TV value) ? value : defaultValue;
        }

        public static TV GetAndRemove<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, TV defaultValue=default(TV))
        {
            if (!dictionary.TryGetValue(key, out TV value)) {
                return defaultValue;
            }

            dictionary.Remove(key);
            return value;
        }
    }
}

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
        public static V GetOrDefault<K, V>(this Dictionary<K, V> dictionary, K key, V defaultValue=default(V))
        {
            V value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static V GetAndRemove<K, V>(this Dictionary<K, V> dictionary, K key, V defaultValue=default(V))
        {
            V value;
            if(!dictionary.TryGetValue(key, out value)) {
                return defaultValue;
            }

            dictionary.Remove(key);
            return value;
        }
    }
}

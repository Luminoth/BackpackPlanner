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

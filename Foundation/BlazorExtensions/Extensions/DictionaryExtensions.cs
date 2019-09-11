using System.Collections.Generic;

namespace Foundation.BlazorExtensions.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<T, U> AddRange<T, U>(this Dictionary<T, U> currentDictionary, Dictionary<T, U> source)
        {
            if (currentDictionary == null) currentDictionary = new Dictionary<T, U>();

            foreach (var e in source)
                currentDictionary.Add(e.Key, e.Value);
            
            return currentDictionary;
        }
    }
}
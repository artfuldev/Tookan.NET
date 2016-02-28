using System.Collections.Generic;
using Tookan.NET.Sanity;

namespace Tookan.NET.Helpers
{
    internal static class CollectionExtensions
    {
        public static TValue SafeGet<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            Ensure.ArgumentIsNotNull(dictionary, "dictionary");
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }
    }
}

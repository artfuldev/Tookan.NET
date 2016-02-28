using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Tookan.NET
{
    public static class Extensions
    {
        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> source)
        {
            if(ReferenceEquals(null, source)) throw new ArgumentNullException(nameof(source));
            return new ReadOnlyCollection<T>(source.ToList());
        }

        public static async Task<List<T>> ToListAsync<T>(this IEnumerable<T> source)
        {
            if (ReferenceEquals(null, source)) throw new ArgumentNullException(nameof(source));
            // Modify for within API
            return await Task.FromResult(source.ToList());
        }  
    }
}

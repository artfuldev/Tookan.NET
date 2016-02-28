using System.Collections.Generic;
using System.Text;
using Tookan.NET.Sanity;

namespace Tookan.NET.Serialization
{
    internal static class StringExtensions
    {
        public static string ToSnakeCase(this string value)
        {
            Ensure.ArgumentIsNotNullOrEmptyString(value, nameof(value));

            var parts = new List<string>();
            var currentWord = new StringBuilder();
            foreach (var c in value)
            {
                if (char.IsUpper(c) && currentWord.Length > 0)
                {
                    parts.Add(currentWord.ToString());
                    currentWord.Clear();
                }
                currentWord.Append(char.ToLower(c));
            }
            if (currentWord.Length > 0)
            {
                parts.Add(currentWord.ToString());
            }
            return string.Join("_", parts.ToArray());
        }
    }
}
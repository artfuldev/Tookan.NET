using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Tookan.NET.Sanity;

namespace Tookan.NET.Helpers
{
    internal static class StringExtensions
    {
        public static bool IsBlank(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotBlank(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static Uri FormatUri(this string pattern, params object[] args)
        {
            Ensure.ArgumentIsNotNullOrEmptyString(pattern, "pattern");

            return new Uri(string.Format(CultureInfo.InvariantCulture, pattern, args), UriKind.Relative);
        }

        public static string UriEncode(this string input)
        {
            return WebUtility.UrlEncode(input);
        }

        public static string ToBase64String(this string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public static string FromBase64String(this string encoded)
        {
            var decodedBytes = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(decodedBytes, 0, decodedBytes.Length);
        }

        static readonly Regex OptionalQueryStringRegex = new Regex("\\{\\?([^}]+)\\}");
        public static Uri ExpandUriTemplate(this string template, object values)
        {
            var optionalQueryStringMatch = OptionalQueryStringRegex.Match(template);
            if(optionalQueryStringMatch.Success)
            {
                var expansion = "";
                var parameterName = optionalQueryStringMatch.Groups[1].Value;
                var parameterProperty = values.GetType().GetProperty(parameterName);
                if(parameterProperty != null)
                {
                    expansion = "?" + parameterName + "=" + Uri.EscapeDataString("" + parameterProperty.GetValue(values, new object[0]));
                }
                template = OptionalQueryStringRegex.Replace(template, expansion);
            }
            return new Uri(template);
        }

        // Or Snake Case
        public static string ToRubyCase(this string propertyName)
        {
            Ensure.ArgumentIsNotNullOrEmptyString(propertyName, "s");
            return string.Join("_", propertyName.SplitUpperCase()).ToLowerInvariant();
        }

        static IEnumerable<string> SplitUpperCase(this string source)
        {
            Ensure.ArgumentIsNotNullOrEmptyString(source, "source");

            int wordStartIndex = 0;
            var letters = source.ToCharArray();
            var previousChar = char.MinValue;

            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar))
                {
                    //Grab everything before the current character.
                    yield return new string(letters, wordStartIndex, i - wordStartIndex);
                    wordStartIndex = i;
                }
                previousChar = letters[i];
            }

            //We need to have the last word.
            yield return new string(letters, wordStartIndex, letters.Length - wordStartIndex);
        }
    }
}

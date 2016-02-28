using System;
using System.Collections.Generic;
using System.Linq;
using Tookan.NET.Sanity;

namespace Tookan.NET.Helpers
{
    /// <summary>
    /// Extensions for working with Uris
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Merge a dictionary of valeus with an existing <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">Original request Uri</param>
        /// <param name="parameters">Collection of key-value pairs</param>
        /// <returns>Updated request Uri</returns>
        public static Uri ApplyParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            // Expand links for every request
            if (parameters == null || !parameters.Any())
                parameters = new Dictionary<string, string>();
            parameters["expand"] = "links";

            // to prevent values being persisted across requests
            // use a temporary dictionary which combines new and existing parameters
            IDictionary<string, string> p = parameters.ToDictionary(x => x.Key,
                x => x.Key == "q" ? x.Value : Uri.EscapeDataString(x.Value));

            string queryString;
            if (uri.IsAbsoluteUri)
            {
                queryString = uri.Query;
            }
            else
            {
                var hasQueryString = uri.OriginalString.IndexOf("?", StringComparison.Ordinal);
                queryString = hasQueryString == -1
                    ? ""
                    : uri.OriginalString.Substring(hasQueryString);
            }

            var values = queryString.Replace("?", "")
                                    .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            var existingParameters = values.ToDictionary(
                        key => key.Substring(0, key.IndexOf('=')),
                        value => value.Substring(value.IndexOf('=') + 1));

            foreach (var existing in existingParameters.Where(existing => !p.ContainsKey(existing.Key)))
            {
                p.Add(existing);
            }
            var query = string.Join("&", p.Select(kvp => kvp.Key + "=" + kvp.Value));
            if (!uri.IsAbsoluteUri) return new Uri(uri + "?" + query, UriKind.Relative);
            var uriBuilder = new UriBuilder(uri)
            {
                Query = query
            };
            return uriBuilder.Uri;
        }
    }
}

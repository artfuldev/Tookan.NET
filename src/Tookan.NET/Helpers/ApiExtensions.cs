using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Tookan.NET.Http;
using Tookan.NET.Sanity;

namespace Tookan.NET.Helpers
{
    /// <summary>
    /// Extensions for working with the <see cref="IApiConnection"/>
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<T> Get<T>(this IApiConnection connection, Uri uri)
        {
            Ensure.ArgumentIsNotNull(connection, "connection");
            Ensure.ArgumentIsNotNull(uri, "uri");

            return connection.Get<T>(uri, null);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<IReadOnlyList<T>> GetAll<T>(this IApiConnection connection, Uri uri)
        {
            Ensure.ArgumentIsNotNull(connection, "connection");
            Ensure.ArgumentIsNotNull(uri, "uri");

            return connection.GetAll<T>(uri, null);
        }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<IApiResponse<T>> GetResponse<T>(this IConnection connection, Uri uri)
        {
            Ensure.ArgumentIsNotNull(connection, "connection");
            Ensure.ArgumentIsNotNull(uri, "uri");

            return connection.Get<T>(uri, null, null);
        }

        public static Task<IApiResponse<T>> GetRedirect<T>(this IConnection connection, Uri uri)
        {
            Ensure.ArgumentIsNotNull(connection, "connection");
            Ensure.ArgumentIsNotNull(uri, "uri");

            return connection.Get<T>(uri, null, null, false);
        }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="cancellationToken">A token used to cancel the GetResponse request</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<IApiResponse<T>> GetResponse<T>(this IConnection connection, Uri uri, CancellationToken cancellationToken)
        {
            Ensure.ArgumentIsNotNull(connection, "connection");
            Ensure.ArgumentIsNotNull(uri, "uri");

            return connection.Get<T>(uri, null, null, cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Tookan.NET.Helpers;
using Tookan.NET.Sanity;

namespace Tookan.NET.Http
{
    /// <summary>
    /// A connection for making API requests against URI endpoints.
    /// Provides type-friendly convenience methods that wrap <see cref="IConnection"/> methods.
    /// </summary>
    public class ApiConnection : IApiConnection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnection"/> class.
        /// </summary>
        /// <param name="connection">A connection for making HTTP requests</param>
        public ApiConnection(IConnection connection)
        {
            Ensure.ArgumentIsNotNull(connection, "connection");

            Connection = connection;
        }

        /// <summary>
        /// The underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            var response = await Connection.Get<T>(uri, parameters, null).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(accepts, "accepts");

            var response = await Connection.Get<T>(uri, parameters, accepts).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Gets the HTML content of the API resource at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns>The API resource's HTML content.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<string> GetHtml(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            var response = await Connection.GetHtml(uri, parameters).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri)
        {
            return GetAll<T>(uri, null, null);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters)
        {
            return GetAll<T>(uri, parameters, null);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters, string accepts)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return await Get<List<T>>(uri, parameters, accepts).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns><seealso cref="HttpStatusCode"/>Representing the received HTTP response</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task Post(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return Connection.Post(uri);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<T> Post<T>(Uri uri, object data)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");

            return Post<T>(uri, data, null, null);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<T> Post<T>(Uri uri, object data, string accepts)
        {
            return Post<T>(uri, data, accepts, null);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <param name="contentType">Content type of the API request</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Post<T>(Uri uri, object data, string accepts, string contentType)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");

            var response = await Connection.Post<T>(
                uri,
                data,
                accepts,
                contentType).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <param name="contentType">Content type of the API request</param>
        /// <param name="twoFactorAuthenticationCode">Two Factor Authentication Code</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Post<T>(Uri uri, object data, string accepts, string contentType, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");
            Ensure.ArgumentIsNotNull(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");

            var response = await Connection.Post<T>(
                uri,
                data,
                accepts,
                contentType,
                twoFactorAuthenticationCode).ConfigureAwait(false);
            return response.Body;
        }


        public async Task<T> Post<T>(Uri uri, object data, string accepts, string contentType, TimeSpan timeout)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");

            var response = await Connection.Post<T>(
                uri,
                data,
                accepts,
                contentType,
                timeout).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI
        /// </summary>
        /// <param name="uri">URI of the API resource to put</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Put(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return Connection.Put(uri);
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Put<T>(Uri uri, object data)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");

            var response = await Connection.Put<T>(uri, data).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Put<T>(Uri uri, object data, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");
            Ensure.ArgumentIsNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");

            var response = await Connection.Put<T>(uri, data, twoFactorAuthenticationCode).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Put<T>(Uri uri, object data, string twoFactorAuthenticationCode, string accepts)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");

            var response = await Connection.Put<T>(uri, data, twoFactorAuthenticationCode, accepts).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to patch</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Patch(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return Connection.Patch(uri);
        }

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Patch<T>(Uri uri, object data)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");

            var response = await Connection.Patch<T>(uri, data).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Patch<T>(Uri uri, object data, string accepts)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");
            Ensure.ArgumentIsNotNull(accepts, "accepts");

            var response = await Connection.Patch<T>(uri, data, accepts).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Deletes the API object at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to delete</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Delete(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return Connection.Delete(uri);
        }

        /// <summary>
        /// Deletes the API object at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to delete</param>
        /// <param name="twoFactorAuthenticationCode">Two Factor Code</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Delete(Uri uri, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return Connection.Delete(uri, twoFactorAuthenticationCode);
        }

        /// <summary>
        /// Deletes the API object at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to delete</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Delete(Uri uri, object data)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");

            return Connection.Delete(uri, data);
        }

        /// <summary>
        /// Executes a GET to the API object at the specified URI. This operation is appropriate for
        /// API calls which wants to return the redirect URL.
        /// It expects the API to respond with a 302 Found.
        /// </summary>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns>The URL returned by the API in the Location header</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs, or the API does not respond with a 302 Found</exception>
        public async Task<string> GetRedirect(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            var response = await Connection.GetRedirect<string>(uri);

            if (response.HttpResponse.StatusCode == HttpStatusCode.Redirect)
            {
                return response.HttpResponse.Headers["Location"];
            }

            throw new Exception();
            // TODO: ApiException
            //throw new ApiException("Redirect Operation expect status code of Redirect.",
                //response.HttpResponse.StatusCode);
        }

        /// <summary>
        /// Executes a GET to the API object at the specified URI. This operation is appropriate for
        /// API calls which queue long running calculations.
        /// It expects the API to respond with an initial 202 Accepted, and queries again until a 
        /// 200 OK is received.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> GetQueuedOperation<T>(Uri uri, CancellationToken cancellationToken)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            var response = await Connection.GetResponse<T>(uri, cancellationToken);

            if (response.HttpResponse.StatusCode == HttpStatusCode.Accepted)
            {
                return await GetQueuedOperation<T>(uri, cancellationToken);
            }

            if (response.HttpResponse.StatusCode == HttpStatusCode.OK)
            {
                return response.Body;
            }
            throw new Exception();
            // TODO: ApiException
            //throw new ApiException("Queued Operations expect status codes of Accepted or OK.",
            //    response.HttpResponse.StatusCode);
        }
    }
}

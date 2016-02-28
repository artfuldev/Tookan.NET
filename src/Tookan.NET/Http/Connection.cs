using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tookan.NET.Authentication;
using Tookan.NET.Helpers;
using Tookan.NET.Sanity;
using Tookan.NET.Serialization;

namespace Tookan.NET.Http
{
    // NOTE: Every request method must go through the `RunRequest` code path. So if you need to add a new method
    //       ensure it goes through there. :)
    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public class Connection : IConnection
    {
        private static readonly ICredentialStore AnonymousCredentials = new InMemoryCredentialStore(Credentials.Anonymous);

        private readonly Authenticator _authenticator;
        private readonly IJsonHttpPipeline _jsonPipeline;
        private readonly IHttpClient _httpClient;

        /// <summary>
        /// Creates a new connection instance used to make requests of the Sulekha Property API.
        /// </summary>
        public Connection(Uri baseAddress)
            : this(baseAddress, AnonymousCredentials)
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the Sulekha Property API.
        /// </summary>
        /// <param name="httpClient">
        /// The client to use for executing requests
        /// </param>
        public Connection(Uri baseAddress, IHttpClient httpClient)
            : this(baseAddress, AnonymousCredentials, httpClient, new SerializationService())
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the Sulekha Property API.
        /// </summary>
        /// <param name="baseAddress">The address to point this client to, such as https://api.tookanapp.com:8888.</param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        public Connection(Uri baseAddress, ICredentialStore credentialStore)
            : this(baseAddress, credentialStore, new HttpClientAdapter(), new SerializationService())
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the Sulekha Property API.
        /// </summary>
        /// <param name="baseAddress">The address to point this client to, such as https://api.tookanapp.com:8888.</param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        /// <param name="httpClient">A raw <see cref="IHttpClient"/> used to make requests</param>
        /// <param name="serializer">Class used to serialize and deserialize JSON requests</param>
        public Connection(Uri baseAddress, ICredentialStore credentialStore, IHttpClient httpClient, ISerializationService serializer)
            : this(baseAddress, credentialStore, httpClient, serializer, new JsonHttpPipeline(serializer))
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the Sulekha Property API.
        /// </summary>
        /// <param name="baseAddress">The address to point this client to, such as https://api.tookanapp.com:8888.</param>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        /// <param name="httpClient">A raw <see cref="IHttpClient"/> used to make requests</param>
        /// <param name="serializer">Class used to serialize and deserialize JSON requests</param>
        /// <param name="jsonPipeline">Json Pipeline used to serialize and deserialize.</param>
        public Connection(
            Uri baseAddress,
            ICredentialStore credentialStore,
            IHttpClient httpClient,
            ISerializationService serializer,
            IJsonHttpPipeline jsonPipeline)
        {
            Ensure.ArgumentIsNotNull(baseAddress, nameof(baseAddress));
            Ensure.ArgumentIsNotNull(credentialStore, nameof(credentialStore));
            Ensure.ArgumentIsNotNull(httpClient, nameof(httpClient));
            Ensure.ArgumentIsNotNull(serializer, nameof(serializer));
            Ensure.ArgumentIsNotNull(jsonPipeline, nameof(jsonPipeline));

            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture, "The base address '{0}' must be an absolute URI",
                        baseAddress), nameof(baseAddress));
            }

            BaseAddress = baseAddress;
            _authenticator = new Authenticator(credentialStore);
            _httpClient = httpClient;
            _jsonPipeline = jsonPipeline;
        }

        public Task<IApiResponse<T>> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Get, null, accepts, null, CancellationToken.None);
        }

        /// <summary>
        /// Performs an asynchronous HTTP GET request.
        /// Attempts to map the response to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="parameters">Querystring parameters for the request</param>
        /// <param name="accepts">Specifies accepted response media types.</param>
        /// <param name="allowAutoRedirect">To follow redirect links automatically or not</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>

        public Task<IApiResponse<T>> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, bool allowAutoRedirect)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Get, null, accepts, null, CancellationToken.None, allowAutoRedirect: allowAutoRedirect);
        }

        public Task<IApiResponse<T>> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, CancellationToken cancellationToken)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            return SendData<T>(uri.ApplyParameters(parameters), HttpMethod.Get, null, accepts, null, cancellationToken);
        }

        public Task<IApiResponse<T>> Patch<T>(Uri uri, object body)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(body, "body");

            return SendData<T>(uri, HttpVerb.Patch, body, null, null, CancellationToken.None);
        }

        public Task<IApiResponse<T>> Patch<T>(Uri uri, object body, string accepts)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(body, "body");
            Ensure.ArgumentIsNotNull(accepts, "accepts");

            return SendData<T>(uri, HttpVerb.Patch, body, accepts, null, CancellationToken.None);
        }

        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        public async Task<HttpStatusCode> Post(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, nameof(uri));

            var response = await SendData<object>(uri, HttpMethod.Post, null, null, null, CancellationToken.None);
            return response.HttpResponse.StatusCode;
        }

        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts, string contentType)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(body, "body");

            return SendData<T>(uri, HttpMethod.Post, body, accepts, contentType, CancellationToken.None);
        }

        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// Attempts to map the response body to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="body">The object to serialize as the body of the request</param>
        /// <param name="accepts">Specifies accepted response media types.</param>
        /// <param name="contentType">Specifies the media type of the request body</param>
        /// <param name="twoFactorAuthenticationCode">Two Factor Authentication Code</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts, string contentType, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(body, "body");
            Ensure.ArgumentIsNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");

            return SendData<T>(uri, HttpMethod.Post, body, accepts, contentType, CancellationToken.None, twoFactorAuthenticationCode);

        }

        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts, string contentType, TimeSpan timeout)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(body, "body");

            return SendData<T>(uri, HttpMethod.Post, body, accepts, contentType, timeout, CancellationToken.None);
        }

        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts, string contentType, Uri baseAddress)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(body, "body");

            return SendData<T>(uri, HttpMethod.Post, body, accepts, contentType, CancellationToken.None, baseAddress: baseAddress);
        }

        public Task<IApiResponse<T>> Put<T>(Uri uri, object body)
        {
            return SendData<T>(uri, HttpMethod.Put, body, null, null, CancellationToken.None);
        }

        public Task<IApiResponse<T>> Put<T>(Uri uri, object body, string twoFactorAuthenticationCode)
        {
            return SendData<T>(uri,
                HttpMethod.Put,
                body,
                null,
                null,
                CancellationToken.None,
                twoFactorAuthenticationCode);
        }

        public Task<IApiResponse<T>> Put<T>(Uri uri, object body, string twoFactorAuthenticationCode, string accepts)
        {
            return SendData<T>(uri,
                HttpMethod.Put,
                body,
                accepts,
                null,
                CancellationToken.None,
                twoFactorAuthenticationCode);
        }

        Task<IApiResponse<T>> SendData<T>(
            Uri uri,
            HttpMethod method,
            object body,
            string accepts,
            string contentType,
            TimeSpan timeout,
            CancellationToken cancellationToken,
            string twoFactorAuthenticationCode = null,
            Uri baseAddress = null)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.IsGreaterThanZero(timeout, "timeout");

            var request = new Request
            {
                Method = method,
                BaseAddress = baseAddress ?? BaseAddress,
                Endpoint = uri,
                Timeout = timeout
            };

            return SendDataInternal<T>(body, accepts, contentType, cancellationToken, twoFactorAuthenticationCode, request);
        }

        Task<IApiResponse<T>> SendData<T>(
            Uri uri,
            HttpMethod method,
            object body,
            string accepts,
            string contentType,
            CancellationToken cancellationToken,
            string twoFactorAuthenticationCode = null,
            Uri baseAddress = null,
            bool allowAutoRedirect = true)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            var request = new Request
            {
                Method = method,
                BaseAddress = baseAddress ?? BaseAddress,
                Endpoint = uri,
                AllowAutoRedirect = allowAutoRedirect
            };

            return SendDataInternal<T>(body, accepts, contentType, cancellationToken, twoFactorAuthenticationCode, request);
        }

        Task<IApiResponse<T>> SendDataInternal<T>(object body, string accepts, string contentType, CancellationToken cancellationToken, string twoFactorAuthenticationCode, Request request)
        {
            if (!string.IsNullOrEmpty(accepts))
            {
                request.Headers["Accept"] = accepts;
            }

            if (body != null)
            {
                request.Body = body;
                // Default Content Type
                request.ContentType = contentType ?? "application/json";
            }

            return Run<T>(request, cancellationToken);
        }

        /// <summary>
        /// Performs an asynchronous HTTP PATCH request.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        public async Task<HttpStatusCode> Patch(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            var request = new Request
            {
                Method = HttpVerb.Patch,
                BaseAddress = BaseAddress,
                Endpoint = uri
            };
            var response = await Run<object>(request, CancellationToken.None);
            return response.HttpResponse.StatusCode;
        }

        /// <summary>
        /// Performs an asynchronous HTTP PUT request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        public async Task<HttpStatusCode> Put(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            var request = new Request
            {
                Method = HttpMethod.Put,
                BaseAddress = BaseAddress,
                Endpoint = uri
            };
            var response = await Run<object>(request, CancellationToken.None);
            return response.HttpResponse.StatusCode;
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        public async Task<HttpStatusCode> Delete(Uri uri)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            var request = new Request
            {
                Method = HttpMethod.Delete,
                BaseAddress = BaseAddress,
                Endpoint = uri
            };
            var response = await Run<object>(request, CancellationToken.None);
            return response.HttpResponse.StatusCode;
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="twoFactorAuthenticationCode">Two Factor Code</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        public async Task<HttpStatusCode> Delete(Uri uri, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");

            var response = await SendData<object>(
                uri,
                HttpMethod.Delete,
                null,
                null,
                null,
                CancellationToken.None,
                twoFactorAuthenticationCode);
            return response.HttpResponse.StatusCode;
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="data">The object to serialize as the body of the request</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        public async Task<HttpStatusCode> Delete(Uri uri, object data)
        {
            Ensure.ArgumentIsNotNull(uri, "uri");
            Ensure.ArgumentIsNotNull(data, "data");

            var request = new Request
            {
                Method = HttpMethod.Delete,
                Body = data,
                BaseAddress = BaseAddress,
                Endpoint = uri
            };
            var response = await Run<object>(request, CancellationToken.None);
            return response.HttpResponse.StatusCode;
        }

        /// <summary>
        /// Base address for the connection.
        /// </summary>
        public Uri BaseAddress { get; }

        /// <summary>
        /// Gets the <seealso cref="ICredentialStore"/> used to provide credentials for the connection.
        /// </summary>
        public ICredentialStore CredentialStore => _authenticator.CredentialStore;

        /// <summary>
        /// Gets or sets the credentials used by the connection.
        /// </summary>
        /// <remarks>
        /// You can use this property if you only have a single hard-coded credential. Otherwise, pass in an 
        /// <see cref="ICredentialStore"/> to the constructor. 
        /// Setting this property will change the <see cref="ICredentialStore"/> to use 
        /// the default <see cref="InMemoryCredentialStore"/> with just these credentials.
        /// </remarks>
        public Credentials Credentials
        {
            get
            {
                var credentialTask = CredentialStore.GetCredentials();
                if (credentialTask == null) return Credentials.Anonymous;
                return credentialTask.Result ?? Credentials.Anonymous;
            }
            // Note this is for convenience. We probably shouldn't allow this to be mutable.
            set
            {
                Ensure.ArgumentIsNotNull(value, "value");
                _authenticator.CredentialStore = new InMemoryCredentialStore(value);
            }
        }

        /// <summary>
        /// Gets or sets the access token used by this Tooken.NET client.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// A handler to handle the response, in case of errors, etc
        /// </summary>
        public Action<IResponse> ResponseHandler { get; set; }

        async Task<IApiResponse<T>> Run<T>(IRequest request, CancellationToken cancellationToken)
        {
            _jsonPipeline.SerializeRequest(request);
            var response = await RunRequest(request, cancellationToken).ConfigureAwait(false);
            return request.Method == HttpMethod.Delete
                ? new ApiResponse<T>(response)
                : _jsonPipeline.DeserializeResponse<T>(response);
        }

        // THIS IS THE METHOD THAT EVERY REQUEST MUST GO THROUGH!
        async Task<IResponse> RunRequest(IRequest request, CancellationToken cancellationToken)
        {
            await _authenticator.Apply(request).ConfigureAwait(false);
            var response = await _httpClient.Send(request, cancellationToken).ConfigureAwait(false);
            ResponseHandler?.Invoke(response);
            return response;
        }
    }
}

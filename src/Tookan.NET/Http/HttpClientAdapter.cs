﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tookan.NET.Sanity;

namespace Tookan.NET.Http
{
    /// <summary>
    /// Generic Http client. Useful for those who want to swap out System.Net.HttpClient with something else.
    /// </summary>
    /// <remarks>
    /// Most folks won't ever need to swap this out. But if you're trying to run this on Windows Phone, you might.
    /// </remarks>
    public class HttpClientAdapter : IHttpClient
    {
        readonly IWebProxy _webProxy;

        public HttpClientAdapter() { }

        public HttpClientAdapter(IWebProxy webProxy)
        {
            _webProxy = webProxy;
        }

        /// <summary>
        /// Sends the specified request and returns a response.
        /// </summary>
        /// <param name="request">A <see cref="IRequest"/> that represents the HTTP request</param>
        /// <param name="cancellationToken">Used to cancel the request</param>
        /// <returns>A <see cref="Task" /> of <see cref="IResponse"/></returns>
        public async Task<IResponse> Send(IRequest request, CancellationToken cancellationToken)
        {
            Ensure.ArgumentIsNotNull(request, "request");

            var httpOptions = new HttpClientHandler
            {
                AllowAutoRedirect = request.AllowAutoRedirect
            };
            if (httpOptions.SupportsAutomaticDecompression)
            {
                httpOptions.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            if (httpOptions.SupportsProxy && _webProxy != null)
            {
                httpOptions.UseProxy = true;
                httpOptions.Proxy = _webProxy;
            }

            var http = new HttpClient(httpOptions);
            var cancellationTokenForRequest = cancellationToken;

            if (request.Timeout != TimeSpan.Zero)
            {
                var timeoutCancellation = new CancellationTokenSource(request.Timeout);
                var unifiedCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCancellation.Token);

                cancellationTokenForRequest = unifiedCancellationToken.Token;
            }

            using (var requestMessage = BuildRequestMessage(request))
            {
                // Make the request
                var responseMessage = await http.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationTokenForRequest)
                                                .ConfigureAwait(false);
                return await BuildResponse(responseMessage).ConfigureAwait(false);
            }
        }

        protected async virtual Task<IResponse> BuildResponse(HttpResponseMessage responseMessage)
        {
            Ensure.ArgumentIsNotNull(responseMessage, "responseMessage");

            object responseBody = null;
            string contentType = null;
            using (var content = responseMessage.Content)
            {
                if (content != null)
                {
                    contentType = GetContentMediaType(responseMessage.Content);

                    // We added support for downloading images and zip-files. Let's constrain this appropriately.
                    if (contentType != null && (contentType.StartsWith("image/") || contentType.Equals("application/zip", StringComparison.OrdinalIgnoreCase)))
                    {
                        responseBody = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        responseBody = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }
            }

            return new Response(
                responseMessage.StatusCode,
                responseBody,
                responseMessage.Headers.ToDictionary(h => h.Key, h => h.Value.First()),
                contentType);
        }

        protected virtual HttpRequestMessage BuildRequestMessage(IRequest request)
        {
            Ensure.ArgumentIsNotNull(request, "request");
            HttpRequestMessage requestMessage = null;
            try
            {
                var fullUri = new Uri(request.BaseAddress, request.Endpoint);
                requestMessage = new HttpRequestMessage(request.Method, fullUri);
                foreach (var header in request.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
                var httpContent = request.Body as HttpContent;
                if (httpContent != null)
                {
                    requestMessage.Content = httpContent;
                }

                var body = request.Body as string;
                if (body != null)
                {
                    requestMessage.Content = new StringContent(body, Encoding.UTF8, request.ContentType);
                }

                var bodyStream = request.Body as Stream;
                if (bodyStream != null)
                {
                    requestMessage.Content = new StreamContent(bodyStream);
                    requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.ContentType);
                }
            }
            catch (Exception)
            {
                requestMessage?.Dispose();
                throw;
            }

            return requestMessage;
        }

        static string GetContentMediaType(HttpContent httpContent)
        {
            return httpContent.Headers?.ContentType?.MediaType;
        }
    }
}

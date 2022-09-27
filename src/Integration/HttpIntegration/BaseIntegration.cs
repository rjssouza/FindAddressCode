using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CustomException;
using Newtonsoft.Json;

namespace Integration.HttpIntegration
{
    /// <summary>
    /// Integration base
    /// </summary>
    public abstract class BaseIntegration : IDisposable
    {
        private HttpClient _httpClient;
        private bool disposedValue;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="httpClientFactory">Http Client factory</param>
        public BaseIntegration(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Name);
        }

        public AuthenticationHeaderValue? Authorization { get; }

        /// <summary>
        /// integration api address
        /// </summary>
        protected abstract string ApiAddress { get; }

        /// <summary>
        /// Integration Name
        /// </summary>
        protected abstract string Name { get; }

        protected TReturn? Delete<TReturn>(string urlRequest, object? data = null)
        {
            var result = ExecuteSync(async () =>
            {
                var url = FormatUrl(urlRequest);
                var responseHttp = await SendRequest(HttpMethod.Delete, url, data);
                var processedResponse = await ProcessHttpResponse<TReturn>(responseHttp);

                return processedResponse;
            });

            return result;
        }

        protected async Task<HttpResponseMessage> Delete(string urlRequest, object? data = null)
        {
            var url = FormatUrl(urlRequest);
            var responseHttp = await SendRequest(HttpMethod.Delete, url, data);

            return responseHttp;
        }

        protected static TReturn? ExecuteSync<TReturn>(Func<Task<TReturn>> taskAssincrona)
        {
            try
            {
                TReturn? result = default;
                Task.Run<TReturn>(async () => await taskAssincrona.Invoke().ConfigureAwait(false))
                    .ContinueWith<TReturn>((returnData) => result = returnData.Result)
                    .Wait();

                return result;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                // Tratamento de erro generico de thread assíncrona, retorna o erro "real" para o sistema.
                throw ex;
            }
        }

        protected static TReturn? ExecuteSync<TReturn, TEntrada>(Func<TEntrada, Task<TReturn>> taskAssincrona, TEntrada entrada)
        {
            try
            {
                TReturn? result = default;
                Task.Run<TReturn>(async () => await taskAssincrona.Invoke(entrada).ConfigureAwait(false))
                    .ContinueWith<TReturn>((returnData) => result = returnData.Result)
                    .Wait();

                return result;
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                // Tratamento de erro generico de thread assíncrona, retorna o erro "real" para o sistema.
                throw ex;
            }
        }

        protected static TReturn? ExecuteSync<TReturn, TEntrada1, TEntrada2>(Func<TEntrada1, TEntrada2, Task<TReturn>> taskAssincrona, TEntrada1 entrada, TEntrada2 entrada2)
        {
            TReturn? result = default;
            Task.Run<TReturn>(async () => await taskAssincrona.Invoke(entrada, entrada2))
                .ContinueWith<TReturn>((returnData) => result = returnData.Result);

            return result;
        }

        protected TReturn? Get<TReturn>(string urlRequest)
        {
            var result = ExecuteSync(async () =>
            {
                var url = FormatUrl(urlRequest);
                var responseHttp = await SendRequest(HttpMethod.Get, url);
                var processedResponse = await ProcessHttpResponse<TReturn>(responseHttp);

                return processedResponse;
            });

            return result;
        }
        protected async Task<HttpResponseMessage> Get(string urlRequest)
        {
            var url = FormatUrl(urlRequest);
            var responseHttp = await SendRequest(HttpMethod.Get, url);

            return responseHttp;
        }

        protected TReturn? Post<TReturn>(string urlRequest, object? data = null)
        {
            var result = ExecuteSync(async () =>
            {
                var url = FormatUrl(urlRequest);
                var responseHttp = await SendRequest(HttpMethod.Post, url, data);
                var processedResponse = await ProcessHttpResponse<TReturn>(responseHttp);

                return processedResponse;
            });

            return result;
        }

        protected async Task<HttpResponseMessage> Post(string urlRequest, object? data = null)
        {
            var url = FormatUrl(urlRequest);
            var responseHttp = await SendRequest(HttpMethod.Post, url, data);

            return responseHttp;
        }

        protected TReturn? Put<TReturn>(string urlRequest, object? data = null)
        {
            var result = ExecuteSync(async () =>
            {
                var url = FormatUrl(urlRequest);
                var responseHttp = await SendRequest(HttpMethod.Put, url, data);
                var processedResponse = await ProcessHttpResponse<TReturn>(responseHttp);

                return processedResponse;
            });

            return result;
        }

        protected async Task<HttpResponseMessage> Put(string urlRequest, object? data = null)
        {
            var url = FormatUrl(urlRequest);
            var responseHttp = await SendRequest(HttpMethod.Put, url, data);

            return responseHttp;
        }

        private void SetAuthHeader(HttpRequestMessage request)
        {
            request.Headers.Authorization = Authorization;
        }

        private async Task<HttpResponseMessage> SendRequest(HttpMethod metodo, string url, object? data = null)
        {
            using var request = new HttpRequestMessage(metodo, url);
            SetAuthHeader(request);

            await FillRequest(data, request);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return response;
        }

        private string FormatUrl(string urlRequest)
        {
            var formattedUrl = string.Join("/", new string[] { ApiAddress, urlRequest });
            var url = new Uri(formattedUrl);

            return url.AbsoluteUri;
        }
        private static async Task FillRequest(object? data, HttpRequestMessage request)
        {
            if (data != null)
            {
                var content = await Task.FromResult(JsonConvert.SerializeObject(data)).ConfigureAwait(false);

                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }
        }

        protected async Task<T?> ProcessHttpResponse<T>(HttpResponseMessage responseHttp)
        {
            if (!responseHttp.IsSuccessStatusCode)
                await ProcessMessageError(responseHttp);


            var data = await responseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
            return await Task.FromResult(JsonConvert.DeserializeObject<T>(data)).ConfigureAwait(false);
        }

        /// <summary>
        /// Process message error and throw an exception to application 
        /// </summary>
        /// <param name="responseHttp">Http response from external service</param>
        /// <returns>Async task</returns>
        protected virtual async Task ProcessMessageError(HttpResponseMessage responseHttp)
        {
            var ex = await ThrowException(responseHttp);

            throw ex;
        }

        /// <summary>
        /// Handle exception code from external api
        /// </summary>
        /// <param name="responseHttp">Web api response</param>
        /// <returns>Exception result</returns>
        protected async Task<Exception> ThrowException(HttpResponseMessage responseHttp)
        {
            var data = await responseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
            var statusCode = responseHttp.StatusCode;

            throw statusCode switch
            {
                HttpStatusCode.PreconditionFailed => new ValidationException(Name, data),
                _ => new Exception(data),
            };
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                _httpClient?.Dispose();

                disposedValue = true;
            }
        }
    }
}
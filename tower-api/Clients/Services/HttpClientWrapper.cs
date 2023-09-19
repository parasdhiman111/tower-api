using System;
using credit_work_app.Clients.Interfaces;
using System.Net.Http.Headers;

namespace credit_work_app.Clients.Services
{
    public class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private HttpClient Client { get; }

        public HttpRequestHeaders DefaultRequestHeaders => Client.DefaultRequestHeaders;

        public TimeSpan Timeout
        {
            get => Client.Timeout;
            set => Client.Timeout = value;
        }

        public HttpClientWrapper()
        {
            Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await Client.SendAsync(request);
        }

        public void Dispose() => Client.Dispose();
    }
}


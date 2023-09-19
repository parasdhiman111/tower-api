using System;
using System.Net.Http.Headers;

namespace credit_work_app.Clients.Interfaces
{
    public interface IHttpClientWrapper
    {
        HttpRequestHeaders DefaultRequestHeaders { get; }
        TimeSpan Timeout { get; set; }
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}


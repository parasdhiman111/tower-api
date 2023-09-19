using System;
using credit_work_app.Clients.Interfaces;
using System.Net.Http.Headers;
using credit_work_app.Clients.Models;

namespace credit_work_app.Clients.Services
{
    public class WebClientDataFormatSetter : IWebClientDataFormatSetter
    {
        public void Set(IHttpClientWrapper client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Json));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Xml));
        }
    }
}


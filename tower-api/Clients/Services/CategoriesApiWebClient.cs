using System.Net.Http.Formatting;
using credit_work_app.Clients.Exceptions;
using credit_work_app.Clients.Interfaces;
using credit_work_app.Clients.Models;

namespace credit_work_app.Clients.Services
{
    public class CategoriesApiWebClient: ICategoriesApiWebClient
    {
        private IHttpClientWrapper Client { get; }
        public IWebClientDataFormatSetter DataFormatSetter { get; }

        public CategoriesApiWebClient(IHttpClientWrapper client, IWebClientDataFormatSetter dataFormatSetter)
        {
            Client = client;
            DataFormatSetter = dataFormatSetter;
        }

        public async Task<ApiResponse<TSuccess>> GetAsync<TSuccess>(string url)
        {
            Validate(url);
            var response = await TryGetAsync(url);
            return await ReadContentAsync<TSuccess>(response);
        }

        private static async Task<ApiResponse<TSuccess>> ReadContentAsync<TSuccess>(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode
                ? new ApiResponse<TSuccess>(response.StatusCode, await ReadSuccessContentAsync<TSuccess>(response))
                : new ApiResponse<TSuccess>(response.StatusCode, await ReadErrorContentAsync(response));
        }

        private static async Task<TSuccess> ReadSuccessContentAsync<TSuccess>(HttpResponseMessage response)
        {
            var formatters = new MediaTypeFormatter[] { new JsonMediaTypeFormatter() };
            return await response.Content.ReadAsAsync<TSuccess>(formatters);
        }

        private static async Task<string> ReadErrorContentAsync(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        private static void Validate(string url)
        {
            try
            {
                new Uri(url);
            }
            catch (Exception)
            {
                throw new InvalidUrl(url);
            }
        }

        private async Task<HttpResponseMessage> TryGetAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            return await TrySend(request);
        }

        private async Task<HttpResponseMessage> TrySend(HttpRequestMessage request)
        {
            try
            {
                return await Client.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new ServerError(ex.Message);
            }
        }
    }
}


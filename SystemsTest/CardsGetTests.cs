using System.Net;
using FluentAssertions;
using tower_api.Models.Responses;

namespace SystemsTest
{

    public class CardsGetTests
    {
        public string Url => $"http://localhost:5039/api/";

        private void VerifyCategories(IEnumerable<ApiCardResponse> response, HttpStatusCode statusCode)
        {
            statusCode.Should().Be(HttpStatusCode.OK);
            response.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public async Task Given_Cards_When_Call_GetCards_Then_Return_Cards()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };

            var response = await client.GetAsync("cards");
            var result = await response.Content.ReadAsAsync<IEnumerable<ApiCardResponse>>();

            VerifyCategories(result,response.StatusCode);
        }


    }
}

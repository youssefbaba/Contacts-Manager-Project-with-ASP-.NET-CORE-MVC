using FluentAssertions;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace Testing.IntegrationTests
{
    public class TardeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        // Fields
        private readonly HttpClient _client;

        // Constructors
        public TardeControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
                _client = factory.CreateClient();   
        }

        // Methods
        #region Index

        [Fact]
        public async Task Index_ToReturnIndexView()
        {
            // Arrange

            // Act
            HttpResponseMessage response = await _client.GetAsync("/Trade/Index/MSFT");

            // Assert
            response.Should().BeSuccessful();  // 2xx Status Code

            string responseBody = await response.Content.ReadAsStringAsync();  // Response Body

            HtmlDocument html = new HtmlDocument();  // An empty Html document

            html.LoadHtml(responseBody);  // To load response body into that empty html document 

            var document = html.DocumentNode;

            document.QuerySelector(".price").Should().NotBeNull();
        }

        #endregion
    }
}

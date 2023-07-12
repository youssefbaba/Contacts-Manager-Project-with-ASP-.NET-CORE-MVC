using FluentAssertions;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace ContactsManager.IntegrationTests
{
    public class PersonsControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        //Fields
        private readonly HttpClient _client;

        // Constructors
        public PersonsControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
           _client = factory.CreateClient();
        }

        // Methods
        #region Index

        [Fact]
        public async Task Index_ToReturnView()
        {
            // Arrange

            // Act
            HttpResponseMessage response = await _client.GetAsync("/Persons/Index");

            // Assert
            response.Should().BeSuccessful();  // 2xx as response

            string responseBody = await response.Content.ReadAsStringAsync();

            HtmlDocument html = new HtmlDocument();

            html.LoadHtml(responseBody);

            var document = html.DocumentNode;

            document.QuerySelector("table.persons").Should().NotBeNull();
        }

        #endregion
    }

}

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace MessageDrivenApp.Tests
{
    public class ProducerConsumerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProducerConsumerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetWeatherForecast_ShouldReturnSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/weatherforecast");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Date").And.Contain("TemperatureC").And.Contain("Summary");
        }

        [Fact]
        public async Task GetMetrics_ShouldReturnSuccessStatusCode()
        {
            // Act
            var response = await _client.GetAsync("/metrics");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("SuccessCount").And.Contain("ErrorCount");
        }
    }
}

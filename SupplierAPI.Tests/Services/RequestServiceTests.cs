using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SupplierAPI.Models;
using SupplierAPI.Repositories;
using SupplierAPI.Services;
using Xunit;

namespace SupplierAPI.Tests.Services
{
    public class RequestServiceTests
    {
        private readonly Mock<ISettingsRepository> _settingsRepositoryMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly RequestService _requestService;

        public RequestServiceTests()
        {
            _settingsRepositoryMock = new Mock<ISettingsRepository>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _requestService = new RequestService(_settingsRepositoryMock.Object, _httpClient);
        }

        [Fact]
        public async Task GetAsync_Should_Return_Response_When_Settings_Exist()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var settings = new Settings { TenantId = tenantId, ApiRequestUrl = "https://api.example.com" };
            _settingsRepositoryMock.Setup(repo => repo.GetSettingsByTenantIdAsync(tenantId)).ReturnsAsync(settings);

            var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("response content")
            };
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var response = await _requestService.GetAsync(tenantId);

            // Assert
            Assert.Equal("response content", response);
        }

        [Fact]
        public async Task PostAsync_Should_Return_Response_When_Settings_Exist()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var settings = new Settings { TenantId = tenantId, ApiRequestUrl = "https://api.example.com" };
            _settingsRepositoryMock.Setup(repo => repo.GetSettingsByTenantIdAsync(tenantId)).ReturnsAsync(settings);

            var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("response content")
            };
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var data = new { key = "value" };

            // Act
            var response = await _requestService.PostAsync(tenantId, data);

            // Assert
            Assert.Equal("response content", response);
        }
    }
}

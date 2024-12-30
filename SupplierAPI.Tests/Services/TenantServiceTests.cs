using System;
using System.Threading.Tasks;
using Moq;
using SupplierAPI.Models;
using SupplierAPI.Repositories;
using SupplierAPI.Services;
using Xunit;

namespace SupplierAPI.Tests.Services
{
    public class TenantServiceTests
    {
        private readonly Mock<ITenantRepository> _tenantRepositoryMock;
        private readonly TenantService _tenantService;

        public TenantServiceTests()
        {
            _tenantRepositoryMock = new Mock<ITenantRepository>();
            _tenantService = new TenantService(_tenantRepositoryMock.Object);
        }

        [Fact]
        public async Task GetTenantTypeByIdAsync_Should_Return_ClientTenant_When_TenantType_Is_Client()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var tenant = new Tenant { Id = tenantId, Type = "Client Tenant" };
            _tenantRepositoryMock.Setup(repo => repo.GetTenantByIdAsync(tenantId)).ReturnsAsync(tenant);

            // Act
            var result = await _tenantService.GetTenantTypeByIdAsync(tenantId);

            // Assert
            Assert.Equal("Client Tenant", result);
        }

        [Fact]
        public async Task GetTenantTypeByIdAsync_Should_Return_ServerTenant_When_TenantType_Is_Server()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var tenant = new Tenant { Id = tenantId, Type = "Server Tenant" };
            _tenantRepositoryMock.Setup(repo => repo.GetTenantByIdAsync(tenantId)).ReturnsAsync(tenant);

            // Act
            var result = await _tenantService.GetTenantTypeByIdAsync(tenantId);

            // Assert
            Assert.Equal("Server Tenant", result);
        }

        [Fact]
        public async Task GetTenantTypeByIdAsync_Should_Return_Null_When_Tenant_Not_Found()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            _tenantRepositoryMock.Setup(repo => repo.GetTenantByIdAsync(tenantId)).ReturnsAsync((Tenant)null);

            // Act
            var result = await _tenantService.GetTenantTypeByIdAsync(tenantId);

            // Assert
            Assert.Null(result);
        }
    }
}

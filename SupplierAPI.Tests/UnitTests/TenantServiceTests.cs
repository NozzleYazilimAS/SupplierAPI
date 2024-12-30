using System;
using System.Threading.Tasks;
using Moq;
using SupplierAPI.Models;
using SupplierAPI.Repositories;
using SupplierAPI.Services;
using Xunit;

namespace SupplierAPI.Tests.UnitTests
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
        public async Task GetTenantTypeById_Should_Return_Correct_Type()
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
        public async Task GetTenantById_Should_Return_Tenant()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var tenant = new Tenant { Id = tenantId, Name = "Test Tenant", Type = "Client Tenant" };
            _tenantRepositoryMock.Setup(repo => repo.GetTenantByIdAsync(tenantId)).ReturnsAsync(tenant);

            // Act
            var result = await _tenantService.GetTenantByIdAsync(tenantId);

            // Assert
            Assert.Equal(tenant, result);
        }

        [Fact]
        public async Task CreateTenant_Should_Add_Tenant()
        {
            // Arrange
            var tenant = new Tenant { Name = "New Tenant", Type = "Client Tenant" };
            _tenantRepositoryMock.Setup(repo => repo.CreateTenantAsync(It.IsAny<Tenant>())).ReturnsAsync(tenant);

            // Act
            var result = await _tenantService.CreateTenantAsync(tenant);

            // Assert
            _tenantRepositoryMock.Verify(repo => repo.CreateTenantAsync(It.IsAny<Tenant>()), Times.Once);
            Assert.Equal(tenant, result);
        }

        [Fact]
        public async Task UpdateTenant_Should_Update_Tenant()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var tenant = new Tenant { Id = tenantId, Name = "Updated Tenant", Type = "Server Tenant" };
            _tenantRepositoryMock.Setup(repo => repo.GetTenantByIdAsync(tenantId)).ReturnsAsync(tenant);
            _tenantRepositoryMock.Setup(repo => repo.UpdateTenantAsync(It.IsAny<Tenant>())).ReturnsAsync(tenant);

            // Act
            var result = await _tenantService.UpdateTenantAsync(tenantId, tenant);

            // Assert
            _tenantRepositoryMock.Verify(repo => repo.UpdateTenantAsync(It.IsAny<Tenant>()), Times.Once);
            Assert.Equal(tenant, result);
        }

        [Fact]
        public async Task DeleteTenant_Should_Remove_Tenant()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            _tenantRepositoryMock.Setup(repo => repo.DeleteTenantAsync(tenantId)).ReturnsAsync(true);

            // Act
            var result = await _tenantService.DeleteTenantAsync(tenantId);

            // Assert
            _tenantRepositoryMock.Verify(repo => repo.DeleteTenantAsync(tenantId), Times.Once);
            Assert.True(result);
        }
    }
}

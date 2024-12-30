using System.Threading.Tasks;
using Moq;
using SupplierAPI.Models;
using SupplierAPI.Repositories;
using SupplierAPI.Services;
using Xunit;

namespace SupplierAPI.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITenantRepository> _tenantRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tenantRepositoryMock = new Mock<ITenantRepository>();
            _userService = new UserService(_userRepositoryMock.Object, _tenantRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateUser_Should_Add_User_When_Valid_Input()
        {
            // Arrange
            var user = new User { UserName = "TestUser", Password = "TestPassword" };
            _userRepositoryMock.Setup(repo => repo.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.CreateUserAsync(user);

            // Assert
            _userRepositoryMock.Verify(repo => repo.CreateUserAsync(It.IsAny<User>()), Times.Once);
            Assert.Equal(user.UserName, result.UserName);
        }

        [Fact]
        public async Task Login_Should_Return_User_When_Valid_Credentials()
        {
            // Arrange
            var tenantName = "TestTenant";
            var username = "TestUser";
            var password = "TestPassword";
            var tenantId = Guid.NewGuid();
            var user = new User { UserName = username, Password = password, TenantId = tenantId };

            _tenantRepositoryMock.Setup(repo => repo.GetTenantByNameAsync(tenantName)).ReturnsAsync(new Tenant { Id = tenantId });
            _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAndPasswordAsync(username, password)).ReturnsAsync(user);

            // Act
            var result = await _userService.LoginAsync(tenantName, username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserName, result.UserName);
        }
    }
}

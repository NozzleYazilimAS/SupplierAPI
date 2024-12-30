# SupplierAPI

## Project Setup Instructions

1. Clone the repository:
   ```sh
   git clone https://github.com/githubnext/workspace-blank.git
   cd workspace-blank
   ```

2. Open the solution file `SupplierAPI.sln` in Visual Studio.

3. Build the solution to restore the NuGet packages.

4. Update the database connection string in `SupplierAPI/AppSettings.json` if necessary.

5. Run the application.

## Architectural Overview

The project follows a layered (multi-tier) architecture with the following layers:

- **API Layer (Controllers)**: Handles HTTP requests and responses.
- **Service Layer (Business Logic)**: Contains the business logic of the application.
- **Data Access Layer (Repositories)**: Manages data access and database operations.
- **Domain Layer (Entities and Models)**: Defines the entities and models used in the application.

### Features

- **SaaS Tenant Structure**: Users belong to specific tenants. Each tenant has a Settings table associated with it.
- **GUID-based IDs**: All IDs in the system are in GUID format.
- **Permission Checks**: Controller actions enforce permission checks to ensure appropriate access control.
- **Quartz.NET Integration**: Integrated Quartz.NET for task scheduling.
- **Advanced Logging System**: Captures detailed information, including tenant-specific data, and supports structured logging.

## Usage Examples

### User Management

- **Create User**:
  ```sh
  POST /api/users
  {
    "userName": "john.doe",
    "password": "password123",
    "tenantId": "guid-tenant-id"
  }
  ```

- **Get User**:
  ```sh
  GET /api/users/{id}
  ```

- **Update User**:
  ```sh
  PUT /api/users/{id}
  {
    "userName": "john.doe",
    "password": "newpassword123",
    "tenantId": "guid-tenant-id"
  }
  ```

- **Delete User**:
  ```sh
  DELETE /api/users/{id}
  ```

### Tenant Management

- **Create Tenant**:
  ```sh
  POST /api/tenants
  {
    "name": "TenantName",
    "type": "Client Tenant"
  }
  ```

- **Get Tenant**:
  ```sh
  GET /api/tenants/{id}
  ```

- **Update Tenant**:
  ```sh
  PUT /api/tenants/{id}
  {
    "name": "NewTenantName",
    "type": "Server Tenant"
  }
  ```

- **Delete Tenant**:
  ```sh
  DELETE /api/tenants/{id}
  ```

### Settings Management

- **Create Settings**:
  ```sh
  POST /api/settings
  {
    "tenantId": "guid-tenant-id",
    "apiRequestUrl": "https://api.example.com"
  }
  ```

- **Get Settings**:
  ```sh
  GET /api/settings/{id}
  ```

- **Update Settings**:
  ```sh
  PUT /api/settings/{id}
  {
    "tenantId": "guid-tenant-id",
    "apiRequestUrl": "https://api.newexample.com"
  }
  ```

- **Delete Settings**:
  ```sh
  DELETE /api/settings/{id}
  ```

## Developer Notes

- Ensure that the database connection string in `SupplierAPI/AppSettings.json` is correctly configured.
- The project uses Quartz.NET for task scheduling. Refer to the Quartz.NET documentation for more details on configuring and using Quartz.NET.
- The advanced logging system is configured to capture detailed information, including tenant-specific data. Ensure that the logging configuration in `SupplierAPI/AppSettings.json` is correctly set up.
- The project follows a layered architecture to ensure separation of concerns and maintainability. Make sure to follow the same architectural principles when adding new features or making changes to the existing codebase.

## Unit Test Project

The solution includes a separate Unit Test project named `SupplierAPI.Tests`. The test project uses xUnit as the testing framework and includes the Moq library for mocking dependencies.

### Unit Test Structure

The test project follows a folder structure that mirrors the main project structure (e.g., Services, Controllers, Repositories). Test classes correspond to key components of the main project.

### Sample Test Cases

#### UserServiceTests

```csharp
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void CreateUser_Should_Add_User_When_Valid_Input()
    {
        // Arrange
        var user = new User { UserName = "TestUser", Password = "TestPassword" };
        _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        var result = _userService.CreateUser(user);

        // Assert
        _userRepositoryMock.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
    }
}
```

#### TenantServiceTests

```csharp
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
    public void GetTenantTypeById_Should_Return_Correct_Type()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var tenant = new Tenant { Id = tenantId, Type = "Client Tenant" };
        _tenantRepositoryMock.Setup(repo => repo.GetTenantByIdAsync(tenantId)).ReturnsAsync(tenant);

        // Act
        var result = _tenantService.GetTenantTypeByIdAsync(tenantId);

        // Assert
        Assert.Equal("Client Tenant", result);
    }
}
```

#### RequestServiceTests

```csharp
public class RequestServiceTests
{
    private readonly Mock<ISettingsRepository> _settingsRepositoryMock;
    private readonly Mock<HttpClient> _httpClientMock;
    private readonly RequestService _requestService;

    public RequestServiceTests()
    {
        _settingsRepositoryMock = new Mock<ISettingsRepository>();
        _httpClientMock = new Mock<HttpClient>();
        _requestService = new RequestService(_settingsRepositoryMock.Object, _httpClientMock.Object);
    }

    [Fact]
    public async Task GetAsync_Should_Return_Response_When_Valid_TenantId()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var settings = new Settings { TenantId = tenantId, ApiRequestUrl = "https://api.example.com" };
        _settingsRepositoryMock.Setup(repo => repo.GetSettingsByTenantIdAsync(tenantId)).ReturnsAsync(settings);
        _httpClientMock.Setup(client => client.GetAsync(settings.ApiRequestUrl)).ReturnsAsync(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("response content")
        });

        // Act
        var result = await _requestService.GetAsync(tenantId);

        // Assert
        Assert.Equal("response content", result);
    }
}
```

### Test Execution

Configure the test project to run with your preferred test runner (e.g., Visual Studio Test Explorer or CLI tools). Add CI/CD pipeline integration for running tests on every push or pull request.

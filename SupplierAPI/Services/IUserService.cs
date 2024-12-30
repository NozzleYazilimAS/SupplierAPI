using System;
using System.Threading.Tasks;
using SupplierAPI.Models;

namespace SupplierAPI.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(Guid id, User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> LoginAsync(string tenantName, string username, string password);
        Task<Guid> GetTenantIdByNameAsync(string tenantName);
    }
}

using System;
using System.Threading.Tasks;
using SupplierAPI.Models;

namespace SupplierAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
    }
}

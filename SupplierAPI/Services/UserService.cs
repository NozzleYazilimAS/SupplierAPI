using System;
using System.Threading.Tasks;
using SupplierAPI.Models;
using SupplierAPI.Repositories;

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

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITenantRepository _tenantRepository;

        public UserService(IUserRepository userRepository, ITenantRepository tenantRepository)
        {
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreationTime = DateTime.UtcNow;
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(Guid id, User user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.LastModificationTime = DateTime.UtcNow;
            existingUser.LastModifierId = user.LastModifierId;

            return await _userRepository.UpdateUserAsync(existingUser);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            user.IsDeleted = true;
            user.DeletionTime = DateTime.UtcNow;
            user.DeleterId = user.DeleterId;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<User> LoginAsync(string tenantName, string username, string password)
        {
            var tenantId = await GetTenantIdByNameAsync(tenantName);
            var user = await _userRepository.GetUserByUsernameAndPasswordAsync(username, password);
            if (user == null || user.TenantId != tenantId)
            {
                return null;
            }
            return user;
        }

        public async Task<Guid> GetTenantIdByNameAsync(string tenantName)
        {
            var tenant = await _tenantRepository.GetTenantByNameAsync(tenantName);
            if (tenant == null)
            {
                throw new Exception("Tenant not found.");
            }
            return tenant.Id;
        }
    }
}

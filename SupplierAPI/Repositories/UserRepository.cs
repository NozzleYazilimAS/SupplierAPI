using System;
using System.Collections.Generic;
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

    public class UserRepository : IUserRepository
    {
        private readonly IMainRepository _mainRepository;

        public UserRepository(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _mainRepository.GetByIdAsync<User>(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _mainRepository.AddAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _mainRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _mainRepository.DeleteAsync<User>(id);
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            var users = await _mainRepository.GetAllAsync<User>();
            return users.Find(user => user.UserName == username && user.Password == password);
        }
    }
}

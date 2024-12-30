using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SupplierAPI.Models;

namespace SupplierAPI.Repositories
{
    public interface ITenantRepository
    {
        Task<Tenant> GetTenantByIdAsync(Guid id);
        Task<Tenant> CreateTenantAsync(Tenant tenant);
        Task<Tenant> UpdateTenantAsync(Tenant tenant);
        Task<bool> DeleteTenantAsync(Guid id);
        Task<Tenant> GetTenantByNameAsync(string name);
    }

    public class TenantRepository : ITenantRepository
    {
        private readonly IMainRepository _mainRepository;

        public TenantRepository(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }

        public async Task<Tenant> GetTenantByIdAsync(Guid id)
        {
            return await _mainRepository.GetByIdAsync<Tenant>(id);
        }

        public async Task<Tenant> CreateTenantAsync(Tenant tenant)
        {
            return await _mainRepository.AddAsync(tenant);
        }

        public async Task<Tenant> UpdateTenantAsync(Tenant tenant)
        {
            return await _mainRepository.UpdateAsync(tenant);
        }

        public async Task<bool> DeleteTenantAsync(Guid id)
        {
            return await _mainRepository.DeleteAsync<Tenant>(id);
        }

        public async Task<Tenant> GetTenantByNameAsync(string name)
        {
            var tenants = await _mainRepository.GetAllAsync<Tenant>();
            return tenants.Find(tenant => tenant.Name == name);
        }
    }
}

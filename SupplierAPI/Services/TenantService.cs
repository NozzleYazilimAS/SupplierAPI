using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SupplierAPI.Models;
using SupplierAPI.Repositories;

namespace SupplierAPI.Services
{
    public interface ITenantService
    {
        Task<Tenant> GetTenantByIdAsync(Guid id);
        Task<Tenant> CreateTenantAsync(Tenant tenant);
        Task<Tenant> UpdateTenantAsync(Guid id, Tenant tenant);
        Task<bool> DeleteTenantAsync(Guid id);
        Task<string> GetTenantTypeByIdAsync(Guid id);
    }

    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> GetTenantByIdAsync(Guid id)
        {
            return await _tenantRepository.GetTenantByIdAsync(id);
        }

        public async Task<Tenant> CreateTenantAsync(Tenant tenant)
        {
            tenant.Id = Guid.NewGuid();
            return await _tenantRepository.CreateTenantAsync(tenant);
        }

        public async Task<Tenant> UpdateTenantAsync(Guid id, Tenant tenant)
        {
            var existingTenant = await _tenantRepository.GetTenantByIdAsync(id);
            if (existingTenant == null)
            {
                return null;
            }

            existingTenant.Name = tenant.Name;
            existingTenant.Type = tenant.Type;

            return await _tenantRepository.UpdateTenantAsync(existingTenant);
        }

        public async Task<bool> DeleteTenantAsync(Guid id)
        {
            return await _tenantRepository.DeleteTenantAsync(id);
        }

        public async Task<string> GetTenantTypeByIdAsync(Guid id)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(id);
            return tenant?.Type;
        }
    }
}

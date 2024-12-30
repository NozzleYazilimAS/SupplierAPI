using System;
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
}

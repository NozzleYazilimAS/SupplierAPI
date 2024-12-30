using System;
using System.Threading.Tasks;
using SupplierAPI.Models;

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
}

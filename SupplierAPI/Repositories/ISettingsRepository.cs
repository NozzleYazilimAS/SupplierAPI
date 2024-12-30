using System;
using System.Threading.Tasks;
using SupplierAPI.Models;

namespace SupplierAPI.Repositories
{
    public interface ISettingsRepository
    {
        Task<Settings> GetSettingsByIdAsync(Guid id);
        Task<Settings> CreateSettingsAsync(Settings settings);
        Task<Settings> UpdateSettingsAsync(Settings settings);
        Task<bool> DeleteSettingsAsync(Guid id);
        Task<Settings> GetSettingsByTenantIdAsync(Guid tenantId);
    }
}

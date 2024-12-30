using System;
using System.Threading.Tasks;
using SupplierAPI.Models;

namespace SupplierAPI.Services
{
    public interface ISettingsService
    {
        Task<Settings> GetSettingsByIdAsync(Guid id);
        Task CreateSettingsAsync(Settings settings);
        Task UpdateSettingsAsync(Guid id, Settings settings);
        Task DeleteSettingsAsync(Guid id);
    }
}

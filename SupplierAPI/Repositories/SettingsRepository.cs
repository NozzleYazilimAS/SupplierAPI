using System;
using System.Collections.Generic;
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

    public class SettingsRepository : ISettingsRepository
    {
        private readonly IMainRepository _mainRepository;

        public SettingsRepository(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }

        public async Task<Settings> GetSettingsByIdAsync(Guid id)
        {
            return await _mainRepository.GetByIdAsync<Settings>(id);
        }

        public async Task<Settings> CreateSettingsAsync(Settings settings)
        {
            return await _mainRepository.AddAsync(settings);
        }

        public async Task<Settings> UpdateSettingsAsync(Settings settings)
        {
            return await _mainRepository.UpdateAsync(settings);
        }

        public async Task<bool> DeleteSettingsAsync(Guid id)
        {
            return await _mainRepository.DeleteAsync<Settings>(id);
        }

        public async Task<Settings> GetSettingsByTenantIdAsync(Guid tenantId)
        {
            var settingsList = await _mainRepository.GetByTenantIdAsync<Settings>(tenantId);
            return settingsList.Find(settings => settings.TenantId == tenantId);
        }
    }
}

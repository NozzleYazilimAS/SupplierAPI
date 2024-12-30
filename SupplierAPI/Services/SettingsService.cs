using System;
using System.Threading.Tasks;
using SupplierAPI.Models;
using SupplierAPI.Repositories;

namespace SupplierAPI.Services
{
    public interface ISettingsService
    {
        Task<Settings> GetSettingsByIdAsync(Guid id);
        Task CreateSettingsAsync(Settings settings);
        Task UpdateSettingsAsync(Guid id, Settings settings);
        Task DeleteSettingsAsync(Guid id);
    }

    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<Settings> GetSettingsByIdAsync(Guid id)
        {
            return await _settingsRepository.GetSettingsByIdAsync(id);
        }

        public async Task CreateSettingsAsync(Settings settings)
        {
            await _settingsRepository.CreateSettingsAsync(settings);
        }

        public async Task UpdateSettingsAsync(Guid id, Settings settings)
        {
            await _settingsRepository.UpdateSettingsAsync(id, settings);
        }

        public async Task DeleteSettingsAsync(Guid id)
        {
            await _settingsRepository.DeleteSettingsAsync(id);
        }
    }
}

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SupplierAPI.Models;
using SupplierAPI.Repositories;

namespace SupplierAPI.Services
{
    public interface IRequestService
    {
        Task<string> GetAsync(Guid tenantId);
        Task<string> PostAsync(Guid tenantId, object data);
    }

    public class RequestService : IRequestService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly HttpClient _httpClient;

        public RequestService(ISettingsRepository settingsRepository, HttpClient httpClient)
        {
            _settingsRepository = settingsRepository;
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(Guid tenantId)
        {
            var settings = await _settingsRepository.GetSettingsByTenantIdAsync(tenantId);
            if (settings == null)
            {
                throw new Exception("Settings not found for the tenant.");
            }

            var response = await _httpClient.GetAsync(settings.ApiRequestUrl);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(Guid tenantId, object data)
        {
            var settings = await _settingsRepository.GetSettingsByTenantIdAsync(tenantId);
            if (settings == null)
            {
                throw new Exception("Settings not found for the tenant.");
            }

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(settings.ApiRequestUrl, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}

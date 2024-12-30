using System;
using System.Threading.Tasks;

namespace SupplierAPI.Services
{
    public interface IRequestService
    {
        Task<string> GetAsync(Guid tenantId);
        Task<string> PostAsync(Guid tenantId, object data);
    }
}

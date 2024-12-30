namespace SupplierAPI.Domain.Models
{
    public class SettingsModel
    {
        public string ApiRequestUrl { get; set; }
        public Guid TenantId { get; set; }
    }
}

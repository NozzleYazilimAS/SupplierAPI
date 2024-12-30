using System;

namespace SupplierAPI.Domain.Entities
{
    public class Settings : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string ApiRequestUrl { get; set; }
    }
}

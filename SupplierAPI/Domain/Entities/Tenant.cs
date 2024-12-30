using System;

namespace SupplierAPI.Domain.Entities
{
    public class Tenant : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid TenantId { get; set; }
    }
}

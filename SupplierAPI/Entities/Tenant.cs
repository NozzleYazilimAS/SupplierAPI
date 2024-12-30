using System;

namespace SupplierAPI.Entities
{
    public class Tenant : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}

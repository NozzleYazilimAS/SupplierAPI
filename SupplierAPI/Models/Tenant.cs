using System;

namespace SupplierAPI.Models
{
    public class Tenant : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}

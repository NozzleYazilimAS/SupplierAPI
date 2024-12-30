using System;

namespace SupplierAPI.Entities
{
    public class User : AuditableEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid TenantId { get; set; }
    }
}

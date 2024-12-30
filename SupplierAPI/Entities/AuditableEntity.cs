using System;

namespace SupplierAPI.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public Guid? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}

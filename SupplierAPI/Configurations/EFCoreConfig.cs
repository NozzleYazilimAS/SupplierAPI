using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierAPI.Domain.Entities;

namespace SupplierAPI.Configurations
{
    public class AuditableEntityConfig : IEntityTypeConfiguration<AuditableEntity>
    {
        public void Configure(EntityTypeBuilder<AuditableEntity> builder)
        {
            builder.Property(a => a.CreationTime)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(a => a.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(a => a.CreatorId)
                .IsRequired(false);

            builder.Property(a => a.LastModificationTime)
                .IsRequired(false);

            builder.Property(a => a.LastModifierId)
                .IsRequired(false);

            builder.Property(a => a.DeleterId)
                .IsRequired(false);

            builder.Property(a => a.DeletionTime)
                .IsRequired(false);
        }
    }
}

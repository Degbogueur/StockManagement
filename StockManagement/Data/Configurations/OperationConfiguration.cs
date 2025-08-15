using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Models;

namespace StockManagement.Data.Configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasIndex(o => o.Code).IsUnique();
        builder.Property(o => o.Date).IsRequired();
        builder.Property(o => o.Type).IsRequired().HasConversion<string>();

        builder.HasMany(o => o.Rows)
            .WithOne(r => r.Operation)
            .HasForeignKey(r => r.OperationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

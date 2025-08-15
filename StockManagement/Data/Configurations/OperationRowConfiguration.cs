using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockManagement.Models;

namespace StockManagement.Data.Configurations;

public class OperationRowConfiguration : IEntityTypeConfiguration<OperationRow>
{
    public void Configure(EntityTypeBuilder<OperationRow> builder)
    {
        builder.Property(r => r.Date).IsRequired().HasColumnType("date");
    }
}

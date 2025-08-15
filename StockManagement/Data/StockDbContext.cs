using Microsoft.EntityFrameworkCore;
using StockManagement.Models;
using System.Reflection;

namespace StockManagement.Data;

public class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions<StockDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Company> Company { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<OperationRow> OperationRows { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
}

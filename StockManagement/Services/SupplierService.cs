using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Extensions;
using StockManagement.Models;
using StockManagement.Models.Result;
using StockManagement.Services.Interfaces;

namespace StockManagement.Services;

public class SupplierService(StockDbContext dbContext) : ISupplierService
{
    public async Task<PagedResult<Supplier>> GetAllAsync(int page, int pageSize, CancellationToken cancellation)
    {
        return await dbContext.Suppliers
            .AsNoTracking()
            .ToPagedResultAsync(page, pageSize, cancellation);
    }

    public async Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellation)
    {
        return await dbContext.Suppliers.FindAsync(id, cancellation);
    }

    public async Task<Supplier> CreateAsync(Supplier supplier, CancellationToken cancellation)
    {
        await dbContext.Suppliers.AddAsync(supplier, cancellation);
        await dbContext.SaveChangesAsync(cancellation);

        return supplier;
    }

    public async Task<bool> UpdateAsync(Supplier supplier, CancellationToken cancellation)
    {
        var updatedSupplier = await dbContext.Suppliers
            .Where(s => s.Id == supplier.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.Name, supplier.Name), cancellation);

        return updatedSupplier > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellation)
    {
        var deletedSupplier = await dbContext.Suppliers
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(cancellation);

        return deletedSupplier > 0;
    }
}

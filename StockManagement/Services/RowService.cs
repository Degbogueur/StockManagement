using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Services.Interfaces;

namespace StockManagement.Services;

public class RowService(StockDbContext dbContext) : IRowService
{
    public async Task CreateAsync(OperationRow row, CancellationToken cancellation)
    {
        await dbContext.OperationRows.AddAsync(row, cancellation);
        await dbContext.SaveChangesAsync(cancellation);
    }

    public async Task AddRangeAsync(List<OperationRow> rows, CancellationToken cancellation)
    {
        await dbContext.OperationRows.AddRangeAsync(rows, cancellation);
        await dbContext.SaveChangesAsync(cancellation);
    }

    public async Task<bool> UpdateAsync(OperationRow row, CancellationToken cancellation)
    {
        var updatedRow = await dbContext.OperationRows
            .Where(r => r.Id == row.Id)
            .ExecuteUpdateAsync(r => r
                .SetProperty(r => r.ProductId, row.ProductId)
                .SetProperty(r => r.EmployeeId, row.EmployeeId)
                .SetProperty(r => r.SupplierId, row.SupplierId)
                .SetProperty(r => r.Quantity, row.Quantity), cancellation);

        return updatedRow > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellation)
    {
        var deletedRow = await dbContext.OperationRows
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync(cancellation);

        return deletedRow > 0;
    }
}

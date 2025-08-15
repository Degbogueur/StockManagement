using StockManagement.Models;

namespace StockManagement.Services.Interfaces;

public interface IRowService
{
    Task CreateAsync(OperationRow row, CancellationToken cancellation);
    Task AddRangeAsync(List<OperationRow> rows, CancellationToken cancellation);
    Task<bool> UpdateAsync(OperationRow row, CancellationToken cancellation);
    Task<bool> DeleteAsync(int id, CancellationToken cancellation);
}

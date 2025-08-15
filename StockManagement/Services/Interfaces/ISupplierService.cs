using StockManagement.Models;
using StockManagement.Models.Result;

namespace StockManagement.Services.Interfaces;

public interface ISupplierService
{
    Task<PagedResult<Supplier>> GetAllAsync(int page, int pageSize, CancellationToken cancellation);
    Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellation);
    Task<Supplier> CreateAsync(Supplier supplier, CancellationToken cancellation);
    Task<bool> UpdateAsync(Supplier supplier, CancellationToken cancellation);
    Task<bool> DeleteAsync(int id, CancellationToken cancellation);
}

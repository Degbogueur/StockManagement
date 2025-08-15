using StockManagement.Models;
using StockManagement.Models.Result;

namespace StockManagement.Services.Interfaces;

public interface IProductService
{
    Task<PagedResult<Product>> GetAllAsync(int page, int pageSize, CancellationToken cancellation);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellation);
    Task CreateAsync(Product product, CancellationToken cancellation);
    Task<bool> UpdateAsync(Product product, CancellationToken cancellation);
    Task<bool> DeleteAsync(int id, CancellationToken cancellation);
    Task<int> GetActualQuantity(int productId, CancellationToken cancellation);
}

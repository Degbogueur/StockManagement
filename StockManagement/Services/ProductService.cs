using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Exceptions;
using StockManagement.Extensions;
using StockManagement.Models;
using StockManagement.Models.Result;
using StockManagement.Services.Interfaces;

namespace StockManagement.Services;

public class ProductService(StockDbContext dbContext, ILogger<ProductService> logger) : IProductService
{
    public async Task<PagedResult<Product>> GetAllAsync(
        int page = 1, int pageSize = 20, CancellationToken cancellation = default)
    {
        return await dbContext.Products
            .AsNoTracking()
            .ToPagedResultAsync(page, pageSize, cancellation);
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        return await dbContext.Products.FindAsync(id, cancellation);
    }

    public async Task CreateAsync(Product product, CancellationToken cancellation = default)
    {
        try
        {            
            if (await ProductExists(product.Name))
                throw new AlreadyExistsException(product.Name);

            product.NormalizedName = product.Name.ToNormalize();
            await dbContext.Products.AddAsync(product, cancellation);
            await dbContext.SaveChangesAsync(cancellation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de l'enregistrement du nouveau produit.");
            throw;
        }        
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellation = default)
    {
        var updatedProduct = await dbContext.Products
            .Where(p => p.Id == product.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(p => p.Description, product.Description)
                .SetProperty(p => p.AlertQuantity, product.AlertQuantity), cancellation);

        return updatedProduct > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellation = default)
    {
        var deletedProduct = await dbContext.Products
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync(cancellation);

        return deletedProduct > 0;
    }

    public async Task<int> GetActualQuantity(int productId, CancellationToken cancellation)
    {
        return await dbContext.Products
            .Where(p => p.Id == productId)
            .Select(p => p.ActualQuantity)
            .FirstOrDefaultAsync(cancellation);
    }

    private async Task<bool> ProductExists(string name, int? productId = null)
    {
        var normalizedName = name.ToNormalize();

        return await dbContext.Products
            .AnyAsync(p => p.NormalizedName == normalizedName
                        && (!productId.HasValue || p.Id != productId.Value));
    }
}

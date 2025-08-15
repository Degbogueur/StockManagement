using StockManagement.Models;

namespace StockManagement.Services.Interfaces;

public interface ICompanyService
{
    Task<Company> CreateAsync(Company company, CancellationToken cancellation);
    Task<Company?> GetFirstAsync(CancellationToken cancellation);
    Task<bool> UpdateAsync(Company company, CancellationToken cancellation);
}

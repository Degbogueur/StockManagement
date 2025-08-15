using StockManagement.Models;
using StockManagement.Models.Result;

namespace StockManagement.Services.Interfaces;

public interface IEmployeeService
{
    Task<PagedResult<Employee>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task CreateAsync(Employee employee, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Employee employee, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}

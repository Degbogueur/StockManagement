using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Extensions;
using StockManagement.Models;
using StockManagement.Models.Result;
using StockManagement.Services.Interfaces;

namespace StockManagement.Services;

public class EmployeeService(StockDbContext dbContext, ILogger<EmployeeService> logger) : IEmployeeService
{
    public async Task<PagedResult<Employee>> GetAllAsync(
        int page = 1, int pageSize = 20, CancellationToken cancellationToken = default)
    {
        return await dbContext.Employees
            .AsNoTracking()
            .ToPagedResultAsync(page, pageSize, cancellationToken);
    }

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Employees.FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        try
        {
            await dbContext.Employees.AddAsync(employee, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de l'enregistrement du nouvel employé.");
            throw;
        }
        
    }

    public async Task<bool> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        var updatedEmployee = await dbContext.Employees
            .Where(e => e.Id == employee.Id)
            .ExecuteUpdateAsync(e => e
                .SetProperty(e => e.FullName, employee.FullName), cancellationToken);

        return updatedEmployee > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var deletedEmployee = await dbContext.Employees
            .Where(e => e.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        return deletedEmployee > 0;
    }
}

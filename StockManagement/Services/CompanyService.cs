using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Services.Interfaces;

namespace StockManagement.Services;

public class CompanyService(StockDbContext dbContext) : ICompanyService
{
    public async Task<Company> CreateAsync(Company company, CancellationToken cancellation = default)
    {
        await dbContext.Company.AddAsync(company, cancellation);
        await dbContext.SaveChangesAsync(cancellation);

        return company;
    }

    public async Task<bool> UpdateAsync(Company company, CancellationToken cancellation = default)
    {
        var updatedCompany = await dbContext.Company
            .Where(c => c.Id == company.Id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.Name, company.Name)
                .SetProperty(c => c.Address, company.Address)
                .SetProperty(c => c.Website, company.Website), cancellation);

        return updatedCompany > 0;
    }
}

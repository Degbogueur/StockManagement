using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Services;
using StockManagement.Services.Interfaces;

namespace StockManagement.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StockDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("StockDatabase"));
        });
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IRowService, RowService>();
        services.AddScoped<ISupplierService, SupplierService>();
        return services;
    }
}

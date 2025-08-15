using Microsoft.AspNetCore.Mvc;
using StockManagement.Exceptions;
using StockManagement.Models;
using StockManagement.Services.Interfaces;

namespace StockManagement.Controllers
{
    public class CompanyController(ICompanyService companyService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellation = default)
        {
            var company = await companyService.GetFirstAsync(cancellation);
            if (company == null)
            {
                company = new Company { Name = string.Empty };
            }
            return View(company);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Company company, IFormFile? logoFile, CancellationToken cancellation = default)
        {
            try
            {
                if (logoFile != null && logoFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await logoFile.CopyToAsync(ms, cancellation);
                    company.Logo = ms.ToArray();
                }

                await companyService.CreateAsync(company, cancellation);
                TempData["StatusMessage"] = "Compagnie enregistrée avec succès.";
            }
            catch (BaseException ex)
            {
                TempData["StatusMessage"] = ex.Message;
            }
            catch
            {
                TempData["StatusMessage"] = "Une erreur s'est produite.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Company company, IFormFile? logoFile, CancellationToken cancellation = default)
        {
            try
            {
                if (logoFile != null && logoFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await logoFile.CopyToAsync(ms, cancellation);
                    company.Logo = ms.ToArray();
                }

                await companyService.UpdateAsync(company, cancellation);
                TempData["StatusMessage"] = "Informations mises à jour avec succès.";
            }
            catch
            {
                TempData["StatusMessage"] = "Une erreur s'est produite.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

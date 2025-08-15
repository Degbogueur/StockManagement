using Microsoft.AspNetCore.Mvc;
using StockManagement.Exceptions;
using StockManagement.Models;
using StockManagement.Services;
using StockManagement.Services.Interfaces;

namespace StockManagement.Controllers
{
    public class SupplierController(ISupplierService supplierService ) : Controller
    {
        public async Task<IActionResult> Index(int page = 1, int pageSize = 20, CancellationToken cancellation = default)
        {
            var supplier = await supplierService.GetAllAsync(page, pageSize, cancellation);
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier, CancellationToken cancellation = default)
        {
            try
            {
                await supplierService.CreateAsync(supplier, cancellation);
                TempData["StatusMessage"] = "Nouveau fournisseur enregistré avec succès.";
            }
            catch (BaseException ex)
            {
                TempData["StatusMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["StatusMessage"] = "Une erreur s'est produite.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier supplier, CancellationToken cancellation = default)
        {
            try
            {
                await supplierService.UpdateAsync(supplier, cancellation);
                TempData["StatusMessage"] = "Informations du fournisseur mises à jour avec succès.";
            }
            catch (Exception)
            {
                TempData["StatusMessage"] = "Une erreur s'est produite.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation = default)
        {
            try
            {
                await supplierService.DeleteAsync(id, cancellation);
                TempData["StatusMessage"] = "Fournisseur retiré avec succès.";
            }
            catch (Exception)
            {
                TempData["StatusMessage"] = "Une erreur s'est produite.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

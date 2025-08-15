using Microsoft.AspNetCore.Mvc;
using StockManagement.Exceptions;
using StockManagement.Models;
using StockManagement.Services.Interfaces;

namespace StockManagement.Controllers;

public class ProductsController(IProductService productService) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 20, CancellationToken cancellation = default)
    {
        var products = await productService.GetAllAsync(page, pageSize, cancellation);
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product, CancellationToken cancellation = default)
    {
        try
        {
            await productService.CreateAsync(product, cancellation);
            TempData["StatusMessage"] = "Nouveau produit enregistré avec succès.";
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
    public async Task<IActionResult> Edit(Product product, CancellationToken cancellation = default)
    {
        try
        {
            await productService.UpdateAsync(product, cancellation);
            TempData["StatusMessage"] = "Produit mis à jour avec succès.";
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
            await productService.DeleteAsync(id, cancellation);
            TempData["StatusMessage"] = "Produit retiré avec succès.";
        }
        catch (Exception)
        {
            TempData["StatusMessage"] = "Une erreur s'est produite.";
        }
        return RedirectToAction(nameof(Index));
    }
}

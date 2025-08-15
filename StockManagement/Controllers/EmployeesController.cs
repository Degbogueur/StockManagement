using Microsoft.AspNetCore.Mvc;
using StockManagement.Exceptions;
using StockManagement.Models;
using StockManagement.Services.Interfaces;

namespace StockManagement.Controllers;

public class EmployeesController(IEmployeeService employeeService) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 20, CancellationToken cancellation = default)
    {
        var employees = await employeeService.GetAllAsync(page, pageSize, cancellation);
        return View(employees);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee employee, CancellationToken cancellation = default)
    {
        try
        {
            await employeeService.CreateAsync(employee, cancellation);
            TempData["StatusMessage"] = "Nouvel employé enregistré avec succès.";
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
    public async Task<IActionResult> Edit(Employee employee, CancellationToken cancellation = default)
    {
        try
        {
            await employeeService.UpdateAsync(employee, cancellation);
            TempData["StatusMessage"] = "Informations de l'employé mises à jour avec succès.";
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
            await employeeService.DeleteAsync(id, cancellation);
            TempData["StatusMessage"] = "Employé retiré avec succès.";
        }
        catch (Exception)
        {
            TempData["StatusMessage"] = "Une erreur s'est produite.";
        }
        return RedirectToAction(nameof(Index));
    }
}

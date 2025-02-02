using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers;

public class TransactionController(ExpenseTrackerDbContext context) : Controller
{
    // GET: Transaction
    public async Task<IActionResult> Index()
    {
        var expenseTrackerDbContext = context.Transactions.Include(t => t.Category);
        return View(await expenseTrackerDbContext.ToListAsync());
    }

    // GET: Transaction/Upsert
    public IActionResult Upsert(int id = 0)
    {
        PopulateCategories();

        return View(id == 0 ? new Transaction() : context.Transactions.Find(id));
    }

    // POST: Transaction/Upsert
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert([Bind("TransactionId,CategoryId,Amount,Note,Date")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            transaction.Category = await context.Categories.FindAsync(transaction.CategoryId);

            if (transaction.TransactionId == 0)
            {
                context.Add(transaction);
            }
            else
            {
                context.Update(transaction);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        PopulateCategories();
        return View(transaction);
    }

    // POST: Transaction/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction != null) context.Transactions.Remove(transaction);

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [NonAction]
    private void PopulateCategories()
    {
        var categoryCollection = context.Categories.ToList();

        var defaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category" };

        categoryCollection.Insert(0, defaultCategory);

        ViewBag.Categories = categoryCollection;
    }
}
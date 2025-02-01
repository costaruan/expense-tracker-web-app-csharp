using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers;

public class CategoryController(ExpenseTrackerDbContext context) : Controller
{
    // GET: Category
    public async Task<IActionResult> Index()
    {
        return View(await context.Categories.ToListAsync());
    }

    // GET: Category/Upsert
    public IActionResult Upsert(int id = 0)
    {
        return View(id == 0 ? new Category() : context.Categories.Find(id));
    }

    // POST: Category/Upsert
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert([Bind("CategoryId,Title,Icon,Type")] Category category)
    {
        if (!ModelState.IsValid) return View(category);

        if (category.CategoryId == 0)
            context.Add(category);
        else
            context.Update(category);

        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // POST: Category/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await context.Categories.FindAsync(id);

        if (category != null)
        {
            context.Categories.Remove(category);
        }

        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
using System.Globalization;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers;

public class DashboardController(ExpenseTrackerDbContext context) : Controller
{
    // GET
    public async Task<ActionResult> Index()
    {
        // Last 7 Days
        var startDate = DateTime.Today.AddDays(-6);
        var endDate = DateTime.Today;

        List<Transaction> selectedTransactions = await context.Transactions
            .Include(t => t.Category)
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .ToListAsync();

        // Total Income
        var totalIncome = selectedTransactions
            .Where(t => t.Category is { Type: "Income" })
            .Sum(t => t.Amount);

        ViewBag.TotalIncome = totalIncome.ToString("C0");

        // Total Expense
        var totalExpense = selectedTransactions
            .Where(t => t.Category is { Type: "Expense" })
            .Sum(t => t.Amount);

        ViewBag.TotalExpense = totalExpense.ToString("C0");

        // Balance
        var balance = totalIncome - totalExpense;

        var culture = CultureInfo.CreateSpecificCulture("en-US");
        culture.NumberFormat.CurrencyNegativePattern = 1;

        ViewBag.Balance = string.Format(culture, "{0:C0}", balance);

        // Doughnut Chart - Expense by Category
        ViewBag.DoughnutChartData = selectedTransactions
            .Where(t => t.Category is { Type: "Expense" })
            .GroupBy(t => t.CategoryId)
            .Select(t => new
            {
                categoryTitleWithIcon = t.First().Category?.Icon + " " + t.First().Category?.Title,
                amount = t.Sum(x => x.Amount),
                formattedAmount = t.Sum(x => x.Amount).ToString("C0"),
            })
            .ToList();

        return View();
    }
}
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data;

public class ExpenseTrackerDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
}
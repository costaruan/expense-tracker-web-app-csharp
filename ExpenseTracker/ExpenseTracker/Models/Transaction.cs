using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models;

public class Transaction
{
    [Key] public int TransactionId { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = new Category();

    public int Amount { get; set; }

    [Column(TypeName = "varchar(128)")] public string? Note { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;
}
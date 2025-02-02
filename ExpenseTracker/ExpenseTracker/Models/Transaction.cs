using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace ExpenseTracker.Models;

public class Transaction
{
    [Key] public int TransactionId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a Category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0")]
    public int Amount { get; set; }

    [Column(TypeName = "varchar(128)")] public string? Note { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    [NotMapped] public string? CategoryTitleWithIcon => Category == null ? "" : Category.Icon + " " + Category.Title;

    [NotMapped]
    public string? FormattedAmount =>
        ((Category == null || Category.Type == "Expense") ? "- " : "+ ") +
        Amount.ToString("C0", CultureInfo.GetCultureInfo("en-US"));
}
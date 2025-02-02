using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models;

public class Category
{
    [Key] public int CategoryId { get; set; }

    [Column(TypeName = "nvarchar(64)")] public string Title { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(8)")] public string Icon { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(16)")] public string Type { get; set; } = "Expense";

    [NotMapped] public string? TitleWithIcon => Icon + " " + Title;
}
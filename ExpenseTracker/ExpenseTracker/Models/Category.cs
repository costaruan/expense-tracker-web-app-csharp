using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models;

public class Category
{
    [Key] public int CategoryId { get; set; }

    [Column(TypeName = "varchar(64)")] public string Title { get; set; } = string.Empty;

    [Column(TypeName = "varchar(8)")] public string Icon { get; set; } = string.Empty;

    [Column(TypeName = "varchar(16)")] public string Type { get; set; } = string.Empty;
}
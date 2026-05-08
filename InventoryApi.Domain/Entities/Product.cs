using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Domain.Entities;

public class Product
{
    [Key]
    public int IdProduct { get; set; }

    //[Required]
    //[MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    //[Range(0.01, 999999)]
    public decimal Price { get; set; }

    //[Range(0, int.MaxValue)]
    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

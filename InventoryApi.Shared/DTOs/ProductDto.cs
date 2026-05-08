

namespace InventoryApi.Shared.DTOs;

public class ProductDto
{
    public int IdProduct { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }
}
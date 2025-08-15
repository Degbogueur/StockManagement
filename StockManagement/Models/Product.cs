namespace StockManagement.Models;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public string NormalizedName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ActualQuantity { get; set; }
    public int AlertQuantity { get; set; }

}

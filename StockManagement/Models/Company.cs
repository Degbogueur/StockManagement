namespace StockManagement.Models;

public class Company : BaseEntity
{
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? Website { get; set; }
}

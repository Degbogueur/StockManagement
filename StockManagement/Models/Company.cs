namespace StockManagement.Models;

public class Company : BaseEntity
{
    public byte[]? Logo { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? Website { get; set; }
    public string? PhoneNumber { get; set; }
}

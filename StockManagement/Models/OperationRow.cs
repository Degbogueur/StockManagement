namespace StockManagement.Models;

public class OperationRow : BaseEntity
{
    public int OperationId { get; set; }
    public Operation? Operation { get; set; }
    public int ProductId { get; set; }
    public Employee? Product { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
}

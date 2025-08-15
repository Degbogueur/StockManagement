namespace StockManagement.Models;

public class Operation : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public OperationType Type { get; set; }

    public ICollection<OperationRow> Rows { get; set; } = [];
}

public enum OperationType
{
    StockIn,
    StockOut
}
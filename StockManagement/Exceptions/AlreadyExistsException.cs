namespace StockManagement.Exceptions;

public class AlreadyExistsException(string entityName) : BaseException($"{entityName} déjà existant.")
{
}

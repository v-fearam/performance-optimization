
namespace MonolithicPersistence
{
    public interface IDataAccess
    {
        Task InsertPurchaseOrderHeaderAsync(string cnStr);
        Task LogAsync(string cnStr, string logTableName);
    }
}
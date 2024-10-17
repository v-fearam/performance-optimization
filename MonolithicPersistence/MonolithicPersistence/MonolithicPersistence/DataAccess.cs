using Microsoft.Data.SqlClient;
using MonolithicPersistence.Models;

namespace MonolithicPersistence
{
    public class DataAccess : IDataAccess
    {
        public async Task InsertPurchaseOrderHeaderAsync(string cnStr)
        {
            const string queryString =
                "INSERT INTO SalesLT.SalesOrderHeader " +
                "(RevisionNumber, Status, OrderDate, ShipDate, SubTotal, TaxAmt, Freight, ModifiedDate, DueDate, CustomerId, ShipMethod) " +
                "VALUES " +
                "(@RevisionNumber, @Status, @OrderDate, @ShipDate, @SubTotal, @TaxAmt, @Freight, @ModifiedDate, @DueDate, @CustomerId, @ShipMethod)";

            var dt = DateTime.UtcNow;

            using (var cn = new SqlConnection(cnStr))
            {
                using (var cmd = new SqlCommand(queryString, cn))
                {
                    cmd.Parameters.AddWithValue("@RevisionNumber", 1);
                    cmd.Parameters.AddWithValue("@Status", 4);
                    cmd.Parameters.AddWithValue("@OrderDate", dt);
                    cmd.Parameters.AddWithValue("@ShipDate", dt);
                    cmd.Parameters.AddWithValue("@SubTotal", 123.40M);
                    cmd.Parameters.AddWithValue("@TaxAmt", 12.34M);
                    cmd.Parameters.AddWithValue("@Freight", 5.76M);
                    cmd.Parameters.AddWithValue("@ModifiedDate", dt);
                    cmd.Parameters.AddWithValue("@DueDate", dt);
                    cmd.Parameters.AddWithValue("@CustomerId", 29847);
                    cmd.Parameters.AddWithValue("@ShipMethod", "CARGO TRANSPORT 10");

                    await cn.OpenAsync().ConfigureAwait(false);
                    await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
        }
        public async Task LogAsync(string cnStr, string logTableName)
        {
            string queryString = "INSERT INTO dbo." + logTableName + " (ErrorMessage, ErrorTime, UserName, ErrorNumber) VALUES (@Message, @LogTime, @UserName, @ErrorNumber)";

            var logMessage = new LogMessage();

            using (var cn = new SqlConnection(cnStr))
            {
                using (var cmd = new SqlCommand(queryString, cn))
                {
                    cmd.Parameters.AddWithValue("@Message", logMessage.Message);
                    cmd.Parameters.AddWithValue("@LogTime", logMessage.LogTime);
                    cmd.Parameters.AddWithValue("@UserName", "mspnp");
                    cmd.Parameters.AddWithValue("@ErrorNumber", logMessage.ErrorNumber);

                    await cn.OpenAsync().ConfigureAwait(false);
                    await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
        }
    }
}

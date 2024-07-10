using SalesOrder.Data.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace SalesOrderTest.DBSource
{
    public class DataSource : IDataSource
    {
        private readonly string _connectionString;

        public DataSource(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
        }
    }
}

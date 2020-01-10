using System;
using System.Data.SqlClient;

namespace Logger
{
    public interface IDbConnection
    {
        SqlConnection GetConnection();
        SqlConnection GetReadOnlyConnection();
    }

    public abstract class DbConnection : IDbConnection
    {
        private readonly string _connectionString;
        private readonly string _readOnlyConnectionString;

        public DbConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{this.GetType().Name}_string_has_not_found");

            _connectionString = connectionString;
        }

        public DbConnection(string connectionString, string readOnlyConnectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{this.GetType().Name}_string_has_not_found");

            if (string.IsNullOrEmpty(readOnlyConnectionString))
                throw new ArgumentException($"{this.GetType().Name}_ReadOnly_string_has_not_found");

            _connectionString = connectionString;
            _readOnlyConnectionString = readOnlyConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlConnection GetReadOnlyConnection()
        {
            if (string.IsNullOrEmpty(_readOnlyConnectionString))
                throw new ArgumentException($"{this.GetType().Name}_ReadOnly_string_has_not_found");

            return new SqlConnection(_readOnlyConnectionString);
        }
    }
}

namespace Logger
{
    public interface ILocalDBConnection : IDbConnection
    {
    }

    public class LocalDBConnection : DbConnection, ILocalDBConnection
    {
        public LocalDBConnection(string connectionString) : base(connectionString)
        {
        }

        public LocalDBConnection(string connectionString, string readOnlyConnectionString) : base(connectionString, readOnlyConnectionString)
        {
        }
    }
}

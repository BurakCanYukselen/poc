using Dapper;
using System.Threading.Tasks;

namespace Logger
{
    public interface IRepository
    {
        Task<int> Insert(Log log);
    }
    public class Repository : IRepository
    {
        private readonly ILocalDBConnection _localDBConnection;

        public Repository(ILocalDBConnection localDBConnection)
        {
            this._localDBConnection = localDBConnection;
        }

        public async Task<int> Insert(Log log)
        {
            var query = @"
                          INSERT INTO MFLogPOC..TrackerLog (
                              ProcessId
                              ,OperationName
                              ,Message
                              ,StartedAt
                              )
                          VALUES (
                              @ProcessId
                              ,@OperationName
                              ,@Message
                              ,@StartedAt
                              )
                         ";
            using (var conn = _localDBConnection.GetConnection())
            {
                conn.Open();
                return await conn.ExecuteAsync(query, log);
            }
        }
    }
}

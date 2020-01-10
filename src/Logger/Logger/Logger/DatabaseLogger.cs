namespace Logger
{

    public interface ILogger
    {
        void Log(Log log);
    }

    public class DatabaseLogger : ILogger
    {
        private readonly IRepository _repository;

        public DatabaseLogger(IRepository repository)
        {
            this._repository = repository;
        }

        public void Log(Log log)
        {
            _repository.Insert(log).ConfigureAwait(false);
        }
    }


}

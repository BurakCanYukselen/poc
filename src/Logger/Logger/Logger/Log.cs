using System;

namespace Logger
{
    public class Log
    {
        public Guid ProcessId { get; set; }
        public string OperationName { get; set; }
        public DateTimeOffset StartedAt { get; set; }
        public string Message { get; set; }
    }


}

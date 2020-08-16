namespace SignalRServer.POC.Models
{
    public class AbstractHubMessage<TMessage>
    {
        public string From { get; set; }
        public string To { get; set; }
        public TMessage Message { get; set; }       
    }
}
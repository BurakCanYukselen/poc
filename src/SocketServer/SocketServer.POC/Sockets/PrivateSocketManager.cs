namespace SocketServer.POC.Sockets
{
    public interface IPrivateSocketManager : ISocketManager
    {
    }
    
    public class PrivateSocketManager: AbstractSocketManager, IPrivateSocketManager
    {
    }
}
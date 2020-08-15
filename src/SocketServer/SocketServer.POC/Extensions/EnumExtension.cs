using System;

namespace SocketServer.POC.Extensions
{
    public static class EnumExtension
    {
        public static ArraySegment<byte> GetArraySegment(this object source)
        {
            return new ArraySegment<byte>(new byte[(int) source]);
        }
    }
}
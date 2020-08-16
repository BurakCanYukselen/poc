using System;

namespace SocketServer.POC.Extensions
{
    public static class EnumExtension
    {
        public static ArraySegment<byte> GetArraySegment(this object source, out byte[] buffer)
        {
            buffer = new byte[(int) source];
            return new ArraySegment<byte>(buffer);
        }
    }
}
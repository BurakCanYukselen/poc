using System;
using System.Text;
using Newtonsoft.Json;

namespace SocketServer.POC.Extensions
{
    public static class StringExtension
    {
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }

        public static TModel FromJson<TModel>(this string source)
        {
            return JsonConvert.DeserializeObject<TModel>(source);
        }

        public static ArraySegment<byte> ToByteArraySegment(this string source)
        {
            var bytes = Encoding.UTF8.GetBytes(source);
            return new ArraySegment<byte>(bytes, 0, source.Length);
        } 
    }
}
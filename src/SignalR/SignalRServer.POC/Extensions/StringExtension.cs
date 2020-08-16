using Newtonsoft.Json;

namespace SignalRServer.POC.Extensions
{
    public static class StringExtension
    {
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }

        public static TObject FromJson<TObject>(this string source)
        {
            return JsonConvert.DeserializeObject<TObject>(source);
        }
    }
}
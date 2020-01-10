using System;
using System.Collections.Generic;

namespace ConsolePOC.Extensions
{
    public static class EnumExtension
    {
        public static T GetEnumFlags<T>(this IEnumerable<string> list) where T : struct
        {
            if (typeof(T).IsEnum)
            {
                if (Enum.TryParse(string.Join(",", list), out T result))
                    return result;

                return default;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}

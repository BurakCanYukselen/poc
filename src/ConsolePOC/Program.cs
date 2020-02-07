using ConsolePOC.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConsolePOC
{
    static class Program
    {
        static void Main(string[] args)
        {
            ParseCultureInvariantPOC();
        }

        #region Parse Culture Invariant POC
        public static void ParseCultureInvariantPOC()
        {
            var doubleStr = "7.1";
            var value = double.Parse(doubleStr, CultureInfo.InvariantCulture);
            Console.WriteLine(value);
        }


        #endregion

        #region Newtonsoft Enum Conversion POC
        private static void NewtonsoftEnumConversionPOC()
        {
            var json1 = "{\"Enum\" : 2}";
            var json2 = "{\"Enum\" : 5}";

            var conversion1 = JsonConvert.DeserializeObject<NewtonsoftEnumConversionClass>(json1);
            var conversion2 = JsonConvert.DeserializeObject<NewtonsoftEnumConversionClass>(json2);

            Console.WriteLine($"conversion 1 Enum string: {conversion1.Enum.ToString()}");
            Console.WriteLine($"conversion 2 Enum string: {conversion2.Enum.ToString()}");

            Console.WriteLine($"Cast 2 to Enum(B) : {((NewtonsoftEnumConversionEnum)2).ToString()}");
            Console.WriteLine($"Cast 5 to Enum(not exist) : {((NewtonsoftEnumConversionEnum)5).ToString()}");
        }

        public class NewtonsoftEnumConversionClass
        {
            public NewtonsoftEnumConversionEnum Enum { get; set; }
        }

        public enum NewtonsoftEnumConversionEnum
        {
            None = 0,
            A = 1,
            B = 2
        }
        #endregion

        #region String To Enum Flag POC

        private static void StringToEnumFlagPOC()
        {
            var segmentString = "E|E1|E2|E3";
            var segmentList = segmentString.Split("|");
            var segmentFlag = segmentList.GetEnumFlags<SegmentType>();
            var validSegment = SegmentType.A;
            var ifValid = segmentFlag.HasFlag(validSegment);

            Console.WriteLine(ifValid);
        }

        public enum SegmentType
        {
            None = 0,
            A = 1,
            B = 2,
            C = 3,
            D = 4,
            E1 = 5,
            E2 = 6,
            E3 = 7,
            MS = 8,
            E = 9,
            V = 10,
            VD = 11
        }

        #endregion

        #region Predicate POC

        public static void PredicatePOC()
        {
            var list1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var list2 = new List<int> { 1, 2 };

            var result = list1.GetDataExistsIn(list2, p => p);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        public class A
        {
            public int a { get; set; }
            public int b { get; set; }
            public int c { get; set; }
        }

        #endregion
    }
}

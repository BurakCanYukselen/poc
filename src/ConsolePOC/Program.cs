using ConsolePOC.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConsolePOC
{
    static class Program
    {
        static void Main(string[] args)
        {
            StringToEnumFlagPOC();
            Console.ReadKey();
        }


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

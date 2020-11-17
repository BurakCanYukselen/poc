
using ConsolePOC.Extensions;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace ConsolePOC
{
    static class Program
    {

        static void Main(string[] args)
        {
        }

        #region Timezone Change from UTC to Target Timezone POC
        static void TimezoneChangeFromUTCtoTargetTimezonePOC()
        {
            var timeZoneId = "Pacific Standard Time";
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var utcTime = DateTime.UtcNow;
            var pacificTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timezoneInfo);

            Console.WriteLine($"UTC Time: {utcTime}");
            Console.WriteLine($"Pacific Time: {pacificTime}");

        }
        #endregion

        #region List Left Join POC

        private static void ListLeftJoinPOC()
        {
            var a = new List<ListLeftJoinPOC_A>() {
                new ListLeftJoinPOC_A { Id = 1, Name="A_1" },
                new ListLeftJoinPOC_A { Id = 2, Name="A_2" },
                new ListLeftJoinPOC_A { Id = 3, Name="A_3" },
                new ListLeftJoinPOC_A { Id = 4, Name="A_4" },
            };
            var b = new List<ListLeftJoinPOC_B>() {
                new ListLeftJoinPOC_B { Id = 1, Name="B_1" },
                new ListLeftJoinPOC_B { Id = 3, Name="B_3" },
                new ListLeftJoinPOC_B { Id = 3, Name="B_33" },
            };
            
            var c = a.ToHashSet().LeftJoin(b.ToHashSet(), a => a.Id, b => b.Id);

            Console.WriteLine(c.ToJson());

        }

        public class ListLeftJoinPOC_A
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class ListLeftJoinPOC_B
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        #endregion

        #region List Distinct POC
        public static void ListDistinctPOC()
        {
            var a = new ListDistinctPOC_Class { Id = 1, Name = "A" };
            var a1 = new ListDistinctPOC_Class { Id = 1, Name = "A" };
            var a2 = new ListDistinctPOC_Class { Id = 1, Name = "A" };

            var b = new ListDistinctPOC_Class { Id = 2, Name = "B" };
            var c = new ListDistinctPOC_Class { Id = 3, Name = "C" };

            var rawlist = new List<ListDistinctPOC_Class>() { a, a1, a2, b, c };
            var distinctList = rawlist.ToList().DistinctBy(p => p.Id);

            Console.WriteLine(distinctList.ToJson(Formatting.Indented));
        }

        public class ListDistinctPOC_Class
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }


        #endregion

        #region Datetime UTC Local Comparison
        private static void DatetimeUTCLocalComparison()
        {
            var endDate = new DateTime(2020, 2, 13, 21, 0, 0, DateTimeKind.Utc);
            var deliveryDate = new DateTime(2020, 2, 14, 12, 14, 32, DateTimeKind.Utc);

            Console.WriteLine($"endDate: {endDate}");
            Console.WriteLine($"deliveryDate: {deliveryDate}");

            Console.WriteLine("----");

            Console.WriteLine($"IsEqual: {deliveryDate.ToLocalTime().Date <= endDate.ToLocalTime().Date }");
        }
        #endregion

        #region System.Timer Operations POC
        private static void SystemTimerOperationsPOC()
        {
            Console.WriteLine("başladı");

            int _idleTimeout = (int)TimeSpan.FromSeconds(2).TotalMilliseconds;
            Timer _idleTimer = new Timer(new TimerCallback((s) => { Console.WriteLine("time"); }), null, int.MaxValue, 0);


            Console.WriteLine("3.sn bekle");
            Console.WriteLine("time yazmaması lazım");
            Thread.Sleep(TimeSpan.FromSeconds(3));

            Console.WriteLine("timer start 2sn sonra");
            _idleTimer.Change(_idleTimeout, 0);

            Console.WriteLine("3sn bekle");
            Console.WriteLine("3sn sonra time yazması lazım");
            Thread.Sleep(TimeSpan.FromSeconds(3));


            Console.WriteLine("timer durdur");
            _idleTimer.Change(int.MaxValue, 0);

            Console.WriteLine("3sn bekle");
            Console.WriteLine("time yazmaması lazım");
            Thread.Sleep(TimeSpan.FromSeconds(3));

            Console.WriteLine("kapandı");
        }
        #endregion

        // Result = Not Bulk implicitly loop insert
        #region Dapper Bulk Insert POC
        public static void DapperBulkInsertPOC()
        {
            var conString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True";

            var guidList = new List<object>();
            for (int i = 0; i < 1000; i++)
                guidList.Add(new { value = Guid.NewGuid() });

            var query = @"insert into BulkInsertTest(Guid) values(@value)";

            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                conn.Execute(query, guidList);
            }
        }
        #endregion

        #region String Format POC

        public static void StringFormatPOC()
        {
            Console.WriteLine(string.Format("{0}.dbo.{1}", null, "table"));
        }

        #endregion

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

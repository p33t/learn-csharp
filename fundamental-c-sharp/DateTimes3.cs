using System;
using System.Diagnostics;
using System.Globalization;

namespace fundamental_c_sharp
{
    public class DateTimes3
    {
        public static void Demo()
        {
            Console.WriteLine("DateTimes3 ========================================================");
            var utcNow = DateTime.UtcNow;
            var nowStr = utcNow.ToString("O");

            // Beware parsing DateTime strings. You need the magic "RoundTripKind" parameter.
            var badDate = DateTime.SpecifyKind(DateTime.Parse(nowStr), DateTimeKind.Utc); // surprise, this is bad
            Debug.Assert(utcNow != badDate);
            var goodDate = DateTime.Parse(nowStr, null, DateTimeStyles.RoundtripKind); // the right way
            Debug.Assert(utcNow == goodDate);
        }
    }
}
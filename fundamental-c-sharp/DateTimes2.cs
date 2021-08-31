using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public class DateTimes2
    {
        public static void Demo()
        {
            Console.WriteLine("DateTimes2 ========================================================");
            var utcCalendarDate = new DateTime(2021, 9, 1, 0, 0, 0, DateTimeKind.Utc);
            var localCalendarDate = new DateTime(2021, 9, 1, 0, 0, 0, DateTimeKind.Local);
            var unspecifiedCalendarDate = new DateTime(2021, 9, 1, 0, 0, 0, DateTimeKind.Unspecified);
            
            // DateTime has an extra component 'kind' which is independent of 'ticks'
            // Only 'ticks' are used for comparison
            Debug.Assert(utcCalendarDate.Ticks == localCalendarDate.Ticks);
            Debug.Assert(utcCalendarDate == localCalendarDate);
            Debug.Assert(utcCalendarDate.Equals(localCalendarDate));

            // Rendering an ISO 8601 string depends on the 'kind' component
            // So 'equal' DateTime values output different ISO 8601 values (ouch!)
            Debug.Assert("2021-09-01T00:00:00.0000000Z" == utcCalendarDate.ToString("O"));
            Debug.Assert("2021-09-01T00:00:00.0000000-07:00" == localCalendarDate.ToString("O"));
            Debug.Assert("2021-09-01T00:00:00.0000000" == unspecifiedCalendarDate.ToString("O"));
            
            // General practice is to assume UTC values are used exclusively on the server
            // (ignoring the 'kind' which is commonly 'unspecified' but occasionally 'utc')
            // To obtain an ISO 8601 string in this scenario entails assigning the kind before using ToString("O")
            var iso8601 = DateTime.SpecifyKind(unspecifiedCalendarDate, DateTimeKind.Utc).ToString("O");
            Debug.Assert("2021-09-01T00:00:00.0000000Z" == iso8601);
        }
    }
}
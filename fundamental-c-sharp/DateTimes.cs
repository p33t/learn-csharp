using System;
using System.Diagnostics;
using System.Transactions;

namespace fundamental_c_sharp
{
    public class DateTimes
    {
        public static void Demo()
        {
            Console.WriteLine("DateTimes =========================");
            // DateTime is immutable struct
            
            var someDate = new DateTime(2020, 7, 20); // July 20
            
            // The 'Kind' defaults to 'Unspecified'
            Trace.Assert(DateTimeKind.Unspecified == someDate.Kind);
            
            // Internally stored as 'ticks' so can add any unit
            var someDateTime = someDate.AddMinutes(390); // 6:30 AM
            Trace.Assert(DateTimeKind.Unspecified == someDateTime.Kind);
            Trace.Assert(6 == someDateTime.Hour);
            Trace.Assert(20 == someDateTime.Day);
            // "Unspecified" kind is treated like Zulu-time (?)
            Trace.Assert("Mon, 20 Jul 2020 06:30:00 GMT" == someDateTime.ToString("R"));

            // No timezone is stored, so ToLocalTime() assumes any non-Local kind needs converting
            // Display is always in 'zulu' time
            // NOTE: This was written in Pacific Daylight Time (GMT-7)
            var localDateTime = someDateTime.ToLocalTime();
            Trace.Assert(DateTimeKind.Local == localDateTime.Kind);
            Trace.Assert(23 == localDateTime.Hour);
            Trace.Assert(19 == localDateTime.Day);
            Trace.Assert("Sun, 19 Jul 2020 23:30:00 GMT" == localDateTime.ToString("R"));
            Trace.Assert(localDateTime.Equals(localDateTime.ToLocalTime()));
            Trace.Assert(localDateTime == localDateTime.ToLocalTime());
            
            // Any non-Utc kind is assumed to need converting for ToUniversalTime() 
            var universalTime = someDateTime.ToUniversalTime();
            Trace.Assert(13 == universalTime.Hour);
            Trace.Assert(20 == universalTime.Day);
            Trace.Assert(DateTimeKind.Utc == universalTime.Kind);
            Trace.Assert(universalTime.Equals(universalTime.ToUniversalTime()));
            Trace.Assert(universalTime == universalTime.ToUniversalTime());

            // Converting from 'Local' to 'Universal' does as expected but is only one 'hop'
            // local and universal were calculated from 'unspecified' originally so they are two hops apart.
            var altUniversal = localDateTime.ToUniversalTime();
            Trace.Assert("Mon, 20 Jul 2020 06:30:00 GMT" == altUniversal.ToString("R"));
            Trace.Assert(6 == altUniversal.Hour);
            Trace.Assert(20 == altUniversal.Day);
            Trace.Assert(DateTimeKind.Utc == altUniversal.Kind);

            // Same as altUniversal only now 'Kind' is 'Local'
            var altLocal = universalTime.ToLocalTime();
            Trace.Assert(DateTimeKind.Local == altLocal.Kind);
            Trace.Assert(6 == altLocal.Hour);
            Trace.Assert(20 == altLocal.Day);
            
            // equals examines the 'moment' in time
            // Trace.Assert(someDateTime.Equals(localDateTime)); ===> False
            // Trace.Assert(someDateTime.Equals(universalTime)); ===> False
            // Trace.Assert(someDateTime.Ticks == universalTime.Ticks); ===> False
            // Trace.Assert(altUniversal.Equals(universalTime)); ===> False 

            // Console.WriteLine(universalTime.ToString("R"));
            // Console.WriteLine(universalTime.ToUniversalTime().ToString("R"));
        }
    }
}
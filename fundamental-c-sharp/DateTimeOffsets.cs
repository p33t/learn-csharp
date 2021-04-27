using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public static class DateTimeOffsets
    {
        public static void Demo()
        {
            Console.WriteLine("DateTimeOffsets ============");
            var ts10 = new TimeSpan(0, 10, 0, 0);

            var dto = new DateTimeOffset(2020, 7, 24, 0, 0, 0, ts10);
            
            Trace.Assert(dto.DateTime.Kind == DateTimeKind.Unspecified);

            // Throws "The UTC Offset for Utc DateTime instances must be 0. (Parameter 'offset')"
            // new DateTimeOffset(DateTime.UtcNow, ts10);

            var dtoUniversal = dto.ToUniversalTime();
            Trace.Assert(dtoUniversal.DateTime.Kind == DateTimeKind.Unspecified); // stupid
            Trace.Assert(dtoUniversal.UtcDateTime.Equals(new DateTime(
                2020, 7, 23, 14, 0, 0, DateTimeKind.Utc
                )));
        }
    }
}
using System;

namespace fundamental_c_sharp
{
    public class DateTimes2
    {
        public static void Demo()
        {
            Console.WriteLine("DateTimes2 ========================================================");
            var utcCalendarDate = new DateTime(2021, 9, 1, 0, 0, 0, DateTimeKind.Utc);
            var localCalendarDate = new DateTime(2021, 9, 1, 0, 0, 0, DateTimeKind.Local);

            var utcNow = DateTime.UtcNow;

            var utcCalendarCurrentTimeOfDay = utcCalendarDate.Add(utcNow.TimeOfDay);
            var localCalendarCurrentTimeOfDay = localCalendarDate.Add(utcNow.TimeOfDay);

            Console.WriteLine($"Name\t\t\t\tTime\t\t\t\t\tTicks");
            Console.WriteLine($"{nameof(utcCalendarDate)}\t\t\t{utcCalendarDate:O}\t\t{utcCalendarDate.Ticks}");
            Console.WriteLine($"{nameof(utcCalendarCurrentTimeOfDay)}\t{utcCalendarCurrentTimeOfDay:O}\t\t{utcCalendarCurrentTimeOfDay.Ticks}");
            Console.WriteLine($"{nameof(localCalendarDate)}\t\t{localCalendarDate:O}\t{localCalendarDate.Ticks}");
            Console.WriteLine($"{nameof(localCalendarCurrentTimeOfDay)}\t{localCalendarCurrentTimeOfDay:O}\t{localCalendarCurrentTimeOfDay.Ticks}");

            Console.WriteLine($"Calendar dates equal? {utcCalendarDate == localCalendarDate}");
            Console.WriteLine($"Calendar dates with times of day equal? {utcCalendarCurrentTimeOfDay == localCalendarCurrentTimeOfDay}");            
        }
    }
}
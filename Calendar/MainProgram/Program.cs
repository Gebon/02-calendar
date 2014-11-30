using System;
using Calendar;
using CalendarPageViewGenerator;

namespace MainProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = DateTime.Now;
            var firstDayOfWeek = DayOfWeek.Monday;
            if (args.Length == 2 && (!DateTime.TryParse(args[0], out date) || !Enum.TryParse(args[1], true, out firstDayOfWeek)) || (args.Length != 0 && args.Length != 2))
            {
                Console.WriteLine(@"Usage:   calendar.exe <date> <first day of week>");
                Console.WriteLine(@"Example: calendar.exe 25.01.2145 Monday");
                Console.WriteLine(@"Execute without args to generate for current date");
                Environment.Exit(0);
            }

            var calendarPageView = new CalendarPageImageGenerator(new CalendarPageGenerator().GenerateCalendarPage(date, firstDayOfWeek)).GenerateView();
            calendarPageView.Save("tmp.png");
        }
    }
}

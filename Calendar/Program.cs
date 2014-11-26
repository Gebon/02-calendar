using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = new DateTime();
            if (args.Length != 0 && !DateTime.TryParse(args[0], out date))
            {
                Console.WriteLine("Usage example: calendar.exe 25.01.2145");
                Environment.Exit(0);
            }

            if (args.Length == 0)
                date = DateTime.Now;

            var calendarPageView = CalendarPageViewGenerator.GenerateCalendarPageViewForDate(date);
            calendarPageView.Save("tmp.png");
        }
    }
}

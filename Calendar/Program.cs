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
            var date = DateTime.Parse("26.11.2014");
            CalendarPageViewGenerator.GenerateCalendarPageForDate(DateTime.Now);

        }
    }
}

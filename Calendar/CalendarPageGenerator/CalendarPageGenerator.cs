using System;
using System.Globalization;
using System.Linq;

namespace Calendar
{
    public class CalendarPageGenerator
    {
        private readonly GregorianCalendar calendar = new GregorianCalendar();
        private int startWeek;
        private int endWeek;
        public CalendarPageGenerator()
        {
        }

        public CalendarPage GenerateCalendarPage(DateTime targetDate)
        {
            startWeek = GetWeekOfYear(targetDate.Year, targetDate.Month, 1);
            endWeek = GetWeekOfYear(targetDate.Year, targetDate.Month, GetDaysInMonth(targetDate));

            var items = Enumerable.Range(1, GetDaysInMonth(targetDate))
                .Select(day => GetCalendarItemForDay(targetDate.Year, targetDate.Month, day));

            return new CalendarPage(items, startWeek, endWeek, targetDate.Day, targetDate.Year, targetDate.Month);
        }

        private int GetDaysInMonth(DateTime targetDate)
        {
            return calendar.GetDaysInMonth(targetDate.Year, targetDate.Month);
        }

        private CalendarItem GetCalendarItemForDay(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            var dayOfWeek = (int) date.DayOfWeek;             //days of week in DateTime start from Sunday
            dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek) - 1; //so I change it numbering to our local numbering
            var week = GetWeekOfYear(year, month, day);
            var calendarItem = new CalendarItem(week - startWeek, dayOfWeek, day);
            return calendarItem;
        }

        private int GetWeekOfYear(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
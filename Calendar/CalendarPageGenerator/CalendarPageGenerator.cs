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

        public CalendarPage GenerateCalendarPage(DateTime targetDate, DayOfWeek firstDayOfWeek)
        {

            startWeek = GetWeekOfYear(targetDate.Year, targetDate.Month, 1, firstDayOfWeek);
            endWeek = GetWeekOfYear(targetDate.Year, targetDate.Month, GetDaysInMonth(targetDate), firstDayOfWeek);

            var items = Enumerable.Range(1, GetDaysInMonth(targetDate))
                .Select(day => GetCalendarItemForDay(targetDate.Year, targetDate.Month, day, firstDayOfWeek));

            return new CalendarPage(items,targetDate, firstDayOfWeek);
        }

        private int GetDaysInMonth(DateTime targetDate)
        {
            return calendar.GetDaysInMonth(targetDate.Year, targetDate.Month);
        }

        private CalendarItem GetCalendarItemForDay(int year, int month, int day, DayOfWeek firstDayOfWeek)
        {
            var date = new DateTime(year, month, day);
            var tmp = (int) firstDayOfWeek;
            var dayOfWeek = (int) date.DayOfWeek - tmp;             //days of week in DateTime start from Sunday
            dayOfWeek = (dayOfWeek < 0 ? 7 + dayOfWeek : dayOfWeek); //so I change it numbering to our local numbering
            var week = GetWeekOfYear(year, month, day, firstDayOfWeek);
            var calendarItem = new CalendarItem(week - startWeek, dayOfWeek, day, dayOfWeek == 5 || dayOfWeek == 6, date.DayOfWeek);
            return calendarItem;
        }

        private int GetWeekOfYear(int year, int month, int day, DayOfWeek firstDayOfWeek)
        {
            var date = new DateTime(year, month, day);
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, firstDayOfWeek);
        }
    }
}
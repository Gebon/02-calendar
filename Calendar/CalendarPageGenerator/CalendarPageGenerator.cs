using System;
using System.Globalization;
using System.Linq;

namespace Calendar
{
    public class CalendarPageGenerator
    {
        private readonly GregorianCalendar calendar = new GregorianCalendar();
        private int startWeek;

        public CalendarPage GenerateCalendarPage(DateTime targetDate, DayOfWeek firstDayOfWeek)
        {

            startWeek = GetWeekOfYear(targetDate.Year, targetDate.Month, 1, firstDayOfWeek);

            var determinant = new DeterminantTheDayOff(targetDate.Year);

            var items = Enumerable.Range(1, GetDaysInMonth(targetDate))
                .Select(day => GetCalendarItemForDay(targetDate.Year, targetDate.Month, day, firstDayOfWeek, determinant));

            return new CalendarPage(items,targetDate, firstDayOfWeek);
        }

        private int GetDaysInMonth(DateTime targetDate)
        {
            return calendar.GetDaysInMonth(targetDate.Year, targetDate.Month);
        }

        private CalendarItem GetCalendarItemForDay(int year, int month, int day, DayOfWeek firstDayOfWeek, DeterminantTheDayOff determinant)
        {
            var date = new DateTime(year, month, day);
            var tmp = (int) firstDayOfWeek;
            var dayInWeek = (int) date.DayOfWeek - tmp;
            dayInWeek = (dayInWeek < 0 ? 7 + dayInWeek : dayInWeek);
            var week = GetWeekOfYear(year, month, day, firstDayOfWeek);
            var calendarItem = new CalendarItem(week - startWeek, dayInWeek, day, determinant.IsDayOff(month, day, dayInWeek), date.DayOfWeek);
            return calendarItem;
        }

        private int GetWeekOfYear(int year, int month, int day, DayOfWeek firstDayOfWeek)
        {
            var date = new DateTime(year, month, day);
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, firstDayOfWeek);
        }
    }
}
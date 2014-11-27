using System;
using System.Globalization;
using System.Linq;

namespace Calendar
{
    public class CalendarPageGenerator
    {
        private DateTime targetDate;
        private readonly GregorianCalendar calendar = new GregorianCalendar();
        private int startWeek;
        private int endWeek;
        public CalendarPageGenerator(DateTime targetDate)
        {
            this.targetDate = targetDate;
        }

        public CalendarPage GenerateCalendarPage()
        {
            startWeek = GetWeekOfYearForTargetMonth(1);
            endWeek = GetWeekOfYearForTargetMonth(GetDaysInTargetMonth());

            var items = Enumerable.Range(1, GetDaysInTargetMonth())
                .Select(GetCalendarItemForDay);

            return new CalendarPage(items, startWeek, endWeek, targetDate.Day, targetDate.Year, targetDate.Month);
        }

        private int GetDaysInTargetMonth()
        {
            return calendar.GetDaysInMonth(targetDate.Year, targetDate.Month);
        }

        private CalendarItem GetCalendarItemForDay(int day)
        {
            var date = new DateTime(targetDate.Year, targetDate.Month, day);
            var dayOfWeek = (int) date.DayOfWeek;             //days of week in DateTime start from Sunday
            dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek) - 1; //so I change it numbering to our local numbering
            var week = GetWeekOfYearForTargetMonth(day);
            var calendarItem = new CalendarItem(week - startWeek, dayOfWeek, day);
            return calendarItem;
        }

        private int GetWeekOfYearForTargetMonth(int day)
        {
            var date = new DateTime(targetDate.Year, targetDate.Month, day);
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
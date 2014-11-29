using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Calendar
{
    public class CalendarPage : IEnumerable<CalendarItem>
    {
        public int StartWeek { get; private set; }
        public int EndWeek { get; private set; }
        public CalendarItem ItemFotIllumination { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }
        public DayOfWeek FirstDayOfWeek { get; private set; }

        private readonly List<CalendarItem> items = new List<CalendarItem>();

        public CalendarPage(IEnumerable<CalendarItem> items, DateTime date, DayOfWeek firstDayOfWeek)
        {
            FirstDayOfWeek = firstDayOfWeek;
            var calendar = new GregorianCalendar();
            Year = date.Year;
            Month = date.Month;

            var firstDayOfMonth = new DateTime(Year, Month, 1);
            var lastDayOfMonth = new DateTime(Year, Month, calendar.GetDaysInMonth(date.Year, date.Month));

            StartWeek = calendar.GetWeekOfYear(firstDayOfMonth, CalendarWeekRule.FirstDay, FirstDayOfWeek);
            EndWeek = calendar.GetWeekOfYear(lastDayOfMonth, CalendarWeekRule.FirstDay, FirstDayOfWeek);

            foreach (var calendarItem in items)
            {
                this.items.Add(calendarItem);
                if (calendarItem.DayOfMonth == date.Day)
                    ItemFotIllumination = calendarItem;
            }

        }

        public IEnumerator<CalendarItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
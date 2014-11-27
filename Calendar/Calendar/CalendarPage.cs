using System.Collections;
using System.Collections.Generic;

namespace Calendar
{
    public class CalendarPage : IEnumerable<CalendarItem>
    {
        public int StartWeek { get; private set; }
        public int EndWeek { get; private set; }
        public CalendarItem ItemFotIllumination { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

        private readonly List<CalendarItem> items = new List<CalendarItem>();

        public CalendarPage(IEnumerable<CalendarItem> items, int startWeek, int endWeek, int dayForIllumination,
            int year, int month)
        {
            StartWeek = startWeek;
            EndWeek = endWeek;
            Year = year;
            Month = month;
            foreach (var calendarItem in items)
            {
                this.items.Add(calendarItem);
                if (calendarItem.DayOfMonth == dayForIllumination)
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
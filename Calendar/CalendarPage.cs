using System.Collections;
using System.Collections.Generic;

namespace Calendar
{
    public class CalendarPage : IEnumerable<CalendarItem>
    {
        public int StartWeek { get; private set; }
        public int EndWeek { get; private set; }
        public CalendarItem ItemFotIllumination { get; private set; }

        private List<CalendarItem> items = new List<CalendarItem>();

        public CalendarPage(IEnumerable<CalendarItem> items, int startWeek, int endWeek, CalendarItem itemForIllumination)
        {
            StartWeek = startWeek;
            EndWeek = endWeek;
            this.items.AddRange(items);
            ItemFotIllumination = itemForIllumination;
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
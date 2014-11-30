using System;

namespace Calendar
{
    public class CalendarItem
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int DayOfMonth { get; private set; }
        public bool IsDayOff { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }

        public CalendarItem(int row, int column, int dayOfMonth, bool isDayOff, DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
            Row = row;
            Column = column;
            IsDayOff = isDayOff;
            if (dayOfMonth < 1 || dayOfMonth > 31)
                throw new ArgumentException("Day of month must be in range [1, 31]", "dayOfMonth");
            DayOfMonth = dayOfMonth;
        }
    }
}
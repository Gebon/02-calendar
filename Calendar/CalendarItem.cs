namespace Calendar
{
    public class CalendarItem
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int DayOfMonth { get; private set; }

        public CalendarItem(int row, int column, int dayOfMonth)
        {
            Row = row;
            Column = column;
            DayOfMonth = dayOfMonth;
        }
    }
}
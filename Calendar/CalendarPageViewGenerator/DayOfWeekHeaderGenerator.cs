using System;
using System.Collections.Generic;

namespace CalendarPageViewGenerator
{
    public class DayOfWeekHeaderGenerator
    {
        private readonly Dictionary<DayOfWeek, string> dayOfWeekName = new Dictionary<DayOfWeek, string>
        {
            {DayOfWeek.Monday, "Mon"},
            {DayOfWeek.Tuesday, "Tue"},
            {DayOfWeek.Wednesday, "Wed"},
            {DayOfWeek.Thursday, "Thu"},
            {DayOfWeek.Friday, "Fri"},
            {DayOfWeek.Saturday, "Sat"},
            {DayOfWeek.Sunday, "Sun" }
        };

        public string GetHeaderForDayInWeek(int dayInWeek, DayOfWeek firstDayOfWeek)
        {
            var tmp = (int) firstDayOfWeek;
            return dayOfWeekName[(DayOfWeek) Enum.Parse(typeof (DayOfWeek), ((dayInWeek + tmp - 1)%7).ToString())];
        }
    }
}
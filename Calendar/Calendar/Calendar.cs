using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Calendar
{
    public static class Calendar
    {
        public static CalendarPage GetCalendarPageForDate(DateTime date)
        {
            var startWeek = GetWeekForDate(new DateTime(date.Year, date.Month, 1));
            var endWeek = GetWeekForDate(new DateTime(date.Year, date.Month, GetDaysInMonth(date)));
            var items = new List<CalendarItem>();
            CalendarItem itemForIllumination = null;
            foreach (var day in Enumerable.Range(1, GetDaysInMonth(date)))
            {
                var calendarItem = GetCalendarItemForDay(date.Year, date.Month, day, startWeek);
                items.Add(calendarItem);
                if (day == date.Day)
                    itemForIllumination = calendarItem;
            }
            var result = new CalendarPage(items, startWeek, endWeek, itemForIllumination);
            return result;
        }

        private static CalendarItem GetCalendarItemForDay(int year, int month, int day, int startWeek)
        {
            var tmpDate = new DateTime(year, month, day);
            var dayOfWeek = (int) tmpDate.DayOfWeek;
            var dayInWeek = (dayOfWeek == 0 ? 7 : dayOfWeek) - 1;
            var week = GetWeekForDate(tmpDate);
            var calendarItem = new CalendarItem(week - startWeek, dayInWeek, day);
            return calendarItem;
        }

        private static int GetDaysInMonth(DateTime date)
        {
            var gC = new GregorianCalendar();
            return gC.GetDaysInMonth(date.Year, date.Month);
//            var isLeapYear = date.Year % 4 == 0 && date.Year % 100 != 0 || date.Year % 400 == 0;
//            if (date.Month == 2)
//                return 28 + (isLeapYear ? 1 : 0);
//            if (date.Month < 8)
//                return date.Month % 2 == 0 ? 30 : 31;
//            return date.Month % 2 == 0 ? 31 : 30;
        }

        private static int GetWeekForDate(DateTime date)
        {
            var gC = new GregorianCalendar();

            return gC.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
//            var dayOfYear = date.DayOfYear;
//
//            var firstDayOfYear = new DateTime(date.Year, 1, 1);
//            var dayInWeek = (int)firstDayOfYear.DayOfWeek;
//            dayInWeek = dayInWeek == 0 ? 7 : dayInWeek;
//            dayInWeek -= 1;
//
//            var week = (dayOfYear + dayInWeek - 1) / 7;
//            return week + 1;
        }
//
//        [Test]
//        public static void GetsCorrectWeek()
//        {
//            for (int year = 1; year <= 2014; year++)
//            {
//                for (int month = 1; month <= 12; month++)
//                {
//                    var days = DateTime.DaysInMonth(year, month);
//                    for (int day = 1; day <= days; day++)
//                    {
//                        var date = new DateTime(year, month, day);
//                        var c = new GregorianCalendar();
//                        var gregorianWeek = c.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
//                        var myWeek = GetWeekForDate(date);
//                        Assert.AreEqual(gregorianWeek, myWeek);
//                    }
//                }
//            }
//        }
    }
}
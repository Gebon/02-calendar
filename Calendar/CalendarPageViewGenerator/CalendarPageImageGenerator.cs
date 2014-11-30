using System;
using System.Drawing;
using Calendar;
using CalendarPageViewGenerator.AdditionalGenerator;
using CalendarPageViewGenerator.Interfaces;

namespace CalendarPageViewGenerator
{
    public class CalendarPageImageGenerator : BaseBitmapGenerator, ICalendarPageViewGenerator<Bitmap>

    {
        #region Constants and properities declaration


        private const int DaysInWeekCount = 7;

        private const int SpriteWidth = 105;
        private const int SpriteHeight = 105;

        private const int WeeksColumnWidth = 100;
        private int WeeksColumnHeight { get; set; }

        private const int MainHeaderHeight = 100;

        private const int DaysOfWeekHeaderHeight = 50;
        private const int DaysOfWeekHeaderWidth = PageWidth - WeeksColumnWidth;

        private const int PageWidth = SpriteWidth * DaysInWeekCount + WeeksColumnWidth;
        private int PageHeight { get; set; }


        private readonly CalendarPage page;
        private readonly int weeksCount;

        #endregion
        public CalendarPageImageGenerator(CalendarPage page)
        {
            this.page = page;
            weeksCount = page.EndWeek - page.StartWeek + 1;
            WeeksColumnHeight = SpriteHeight * weeksCount + DaysOfWeekHeaderHeight;
            PageHeight = WeeksColumnHeight + MainHeaderHeight;
        }

        public Bitmap GenerateView()
        {
            var calendarGrid = new CalendarGridGenerator(DaysInWeekCount, weeksCount, page).GenerateBitmap();
            var header = new CalendarMainHeaderGenerator(MainHeaderHeight, PageWidth, page).GenerateBitmap();
            var daysOfWeekHeader = new CalendarDaysOfWeekHeaderGenerator(DaysOfWeekHeaderWidth, DaysOfWeekHeaderHeight, page).GenerateBitmap();
            var weeksColumn = new CalendarWeeksNumbersColumnGenerator(WeeksColumnWidth, WeeksColumnHeight, page).GenerateBitmap();

            Action<Graphics> componeCalendarModules = g =>
            {
                g.FillRectangle(Brushes.White, 0, 0, PageWidth, PageHeight);
                g.DrawImage(header, 0, 0);
                g.DrawImage(daysOfWeekHeader, PageWidth - DaysOfWeekHeaderWidth, PageHeight - WeeksColumnHeight);
                g.DrawImage(weeksColumn, 0, PageHeight - WeeksColumnHeight);
                g.DrawImage(calendarGrid, PageWidth - calendarGrid.Width, PageHeight - calendarGrid.Height);
            };

            return GenerateBitmap(PageWidth, PageHeight, componeCalendarModules);
        }
    }
}

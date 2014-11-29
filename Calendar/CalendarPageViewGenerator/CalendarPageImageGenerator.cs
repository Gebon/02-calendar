﻿using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Calendar;
using CalendarPageViewGenerator.Properties;

namespace CalendarPageViewGenerator
{
    public class CalendarPageImageGenerator : ICalendarPageViewGenerator<Bitmap>
    {
        #region Constants and properities declaration

        private readonly string[] monthNames = { "January", "February", "March", "April", "May", "June", "Jule", "August", "September", "October", "November", "December" };

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
            var calendarGrid = GenerateCalendarGrid();
            var header = GenerateHeader();
            var daysOfWeekHeader = GenerateDaysOfWeekHeader();
            var weeksColumn = GenerateWeeksColumn();

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

        private Bitmap GenerateWeeksColumn()
        {
            return GenerateBitmap(WeeksColumnWidth, WeeksColumnHeight, DrawWeeksColumn);
        }

        private void DrawWeeksColumn(Graphics g)
        {
            g.DrawString("#", new Font("Calibri", 30), Brushes.Blue, WeeksColumnWidth * 0.35f, DaysOfWeekHeaderHeight * 0.3f);
            foreach (var week in Enumerable.Range(page.StartWeek, page.EndWeek - page.StartWeek + 1))
            {
                var index = week - page.StartWeek + 1;
                g.DrawString(week.ToString(CultureInfo.InvariantCulture), new Font("Calibri", 30),
                    Brushes.Blue, WeeksColumnWidth * 0.35f, (index - 0.8f) * SpriteHeight + DaysOfWeekHeaderHeight);
            }
        }

        private Bitmap GenerateDaysOfWeekHeader()
        {
            return GenerateBitmap(DaysOfWeekHeaderWidth, DaysOfWeekHeaderHeight, DrawDaysOfWeekHeader);
        }

        private void DrawDaysOfWeekHeader(Graphics g)
        {
            foreach (var dayInWeek in Enumerable.Range(1, 7))
            {
                var dayOfWeekHeaderGenerator = new DayOfWeekHeaderGenerator();
                g.DrawString(dayOfWeekHeaderGenerator.GetHeaderForDayInWeek(dayInWeek, page.FirstDayOfWeek), new Font("Calibri", 20), Brushes.Blue,
                    SpriteWidth * (dayInWeek - 0.7f),
                    DaysOfWeekHeaderHeight * 0.1f);
            }
        }

        private Bitmap GenerateHeader()
        {
            return GenerateBitmap(PageWidth, MainHeaderHeight, DrawMainHeader);
        }

        private void DrawMainHeader(Graphics g)
        {
            g.DrawString(monthNames[page.Month - 1], new Font("Calibri", 30),
                Brushes.Blue, 50f, MainHeaderHeight * 0.4f);

            g.DrawString(page.Year.ToString(CultureInfo.InvariantCulture), new Font("Calibri", 30),
                Brushes.Blue, PageWidth - 130, MainHeaderHeight * 0.4f);
        }

        private Bitmap GenerateCalendarGrid()
        {
            return GenerateBitmap(SpriteWidth * DaysInWeekCount, SpriteHeight * weeksCount, DrawCalendarGrid);
        }

        private void DrawCalendarGrid(Graphics g)
        {
            var sprites = Resources.sprite_bg;
            foreach (var calendarItem in page)
            {
                var sprite = GetSpriteForItem(page, calendarItem, sprites);

                g.DrawImage(sprite, calendarItem.Column * SpriteWidth, calendarItem.Row * SpriteHeight);

                var dX = calendarItem.Column * SpriteWidth + SpriteWidth / 2 - 15;
                var dY = SpriteHeight * (calendarItem.Row + 0.5f) - 15;
                g.DrawString(calendarItem.DayOfMonth.ToString(CultureInfo.InvariantCulture),
                    new Font("Arial", 16, FontStyle.Regular), calendarItem.IsHolidayOrWeekend ? Brushes.Red : Brushes.Blue, dX, dY);
            }
        }

        private static void Draw(Bitmap bitmap, Action<Graphics> drawer)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                drawer(g);
            }
        }

        private static Bitmap GenerateBitmap(int width, int height, Action<Graphics> drawer)
        {
            var bitmap = new Bitmap(width, height);
            Draw(bitmap, drawer);
            return bitmap;
        }

        private static Image GetSpriteForItem(CalendarPage page, CalendarItem calendarItem, Image sprites)
        {
            var tmp = calendarItem == page.ItemFotIllumination
                ? CropImage(sprites, 0, SpriteHeight * 2, SpriteWidth, SpriteHeight)
                : CropImage(sprites, 0, 0, SpriteWidth, SpriteHeight);
            return tmp;
        }

        private static Image CropImage(Image image, int x, int y, int width, int height)
        {
            var cropRect = new Rectangle(x, y, width, height);
            var target = new Bitmap(cropRect.Width, cropRect.Height);

            using (var g = Graphics.FromImage(target))
            {
                g.DrawImage(image, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            return target;
        }
    }
}

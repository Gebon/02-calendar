using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public static class CalendarPageViewGenerator
    {
        const int DaysInWeekCount = 7;
        public static Bitmap GenerateCalendarPageViewForDate(DateTime date)
        {
            var monthNames = new Dictionary<int, string>
            {
                {1, "January"},
                {2, "February"},
                {3, "March"},
                {4, "April"},
                {5, "May"},
                {6, "June"},
                {7, "Jule"},
                {8, "August"},
                {9, "September"},
                {10, "October"},
                {11, "November"},
                {12, "December"}
            };
            const int spriteWidth = 105;
            const int spriteHeight = 105;

            var page = Calendar.GetCalendarPageForDate(date);
            var weeksCount = page.EndWeek - page.StartWeek + 1;

            const int weeksColumnWidth = 100;
            var pageWidth = spriteWidth * 7 + weeksColumnWidth;
            var daysOfWeekWidth = pageWidth - weeksColumnWidth;

            const int headerHeight = 100;
            const int daysOfWeekHeight = 50;
            var weeksColumnHeight = spriteHeight * weeksCount + daysOfWeekHeight;
            var pageHeight = weeksColumnHeight + headerHeight;

            var bitmap = new Bitmap(pageWidth, pageHeight);

            var calendarGrid = GenerateCalendarGrid(spriteWidth, spriteHeight, weeksCount, page);
            var header = GenerateHeader(monthNames[date.Month], date.Year, pageWidth, headerHeight);
            var daysOfWeekHeader = GenerateDaysOfWeekHeader(daysOfWeekWidth, daysOfWeekHeight, spriteWidth);
            Bitmap weeksColumn = GenerateWeeksColumn(page.StartWeek, page.EndWeek, weeksColumnWidth, weeksColumnHeight, spriteHeight, daysOfWeekHeight);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(header, 0, 0);
                g.DrawImage(daysOfWeekHeader, pageWidth - daysOfWeekWidth, pageHeight - weeksColumnHeight);
                g.DrawImage(weeksColumn, 0, pageHeight - weeksColumnHeight);
                g.DrawImage(calendarGrid, bitmap.Width - calendarGrid.Width, bitmap.Height - calendarGrid.Height);
            }

            return bitmap;
        }

        private static Bitmap GenerateWeeksColumn(int startWeek, int endWeek, int width, int height, int spriteHeight, int space)
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawString("#", new Font("Calibri", 30), Brushes.Blue, width * 0.35f, space * 0.3f);
                foreach (var week in Enumerable.Range(startWeek, endWeek - startWeek + 1))
                {
                    var index = week - startWeek + 1;
                    g.DrawString(week.ToString(), new Font("Calibri", 30), Brushes.Blue, width * 0.35f, (index - 0.8f)  * spriteHeight + space);
                }
            }
            return bitmap;
        }

        private static Bitmap GenerateDaysOfWeekHeader(int width, int height, int spriteWidth)
        {
            var bitmap = new Bitmap(width, height);
            var dayOfWeekName = new Dictionary<int, string>
            {
                {1, "Mon"},
                {2, "Tue"},
                {3, "Wed"},
                {4, "Thu"},
                {5, "Fri"},
                {6, "Sat"},
                {7, "Sun"}
            };

            using (var g = Graphics.FromImage(bitmap))
            {
                foreach (var dayOfWeek in Enumerable.Range(1, 7))
                {
                    g.DrawString(dayOfWeekName[dayOfWeek], new Font("Calibri", 20), Brushes.Blue,
                        (float)(spriteWidth * (dayOfWeek  - 0.7)),
                        (float)(height*0.1));
                }
            }

            return bitmap;
        }

        private static Bitmap GenerateHeader(string month, int year, int width, int height)
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawString(month, new Font("Calibri", 30), Brushes.Blue, 50f, (float)(height / 2 - height * 0.1));
                g.DrawString(year.ToString(), new Font("Calibri", 30), Brushes.Blue, width - 130, (float)(height / 2 - height * 0.1));
            }
            return bitmap;
        }

        private static Bitmap GenerateCalendarGrid(int spriteWidth, int spriteHeight, int weeksCount, CalendarPage page)
        {
            var bitmap = new Bitmap(spriteWidth * DaysInWeekCount, spriteHeight * weeksCount);
            var sprites = Image.FromFile("sprite-bg.gif");

            using (var g = Graphics.FromImage(bitmap))
            {
                foreach (var calendarItem in page)
                {
                    var tmp = calendarItem == page.ItemFotIllumination
                        ? CropImage(sprites, 0, spriteHeight * 2, spriteWidth, spriteHeight)
                        : CropImage(sprites, 0, 0, spriteWidth, spriteHeight);

                    g.DrawImage(tmp, calendarItem.Column * spriteWidth, calendarItem.Row * spriteHeight);

                    var dX = calendarItem.Column * spriteWidth + spriteWidth / 2 - 15;
                    var dY = calendarItem.Row * spriteHeight + spriteHeight / 2 - 15;
                    g.TranslateTransform(dX, dY);
                    g.DrawString(calendarItem.DayOfMonth.ToString(),
                        new Font("Arial", 16, FontStyle.Regular), calendarItem.Column == 5 || calendarItem.Column == 6 ? Brushes.Red : Brushes.Blue, 0, 0);
                    g.TranslateTransform(-dX, -dY);
                }
            }
            return bitmap;
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

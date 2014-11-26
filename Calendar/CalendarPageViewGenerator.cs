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
        public static Bitmap GenerateCalendarPageForDate(DateTime date)
        {
            const int spriteWidth = 105;
            const int spriteHeight = 105;
            var bitmap = new Bitmap(spriteWidth * 8, spriteHeight * 7 + 70);
            var sprites = Image.FromFile("sprite-bg.gif");
            using (var g = Graphics.FromImage(bitmap))
            {
                var page = Calendar.GetCalendarPageForDate(date);
                foreach (var calendarItem in page)
                {
                    var tmp = calendarItem == page.ItemFotIllumination ?
                        CropImage(sprites, 0, spriteHeight * 2, spriteWidth, spriteHeight) :
                        CropImage(sprites, 0, 0, spriteWidth, spriteHeight);

                    g.DrawImage(tmp, calendarItem.Column * spriteWidth, calendarItem.Row * spriteHeight);

                    var dX = calendarItem.Column * spriteWidth + spriteWidth / 2 - 15;
                    var dY = calendarItem.Row * spriteHeight + spriteHeight / 2 - 15;
                    g.TranslateTransform(dX, dY);
                    g.DrawString(calendarItem.DayOfMonth.ToString(), new Font("Arial", 16, FontStyle.Regular), Brushes.Blue, 0, 0);
                    g.TranslateTransform(-dX, -dY);
                }
            }
            bitmap.Save("bit.png");
            return null;
        }

        private static Image CropImage(Image image, int x, int y, int width, int height)
        {
            var cropRect = new Rectangle(x, y, width, height);
            var target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(image, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            return target;
        }
    }
}

using System.Drawing;
using System.Globalization;
using Calendar;
using CalendarPageViewGenerator.Properties;

namespace CalendarPageViewGenerator.AdditionalGenerator
{
    public class CalendarGridGenerator : BaseBitmapGenerator
    {
        private readonly int daysInWeekCount;
        private readonly int weeksCount;
        private readonly CalendarPage page;

        private readonly int spriteWidth;
        private readonly int spriteHeight;
        private readonly Bitmap sprites;
        public CalendarGridGenerator(int daysInWeekCount, int weeksCount, CalendarPage page)
        {
            sprites = Resources.sprite_bg;
            spriteHeight = sprites.Width;
            spriteWidth = sprites.Width;
            this.daysInWeekCount = daysInWeekCount;
            this.weeksCount = weeksCount;
            this.page = page;
        }
        public Bitmap GenerateBitmap()
        {
            return GenerateBitmap(daysInWeekCount * spriteWidth, weeksCount * spriteHeight, DrawCalendarGrid);
        }

        private void DrawCalendarGrid(Graphics g)
        {
            foreach (var calendarItem in page)
            {
                var sprite = GetSpriteForItem(calendarItem);

                g.DrawImage(sprite, calendarItem.Column * spriteWidth, calendarItem.Row * spriteHeight);

                var dX = calendarItem.Column * spriteWidth + spriteWidth / 2 - 15;
                var dY = spriteHeight * (calendarItem.Row + 0.5f) - 15;
                g.DrawString(calendarItem.DayOfMonth.ToString(CultureInfo.InvariantCulture),
                    new Font("Arial", 16, FontStyle.Regular), calendarItem.IsDayOff ? Brushes.Red : Brushes.Blue, dX, dY);
            }
        }

        private Image GetSpriteForItem(CalendarItem calendarItem)
        {
            var tmp = calendarItem == page.ItemFotIllumination
                ? CropImage(sprites, 0, spriteHeight * 2, spriteWidth, spriteHeight)
                : CropImage(sprites, 0, 0, spriteWidth, spriteHeight);
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
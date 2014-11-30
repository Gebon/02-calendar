using System.Drawing;
using System.Linq;
using Calendar;
using CalendarPageViewGenerator.Properties;

namespace CalendarPageViewGenerator.AdditionalGenerator
{
    public class CalendarDaysOfWeekHeaderGenerator : BaseBitmapGenerator
    {
        private readonly int spriteWidth;
        private readonly int height;
        private readonly int width;
        private readonly CalendarPage page;

        public CalendarDaysOfWeekHeaderGenerator(int width, int height, CalendarPage page)
        {
            spriteWidth = Resources.sprite_bg.Width;
            this.height = height;
            this.width = width;
            this.page = page;
        }

        public Bitmap GenerateBitmap()
        {
            return GenerateBitmap(width, height, DrawDaysOfWeekHeader);
        }
        private void DrawDaysOfWeekHeader(Graphics g)
        {
            foreach (var dayInWeek in Enumerable.Range(1, 7))
            {
                var dayOfWeekHeaderGenerator = new DayOfWeekNameGenerator();
                g.DrawString(dayOfWeekHeaderGenerator.GetHeaderForDayInWeek(dayInWeek, page.FirstDayOfWeek), new Font("Calibri", 20), Brushes.Blue,
                    spriteWidth * (dayInWeek - 0.7f),
                    height * 0.1f);
            }
        }
    }
}
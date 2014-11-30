using System.Drawing;
using System.Globalization;
using System.Linq;
using Calendar;
using CalendarPageViewGenerator.Properties;

namespace CalendarPageViewGenerator.AdditionalGenerator
{
    public class CalendarWeeksNumbersColumnGenerator : BaseBitmapGenerator
    {
        private readonly int width;
        private readonly int height;
        private readonly int spriteHeight;
        private readonly CalendarPage page;

        public CalendarWeeksNumbersColumnGenerator(int width, int height, CalendarPage page)
        {
            spriteHeight = Resources.sprite_bg.Width;
            this.width = width;
            this.height = height;
            this.page = page;
        }

        public Bitmap GenerateBitmap()
        {
            return GenerateBitmap(width, height, DrawWeeksColumn);
        }
        private void DrawWeeksColumn(Graphics g)
        {
            g.DrawString("#", new Font("Calibri", 30), Brushes.Blue, width * 0.35f, height * 5e-3f);
            foreach (var week in Enumerable.Range(page.StartWeek, page.EndWeek - page.StartWeek + 1))
            {
                var index = week - page.StartWeek + 1;
                g.DrawString(week.ToString(CultureInfo.InvariantCulture), new Font("Calibri", 30),
                    Brushes.Blue, width * 0.35f, (index - 0.3f) * spriteHeight);
            }
        }
    }
}
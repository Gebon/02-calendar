using System.Drawing;
using System.Globalization;
using Calendar;

namespace CalendarPageViewGenerator.AdditionalGenerator
{
    public class CalendarMainHeaderGenerator : BaseBitmapGenerator
    {
        private readonly string[] monthNames = { "January", "February", "March", "April", "May", "June", "Jule", "August", "September", "October", "November", "December" };

        private readonly CalendarPage page;
        private readonly int height;
        private readonly int width;

        public CalendarMainHeaderGenerator(int height, int width, CalendarPage page)
        {
            this.height = height;
            this.width = width;
            this.page = page;
        }

        public Bitmap GenerateBitmap()
        {
            return GenerateBitmap(width, height, DrawMainHeader);
        }
        private void DrawMainHeader(Graphics g)
        {
            g.DrawString(monthNames[page.Month - 1], new Font("Calibri", 30),
                Brushes.Blue, 50f, height * 0.4f);

            g.DrawString(page.Year.ToString(CultureInfo.InvariantCulture), new Font("Calibri", 30),
                Brushes.Blue, width - 130, height * 0.4f);
        }
    }
}
using System;
using System.Drawing;

namespace CalendarPageViewGenerator.AdditionalGenerator
{
    public class BaseBitmapGenerator
    {
        private void Draw(Bitmap bitmap, Action<Graphics> drawer)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                drawer(g);
            }
        }

        protected Bitmap GenerateBitmap(int width, int height, Action<Graphics> drawer)
        {
            var bitmap = new Bitmap(width, height);
            Draw(bitmap, drawer);
            return bitmap;
        }
    }
}

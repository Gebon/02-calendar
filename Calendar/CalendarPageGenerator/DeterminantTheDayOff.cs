using System;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace Calendar
{
    public class DeterminantTheDayOff
    {
        private int[][] months;
        public DeterminantTheDayOff(int year)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(@"http://web-scripts.ru/vyhodnye-i-prazdniki-" + year + @".html");

                months = doc.DocumentNode.Descendants("table")
                    .Where(x => x.Attributes.Count == 0)
                    .Where(x => x.ChildNodes.Count(y => y.Name != "#text") == 8)
                    .Where(x => x.Name != "#text")
                    .Select(x => x.Descendants()
                        .Where(y => y.HasAttributes)
                        .Skip(1)
                        .Where(y => y.GetAttributeValue("class", "false") == "p_h")
                        .Select(y => int.Parse(y.InnerText))
                        .ToArray())
                    .ToArray();
            }
            catch
            {
            }

        }

        public bool IsDayOff(int month, int day, int dayInWeek)
        {
            try
            {
                return months[month - 1].Contains(day) || dayInWeek == 5 || dayInWeek == 6;
            }
            catch (Exception)
            {
                return dayInWeek == 5 || dayInWeek == 6;
            }
        }
    }
}
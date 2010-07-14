using System.Drawing;
using System;
using BuildAnalyzer.Svg;

namespace BuildAnalyzer.Render
{
    class ScaleRender
    {
        public SvgDocument Document { get; private set; }
        public TimeSpan Finished { get; private set; }
        public int Scale { get; private set; }


        public ScaleRender(SvgDocument document, TimeSpan finished, int scale)
        {
            Document = document;
            Finished = finished;
            Scale = scale;
        }

        public void Render(Point point)
        {
            int finished = (int) Finished.TotalMilliseconds;

            // Draw scale
            Document.DrawLine(point, new Point(point.X + finished/Scale,point.Y), Color.Black, 1);

            for (int j = 0; j < finished/Scale; j += 10)
            {
                int tickHeight = 5;
                if (j % 100 == 0)
                {
                    tickHeight = 10;
                    Document.DrawText(new Point(point.X + j, point.Y + tickHeight + 15), "Arial", 10, FontStyle.Regular, Color.Black, ContentAlignment.MiddleCenter, j * Scale / 1000.0);
                }

                Document.DrawLine(new Point(point.X + j, point.Y), new Point(point.X + j, point.Y + tickHeight), Color.Black, 1);
            }
        }
    }
}

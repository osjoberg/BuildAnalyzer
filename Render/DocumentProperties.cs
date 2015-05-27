using System.Drawing;

namespace BuildAnalyzer.Render
{
    static class DocumentProperties
    {
        public static Point Rounding { get { return new Point(2, 2); } }
        public static int DocumentMargin { get { return 10; } }
        public static int DocumentMinimumWidth { get { return 750; } }
    }
}

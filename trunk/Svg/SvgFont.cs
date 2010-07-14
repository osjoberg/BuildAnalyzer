using System.Drawing;

namespace BuildAnalyzer.Svg
{
    class SvgFont
    {
        public SvgFont()
        {
            Alignment = ContentAlignment.MiddleLeft;
        }

        public string Font { get; set; }
        public int Size { get; set; }
        public Color Fill { get; set; }
        public ContentAlignment Alignment { get; set; }
        public FontStyle FontStyle { get; set; }
        public int RowHeight { get { return (int)(Size*1.4); } }
    }
}

using System.Collections.Generic;
using System.Xml.Linq;
using System.Drawing;

namespace BuildAnalyzer.Svg
{
    class SvgDocument
    {
        public Size Size { get; set; }


        private static readonly XNamespace @namespace = "http://www.w3.org/2000/svg";
        public readonly XElement root = new XElement(@namespace + "svg");
        public readonly XDocument document = new XDocument();

        public void DrawLine(Point p1, Point p2, Color color, int width)
        {
            root.Add(new XElement(@namespace + "line",
                new XAttribute("x1", p1.X),
                new XAttribute("y1", p1.Y),
                new XAttribute("x2", p2.X),
                new XAttribute("y2", p2.Y),
                new XAttribute("stroke", ColorTranslator.ToHtml(color)),
                new XAttribute("stroke-width", width)                
            ));
        }

        public void DrawRectangle(Point p, Size size, Point rounding, Color fill, Color line, int width)
        {
            root.Add(new XElement(@namespace + "rect",            
                new XAttribute("x", p.X),
                new XAttribute("y", p.Y),
                new XAttribute("width", size.Width),
                new XAttribute("height", size.Height),
                new XAttribute("rx", rounding.X),
                new XAttribute("ry", rounding.Y),
                new XAttribute("fill", ColorTranslator.ToHtml(fill)),
                new XAttribute("stroke", ColorTranslator.ToHtml(line)),
                new XAttribute("stroke-width", width)
            ));
        }

        public void DrawText(Point p, string font, int size, FontStyle fontStyle, Color fill, ContentAlignment alignment, object text)
        {
            Dictionary<ContentAlignment, XAttribute> alignmentTransform = new Dictionary<ContentAlignment, XAttribute>
            {
                { ContentAlignment.MiddleLeft, new XAttribute("text-anchor", "start")},
                { ContentAlignment.MiddleCenter, new XAttribute("text-anchor", "middle")},
                { ContentAlignment.MiddleRight, new XAttribute("text-anchor", "end")}
            };

            Dictionary<FontStyle, XAttribute> fontWeightTransform = new Dictionary<FontStyle, XAttribute>
            {
                { FontStyle.Regular, new XAttribute("font-weight", "normal")},
                { FontStyle.Bold, new XAttribute("font-weight", "bold")},
            };

            root.Add(new XElement(@namespace + "text",            
                new XAttribute("x", p.X ),
                new XAttribute("y", p.Y ),
                new XAttribute("fill", ColorTranslator.ToHtml(fill)),
                new XAttribute("stroke", ColorTranslator.ToHtml(fill)),
                new XAttribute("font-size", size),
                new XAttribute("font-family", font),
                new XAttribute("stroke-width", 0),
                alignmentTransform[alignment],
                fontWeightTransform[fontStyle],
                text
            ));
        }

        public void DrawText(Point p, SvgFont font, object text)
        {
            DrawText(p, font.Font, font.Size, font.FontStyle, font.Fill, font.Alignment, text);
        }

        public void Save(string fileName)
        {
            root.Add(new XAttribute("width", Size.Width), new XAttribute("height", Size.Height));
            document.Add(root);
            document.Save(fileName);
        }
    }
}
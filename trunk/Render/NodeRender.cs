using BuildAnalyzer.Svg;
using System.Drawing;
namespace BuildAnalyzer.Render
{
    class NodeRender
    {
        public SvgDocument Document { get; private set; }
        public int NodeId { get; private set; }

        public NodeRender(SvgDocument document, int nodeId)
        {
            Document = document;
            NodeId = nodeId;
        }

        public void Render(Point point, int height)
        {
            Document.DrawRectangle(point, new Size(60, height), DocumentProperties.Rounding, Color.LightSalmon, Color.Black, 1);
            Document.DrawText(new Point(point.X + 10, point.Y + 20), "Arial", 10, FontStyle.Regular, Color.Black, ContentAlignment.MiddleLeft, "Node" + NodeId);            
        }
    }
}

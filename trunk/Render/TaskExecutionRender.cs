using System.Collections.Generic;
using System.Drawing;
using BuildAnalyzer.Analyzer;
using BuildAnalyzer.Svg;

namespace BuildAnalyzer.Render
{
    class TaskExecutionRender
    {
        public IEnumerable<TaskExecution> TaskExecutions { get; private set; }
        public IDictionary<string, Color> ColorTable { get; private set; }
        public SvgDocument Document { get; private set; }
        public int Scale { get; private set; }
        public int Height { get; private set; }

        public TaskExecutionRender(SvgDocument document, IEnumerable<TaskExecution> taskExecutions, IDictionary<string, Color> colorTable, int height, int scale)
        {
            Document = document;
            TaskExecutions = taskExecutions;
            ColorTable = colorTable;
            Scale = scale;
            Height = height;
        }
        public void Render(Point point)
        {            
            Render(point, TaskExecutions, 0);
        }

        private void Render(Point point, IEnumerable<TaskExecution> taskExecutions, int depth)
        {
            foreach (TaskExecution taskExecution in taskExecutions)
            {
                Document.DrawRectangle(new Point(point.X + (int)taskExecution.Started.TotalMilliseconds/Scale, point.Y + depth * 10), new Size((int)taskExecution.Duration.TotalMilliseconds/Scale, Height - depth*20 ), DocumentProperties.Rounding, ColorTable[taskExecution.TaskName], Color.Black, 1);
                Render(point, taskExecution.Childs, depth + 1);
            }
        }
    }
}

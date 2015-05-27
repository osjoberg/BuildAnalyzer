using System;
using System.Collections.Generic;
using System.Drawing;
using BuildAnalyzer.Analyzer;
using BuildAnalyzer.Svg;

namespace BuildAnalyzer.Render
{
    class TaskSummaryRender
    {
        public IEnumerable<TaskSummary> Summary { get; private set; }
        public IDictionary<string, Color> ColorTable { get; private set; }
        public SvgDocument Document { get; private set; }

        private const int taskNameColumnPosition = 15;
        private const int taskCountColumnPosiition = taskNameColumnPosition + 300;
        private const int taskDurationColumnPosition = taskCountColumnPosiition + 100;
        private const int taskDurationExcludingChildrenColumnPosition = taskDurationColumnPosition + 100;
        private const int taskAverageDurationColumnPosition = taskDurationExcludingChildrenColumnPosition + 100;
        private const int taskAverageDurationExcludingChildrenColumnPosition = taskAverageDurationColumnPosition + 100;
        private const int rowHeight = 14;
        private const int headerRowHeight = 10;
        private const int alignmentAdjust = rowHeight / 2;
        private readonly SvgFont fontLeftAlign = new SvgFont { Font = "Arial", Size = 10, Fill = Color.Black, Alignment = ContentAlignment.MiddleLeft };
        private readonly SvgFont fontRightAlign = new SvgFont { Font = "Arial", Size = 10, Fill = Color.Black, Alignment = ContentAlignment.MiddleRight };
        private readonly SvgFont fontBoldLeftAlign = new SvgFont { Font = "Arial", Size = 10, FontStyle = FontStyle.Bold, Fill = Color.Black, Alignment = ContentAlignment.MiddleLeft };
        private readonly SvgFont fontBoldRightAlign = new SvgFont { Font = "Arial", Size = 10, FontStyle = FontStyle.Bold, Fill = Color.Black, Alignment = ContentAlignment.MiddleRight };

        public TaskSummaryRender(SvgDocument document, IEnumerable<TaskSummary> summary, IDictionary<string, Color> colorTable)
        {
            Document = document;
            Summary = summary;
            ColorTable = colorTable;
        }

        public void Render(Point p)
        {
            Document.DrawText(new Point(p.X + taskDurationColumnPosition, p.Y + alignmentAdjust), fontBoldRightAlign, "Total Duration");
            Document.DrawText(new Point(p.X + taskDurationExcludingChildrenColumnPosition, p.Y + alignmentAdjust), fontBoldRightAlign, "Total Duration");
            Document.DrawText(new Point(p.X + taskAverageDurationColumnPosition, p.Y + alignmentAdjust), fontBoldRightAlign, "Average Duration");
            Document.DrawText(new Point(p.X + taskAverageDurationExcludingChildrenColumnPosition, p.Y + alignmentAdjust), fontBoldRightAlign, "Avgerage Duration");

            p.Y += headerRowHeight;

            Document.DrawText(new Point(p.X, p.Y + alignmentAdjust), fontBoldLeftAlign, "Task");
            Document.DrawText(new Point(p.X + taskCountColumnPosiition, p.Y + alignmentAdjust), fontBoldRightAlign, "Executed");
            Document.DrawText(new Point(p.X + taskDurationColumnPosition, p.Y + alignmentAdjust), fontBoldRightAlign, "Including Children");
            Document.DrawText(new Point(p.X + taskDurationExcludingChildrenColumnPosition, p.Y + alignmentAdjust), fontBoldRightAlign, "Excluding Children");
            Document.DrawText(new Point(p.X + taskAverageDurationColumnPosition, p.Y + alignmentAdjust), fontBoldRightAlign, "Including Children");
            Document.DrawText(new Point(p.X + taskAverageDurationExcludingChildrenColumnPosition, p.Y + alignmentAdjust), fontBoldRightAlign, "Excluding Children");

            Document.DrawLine(new Point(p.X, p.Y + 10), new Point(p.X + taskAverageDurationExcludingChildrenColumnPosition, p.Y + 10), Color.Black, 1);

            foreach (TaskSummary summary in Summary)
            {
                p.Y += rowHeight;
                Document.DrawRectangle(new Point(p.X, p.Y), new Size(7, 7), DocumentProperties.Rounding, ColorTable[summary.TaskName], Color.Black, 1);
                Document.DrawText(new Point(p.X + taskNameColumnPosition, p.Y + alignmentAdjust), fontLeftAlign, summary.TaskName);
                Document.DrawText(new Point(p.X + taskCountColumnPosiition, p.Y + alignmentAdjust), fontRightAlign, summary.Count);
                Document.DrawText(new Point(p.X + taskDurationColumnPosition, p.Y + alignmentAdjust), fontRightAlign, new DateTime(summary.Duration.Ticks).ToString("HH:mm:ss.ffff"));
                Document.DrawText(new Point(p.X + taskDurationExcludingChildrenColumnPosition, p.Y + alignmentAdjust), fontRightAlign, new DateTime(summary.DurationExcludingChildren.Ticks).ToString("HH:mm:ss.ffff"));
                Document.DrawText(new Point(p.X + taskAverageDurationColumnPosition, p.Y + alignmentAdjust), fontRightAlign, new DateTime(summary.AverageDuration.Ticks).ToString("HH:mm:ss.ffff"));
                Document.DrawText(new Point(p.X + taskAverageDurationExcludingChildrenColumnPosition, p.Y + alignmentAdjust), fontRightAlign, new DateTime(summary.AverageDurationExcludingChildren.Ticks).ToString("HH:mm:ss.ffff"));
            }
        }
    }
}

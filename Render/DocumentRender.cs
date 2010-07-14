using System;
using System.Collections.Generic;
using System.Drawing;
using BuildAnalyzer.Analyzer;
using BuildAnalyzer.Logger;
using BuildAnalyzer.Svg;
using System.Reflection;

namespace BuildAnalyzer.Render
{
    class DocumentRender
    {
        public DocumentRender(IEnumerable<TaskSummary> taskSummary, IEnumerable<KeyValuePair<int, IEnumerable<TaskExecution>>> taskExecutions, IDictionary<string, Color> colorTable, TimeSpan finished, int maxDepthCount, Parameters parameters)
        {
            int scale = parameters.Scale.Value;

            SvgDocument svg = new SvgDocument();
            SvgFont h1 = new SvgFont { Font = "Arial", Size = 20, FontStyle = FontStyle.Bold };
            SvgFont h2 = new SvgFont { Font = "Arial", Size = 15, FontStyle = FontStyle.Bold };
            SvgFont normal = new SvgFont { Font = "Arial", Size = 10 };
            const int sectionSpacing = 40;

            int height = DocumentProperties.DocumentMargin + h1.Size;

            svg.DrawText(new Point(DocumentProperties.DocumentMargin, height), h1, "BuildAnalyzer " + Assembly.GetExecutingAssembly().GetName().Version + " report");
            height += h1.RowHeight;

            svg.DrawText(new Point(DocumentProperties.DocumentMargin, height), normal, "Report file: " + parameters.ReportFilename);
            height += normal.RowHeight;
            svg.DrawText(new Point(DocumentProperties.DocumentMargin, height), normal, "Report created: " + DateTime.Now);
            height += normal.RowHeight;
            svg.DrawText(new Point(DocumentProperties.DocumentMargin, height), normal, "Command line: " + Environment.CommandLine);
            height += normal.RowHeight;
            svg.DrawText(new Point(DocumentProperties.DocumentMargin, height), normal, "Execution time: " + new DateTime(finished.Ticks).ToString("HH:mm:ss.ffff"));
            height += normal.RowHeight + sectionSpacing;

            svg.DrawText(new Point(DocumentProperties.DocumentMargin, height), h2, "Task Execution Timeline");
            height += h2.RowHeight;

            int nodeHeight = maxDepthCount * 10 * 2;

            foreach (KeyValuePair<int, IEnumerable<TaskExecution>> taskExecution in taskExecutions)
            {
                new NodeRender(svg, taskExecution.Key).Render(new Point(10, height), nodeHeight);
                new TaskExecutionRender(svg, taskExecution.Value, colorTable, nodeHeight, scale).Render(new Point(80, height));
                height += nodeHeight + 10;
            }

            new ScaleRender(svg, finished, scale).Render(new Point(80, height));
            height += 30 + sectionSpacing;

            svg.DrawText(new Point(10, height), h2, "Task Execution Statistics");
            height += h2.RowHeight;

            new TaskSummaryRender(svg, taskSummary, colorTable).Render(new Point(10, height));
            height += (new List<TaskSummary>(taskSummary).Count + 1)*normal.RowHeight+DocumentProperties.DocumentMargin;
            svg.Size = new Size(80 + (int)finished.TotalMilliseconds / scale + DocumentProperties.DocumentMargin, height);
            svg.Save(parameters.ReportFilename);
        }
    }
}

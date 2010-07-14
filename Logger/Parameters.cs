using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BuildAnalyzer.Logger
{
    class Parameters
    {
        public string ReportFilename { get; private set; }
        public int? Scale { get; private set; }

        public void GenerateReportFilename(string projectFilename)
        {
            ReportFilename = Path.ChangeExtension(projectFilename, "svg");
        }

        public void AutomaticScale(TimeSpan finished)
        {
            Dictionary<int, int> scaleSelections = new Dictionary<int, int> 
            {
                {10000, 1},
                {100000, 10},
                {1000000, 100},
                {10000000, 1000}
            };

            Scale = (from scaleSelection in scaleSelections where finished.TotalMilliseconds <= scaleSelection.Key orderby scaleSelection.Value select scaleSelection.Value).First();
        }
    }
}

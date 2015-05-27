using System.Collections.Generic;
using BuildAnalyzer.Analyzer;
using BuildAnalyzer.Render;
using Microsoft.Build.Framework;
using System.Diagnostics;

namespace BuildAnalyzer.Logger
{
    public class TaskLogger : IForwardingLogger
    {
        private readonly Stopwatch stopwatch = Stopwatch.StartNew();
        private readonly Dictionary<TaskIdentifier, TaskInformation> statistics = new Dictionary<TaskIdentifier, TaskInformation>();
        private string projectFilename;

        public void Initialize(IEventSource eventSource)
        {
            eventSource.TaskStarted += EventSource_TaskStarted;
            eventSource.TaskFinished += EventSource_TaskFinished;
            eventSource.ProjectStarted += EventSource_ProjectStarted;
        }

        void EventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
            if (projectFilename == null)
                projectFilename = e.ProjectFile;
        }

        public void Initialize(IEventSource eventSource, int nodeCount)
        {
            Initialize(eventSource);
        }

        void EventSource_TaskStarted(object sender, TaskStartedEventArgs e)
        {
            statistics.Add(new TaskIdentifier(e.BuildEventContext.NodeId, e.BuildEventContext.TaskId), new TaskInformation(e.TaskName, stopwatch.Elapsed));
        }

        void EventSource_TaskFinished(object sender, TaskFinishedEventArgs e)
        {
            statistics[new TaskIdentifier(e.BuildEventContext.NodeId, e.BuildEventContext.TaskId)].Finish(stopwatch.Elapsed);
        }

        public void Shutdown()
        {
            TaskAnalyzer analyzer = new TaskAnalyzer(statistics);

            Parameters parameters = new Parameters();
            parameters.GenerateReportFilename(projectFilename);
            parameters.AutomaticScale(analyzer.GetLastTask().Finished);

            new DocumentRender(analyzer.GetTaskSummary(), analyzer.GetTaskExecutionHierarchy(), analyzer.GetColorTable(),
                               analyzer.GetLastTask().Finished, analyzer.GetMaxDepthCount(), parameters);
        }

        public LoggerVerbosity Verbosity { get; set; }
        public string Parameters { get; set; }
        public IEventRedirector BuildEventRedirector { get; set; }
        public int NodeId { get; set; }        
    }
}

using System;

namespace BuildAnalyzer.Analyzer
{
    class TaskStatistics
    {
        public int NodeId { get; private set; }
        public int ThreadId { get; private set; }

        public int TaskId { get; private set; }
        public string TaskName { get; private set; }
        
        public TimeSpan Started { get; private set; }
        public TimeSpan Finished { get; set; }
        public TimeSpan Duration { get { return Finished - Started; } }

        public TaskStatistics(int nodeId, int threadId, int taskId, string taskName, TimeSpan taskStarted, TimeSpan taskFinished)
        {
            NodeId = nodeId;
            ThreadId = threadId;
            TaskId = taskId;
            TaskName = taskName;
            Started = taskStarted;
            Finished = taskFinished;
        }
    }
}



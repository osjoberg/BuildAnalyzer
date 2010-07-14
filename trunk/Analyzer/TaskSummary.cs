using System;

namespace BuildAnalyzer.Analyzer
{
    class TaskSummary
    {
        public TaskSummary(string taskName, int count, TimeSpan duration, TimeSpan durationExcludingChildren)
        {
            TaskName = taskName;
            Count = count;
            Duration = duration;
            DurationExcludingChildren = durationExcludingChildren; 
        }

        public string TaskName { get; private set; }
        public int Count { get; private set; }
        public TimeSpan Duration { get; private set; }
        public TimeSpan DurationExcludingChildren { get; private set; }
        public TimeSpan AverageDuration { get { return new TimeSpan(Duration.Ticks / Count); } }
        public TimeSpan AverageDurationExcludingChildren { get { return new TimeSpan(DurationExcludingChildren.Ticks / Count); } }
    }
}

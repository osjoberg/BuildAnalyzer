using System;

namespace BuildAnalyzer.Logger
{
    class TaskInformation
    {
        public string TaskName { get; private set; }
        public TimeSpan Started { get; private set; }
        public TimeSpan Finished { get; protected set; }
        public TimeSpan Duration { get { return Finished - Started; } }

        public TaskInformation(string taskName, TimeSpan started)
        {
            TaskName = taskName;
            Started = started;
        }

        public void Finish(TimeSpan finished)
        {
            if (Finished != TimeSpan.Zero)
                throw new InvalidOperationException("Finish can only be called once.");

            Finished = finished;
        }
    }
}



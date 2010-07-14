using System.Collections.Generic;
using System.Linq;
using BuildAnalyzer.Logger;
using System;

namespace BuildAnalyzer.Analyzer
{
    class TaskExecution : TaskInformation
    {
        public List<TaskExecution> Childs { get; private set;}

        public TimeSpan DurationExcludingChildren
        {
            get { return Duration - new TimeSpan(Childs.Sum(child => child.DurationExcludingChildren.Ticks)); }        
        }

        public int LevelCount
        {
            get { return (Childs.Count > 0 ? Childs.Max(child => child.LevelCount) : 0) + 1; }
        }

        public TaskExecution(TaskInformation taskInformation) : base(taskInformation.TaskName, taskInformation.Started)
        {
            Finished = taskInformation.Finished;
            Childs = new List<TaskExecution>();
        }
    }
}



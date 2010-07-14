using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BuildAnalyzer.Logger;

namespace BuildAnalyzer.Analyzer
{
    class TaskAnalyzer
    {
        public IDictionary<TaskIdentifier, TaskInformation> Statistics { get; private set; }

        public TaskAnalyzer(IDictionary<TaskIdentifier, TaskInformation> statistics)
        {
            Statistics = statistics;
        }

        public IList<TaskSummary> GetTaskSummary()
        {
            var nodeTaskExecutions = from statistic in Statistics
                                     group statistic by statistic.Key.NodeId into grouping
                                     let nodeStatistics = from nodeStatistic in grouping
                                                          where nodeStatistic.Key.NodeId == grouping.Key
                                                          select nodeStatistic.Value
                                     select GetTaskExecutions(nodeStatistics);
            
            var query = from nodeTaskExecution in nodeTaskExecutions
                        from taskExecution in nodeTaskExecution
                        group taskExecution by taskExecution.TaskName into grouping
                        let duration = new TimeSpan(grouping.Sum(te => te.Duration.Ticks))
                        let durationExcludingChildren = new TimeSpan(grouping.Sum(te => te.DurationExcludingChildren.Ticks))
                        orderby duration descending
                        select new TaskSummary(grouping.Key, grouping.Count(), duration, durationExcludingChildren);

            return query.ToList();
        }

        public IDictionary<string, Color> GetColorTable()
        {
            int colorIndex = 0;
            var query = from statistic in Statistics.Values
                        group statistic by statistic.TaskName into grouping
                        select new KeyValuePair<string, Color>(grouping.Key, GetColorByIndex(colorIndex++));

            return query.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
        }

        public IDictionary<int, IEnumerable<TaskExecution>> GetTaskExecutionHierarchy()
        {
            var query = from statistic in Statistics
                        group statistic by statistic.Key.NodeId into grouping
                        let nodeStatistics = from nodeStatistic in grouping
                                             where nodeStatistic.Key.NodeId == grouping.Key
                                             select nodeStatistic.Value
                        select new KeyValuePair<int, IEnumerable<TaskExecution>>(grouping.Key, GetTaskExecutionHierarchy(nodeStatistics.ToList()));

            return query.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
        }

        public int GetMaxDepthCount()
        {
            var query = (from hierarchy in GetTaskExecutionHierarchy().Values
                         let nodeMaxDepth = hierarchy.Max(execution => execution.LevelCount)
                         select nodeMaxDepth).Max();

            return query;
        }

        private static IEnumerable<TaskRelationship> GetTaskExecutionRelationships(IEnumerable<TaskInformation> statistics)
        {
            // Construct task executions.
            var taskExecutions = (from statistic in statistics select new TaskExecution(statistic)).ToList();

            // Get relations.
            var relations = (from child in taskExecutions
                             let parent = (from parentTask in taskExecutions
                                           where parentTask.Started < child.Started && parentTask.Finished > child.Finished
                                           orderby parentTask.Duration
                                           select parentTask).FirstOrDefault()
                             select new TaskRelationship(parent, child)).ToList();

            // Assign relations.
            foreach (var relation in relations)
                if (relation.Parent != null)
                    relation.Parent.Childs.Add(relation.Child);

            return relations;
        }

        private static IEnumerable<TaskExecution> GetTaskExecutions(IEnumerable<TaskInformation> statistics)
        {
            var allTaskExecutions = from taskExecution in GetTaskExecutionRelationships(statistics)
                                    select taskExecution.Child;

            return allTaskExecutions.ToList();
        }

        private static IEnumerable<TaskExecution> GetTaskExecutionHierarchy(IEnumerable<TaskInformation> statistics)
        {
            // Return root task executions.
            var rootTaskExecutions = from taskExecution in GetTaskExecutionRelationships(statistics)
                                     where taskExecution.Parent == null
                                     select taskExecution.Child;

            return rootTaskExecutions.ToList();
        }

        public TaskInformation GetLastTask()
        {
            var query = from statistic in Statistics orderby statistic.Value.Finished descending select statistic.Value;
            return query.FirstOrDefault();
        }

        private static Color GetColorByIndex(int index)
        {
            const int startIndex = 39;
            return Color.FromKnownColor(((KnownColor[])Enum.GetValues(typeof(KnownColor)))[startIndex + index]);
        }
    }
}

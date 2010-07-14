namespace BuildAnalyzer.Analyzer
{
    class TaskRelationship
    {
        public TaskRelationship(TaskExecution parent, TaskExecution child)
        {
            Parent = parent;
            Child = child;
        }

        public TaskExecution Parent { get; private set; }
        public TaskExecution Child { get; private set; }
    }
}

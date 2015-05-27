namespace BuildAnalyzer.Logger
{
    struct TaskIdentifier
    {
        public int NodeId { get; private set; }
        public int TaskId { get; private set; }

        public TaskIdentifier(int nodeId, int taskId) : this()
        {
            NodeId = nodeId;
            TaskId = taskId;
        }
    }
}

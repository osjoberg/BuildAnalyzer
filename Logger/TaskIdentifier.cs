namespace BuildAnalyzer.Logger
{
    struct TaskIdentifier
    {
        public int NodeId { get; private set; }
        public int ThreadId { get; private set; }
        public int TaskId { get; private set; }

        public TaskIdentifier(int nodeId, int threadId, int taskId) : this()
        {
            NodeId = nodeId;
            ThreadId = threadId;
            TaskId = taskId;
        }
    }
}

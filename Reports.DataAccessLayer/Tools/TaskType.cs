namespace Reports.Tools
{
    public class TaskType
    {
        public TaskType(Type type)
        {
            Status = type;
        }

        public enum Type
        {
            Open,
            Active,
            Resolved
        }
        public Type Status { get; }
    }
}
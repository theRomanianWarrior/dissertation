namespace ScaleAgents
{
    public class Task
    {
        public Guid Id { get; set; }
        public string? AgentId { get; set; } // Agent assigned to the task
        public TaskType TypeId { get; set; } //type is the task’s type (flight, hotel or attractions)
        public string UserPreferenceId { get; set; } // what is exactly the user want?
        public string Status { get; set; } // available, not available

        public Task(Guid id,string? agent, TaskType type, string? userPreference, string stat)
        {
            Id = id;
            AgentId = agent;
            TypeId = type;
            UserPreferenceId = userPreference;
            Status = stat;
        }
    }
}

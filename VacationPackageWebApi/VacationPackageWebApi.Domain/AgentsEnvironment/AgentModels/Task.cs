using VacationPackageWebApi.Domain.Enums;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels
{
    public class Task
    {
        public Guid Id { get; set; }
        public string? AgentId { get; set; } // Agent assigned to the task
        public TaskType TypeId { get; set; } //type is the task’s type (flight, hotel or attractions)
        public Guid UserPreferenceId { get; set; } // what is exactly the user want?
        public bool Status { get; set; } // available, not available

        public Task(Guid id,string? agent, TaskType type, Guid userPreference, bool available = true)
        {
            Id = id;
            AgentId = agent;
            TypeId = type;
            UserPreferenceId = userPreference;
            Status = available;
        }
    }
}

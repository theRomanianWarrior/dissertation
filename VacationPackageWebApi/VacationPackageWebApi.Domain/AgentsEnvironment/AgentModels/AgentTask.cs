using VacationPackageWebApi.Domain.Enums;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels
{
    public record AgentTask
    {
        public Guid AgentId { get; set; } // Agent assigned to the task
        public TaskType TypeId { get; set; } //type is the task’s type (flight, hotel or attractions)
        public bool Status { get; set; } // available, not available
    }
}

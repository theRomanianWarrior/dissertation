using System;
namespace ScaleAgents
{
    
    public class Agent
    {
        public Guid Id { get; set; }
        public bool Status { get; set; } // available
        public int? TCurrent { get; set; } // the current chosen task
        public Dictionary<TaskType, double> ConfInd { get; set; } // confidence indexes of the agent to each type of task

        public Agent(Guid id, bool status, int? tCurrent, Dictionary<TaskType, double>? confInt)
        {
            this.Id = id;
            this.Status = status;
            this.TCurrent = tCurrent;
            this.ConfInd = confInt!;
        }
    }
}

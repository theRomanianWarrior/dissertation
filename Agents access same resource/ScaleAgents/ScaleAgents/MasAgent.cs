using ActressMas;

namespace ScaleAgents
{
    public class MasAgent : ActressMas.Agent
    {
        private static object _locker = new object();

        public override void Setup()
        {
            lock (_locker)
            {
                bool changed = false;

                List<ScaleAgents.Task> tasksToWork = Environment.Memory["tasksList"];
                if (!tasksToWork.Any()) return;

                var task = tasksToWork.FirstOrDefault(x => x.Status == "available");
                if (task != null)
                {
                    task.Status = "not available";
                    task.AgentId = this.Name;
                    changed = true;
                    Console.WriteLine($"Agent {Name} changed object {task.Id}");
                }

                if (!changed)
                {
                    Console.WriteLine("Agent " + Name + " found no free tasks.");
                }
            }
        }
        public override void Act(Message message)
        {

        }

        public override void ActDefault()
        {

        }
    }
}

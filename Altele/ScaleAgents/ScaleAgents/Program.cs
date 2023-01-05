using ActressMas;

namespace ScaleAgents
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // the system is creating task based on user preferences
            var task1 = new ScaleAgents.Task(Guid.NewGuid(),string.Empty, TaskType.Flight, "some flight data", "available");
            var task2 = new ScaleAgents.Task(Guid.NewGuid(),string.Empty, TaskType.RentACar, "some car data", "available");
            var task3 = new ScaleAgents.Task(Guid.NewGuid(),string.Empty, TaskType.Stay, "a random hotel", "available");
            var task4 = new ScaleAgents.Task(Guid.NewGuid(),string.Empty, TaskType.ToVisit, "some museums", "available");

            var listOfTasks = new List<ScaleAgents.Task>
            {
                task1, task2, task3, task4
            };
            var confidenceIndexForServices = new Dictionary<TaskType, double>
            {
                {TaskType.Flight, 0.25},
                {TaskType.RentACar, 0.33},
                {TaskType.Stay, 0.88},
                {TaskType.ToVisit, 0.22}

            };

            var confidenceIndexForServices1 = new Dictionary<TaskType, double>
            {
                {TaskType.Flight, 0},
                {TaskType.RentACar, 0.15},
                {TaskType.Stay, 0.22},
                {TaskType.ToVisit, 0.11}

            };

// I know that I already have a list of agents in the db
            var agent1 = new ScaleAgents.Agent(Guid.NewGuid(), true, null, confidenceIndexForServices);
            var agent2 = new ScaleAgents.Agent(Guid.NewGuid(), true, null, confidenceIndexForServices1);

            var env = new EnvironmentMas();
            env.Add(new MasAgent(), "agent");
            env.Add(new MasAgent(), "agent1");
            env.Add(new MasAgent(), "agent2");
            env.Add(new MasAgent(), "agent3");
            env.Add(new MasAgent(), "agent4");

            env.Memory["tasksList"] = listOfTasks;
            env.Start();
        }
    }
}
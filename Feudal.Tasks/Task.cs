using Feudal.Interfaces;

namespace Tasks
{
    class Task : ITask
    {
        public static int Count;

        public string Id { get; set; }

        public string Desc { get; set; }

        public int Percent { get; set; }

        public string WorkHoodId { get; set; }

        public string ClanId { get; set; }

        public Task()
        {
            Id = $"TASK_{Count++}";

            Desc = Id;
            Percent = 0;
        }
    }
}
using Feudal.Interfaces;

namespace Tasks
{
    class Task : ITask
    {
        public static Func<string, IWorkHood> FindWorkHood;
        public static Func<string, IClan> FindClan;

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

        public void OnNextTurn()
        {
            var workHood = FindWorkHood(WorkHoodId);
            var clan = FindClan(ClanId);

            switch (workHood.CurrentWorking)
            {
                case IProgressWorking progressWorking:
                    Percent = progressWorking.Percent;
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
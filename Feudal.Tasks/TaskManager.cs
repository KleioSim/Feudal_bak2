using Feudal.Interfaces;
using Feudal.MessageBuses.Interfaces;
using Feudal.Messages;
using System.Collections;

namespace Tasks
{
    public class TaskManager : IEnumerable<ITask>
    {
        private List<ITask> list = new List<ITask>();
        private IMessageBus messageBus;

        public TaskManager(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
            messageBus.Register(this);

            Task.FindClan = (Id) =>
            {
                return messageBus.PostMessage(new MESSAGE_FindClan() { Id = Id }).WaitAck<IClan>();
            };

            Task.FindWorkHood = (Id) =>
            {
                return messageBus.PostMessage(new MESSAGE_FindWorkHood() { Id = Id }).WaitAck<IWorkHood>();
            };
        }

        public IEnumerator<ITask> GetEnumerator()
        {
            return ((IEnumerable<ITask>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }

        [MessageProcess]
        void OnMESSAGE_StartTask(MESSAGE_StartTask message)
        {
            var task = new Task()
            {
                WorkHoodId = message.WorkHoodId,
                ClanId = message.LaborId,
            };

            list.Add(task);
        }

        [MessageProcess]
        void OnMESSAGE_CancelTask(MESSAGE_CancelTask message)
        {
            list.RemoveAll(x => x.Id == message.Id);
        }

        [MessageProcess]
        void OnMESSAGE_NextTurn(MESSAGE_NextTurn message)
        {
            foreach (var task in list)
            {
                task.OnNextTurn();
            }

            list.RemoveAll(x => x.Percent >= 100);
        }
    }
}
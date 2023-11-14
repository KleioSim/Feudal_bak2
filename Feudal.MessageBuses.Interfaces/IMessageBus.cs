namespace Feudal.MessageBuses.Interfaces;

public interface IMessageBus
{
    void Register(object obj);
    IPost PostMessage(Message message);

    public interface IPost
    {
        T WaitAck<T>() where T : class;
    }
}
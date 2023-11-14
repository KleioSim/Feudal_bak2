namespace Feudal.MessageBuses.Interfaces;

public interface IMessageBus
{
    void Register(object obj);
    IPost PostMessage(IMessage message);

    public interface IPost
    {
        T WaitAck<T>() where T : class;
    }
}

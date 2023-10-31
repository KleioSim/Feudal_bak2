using Feudal.Interfaces;

namespace Feudal.Sessions.Builders;

public static class SessionBuilder
{
    public static ISession Build()
    {
        return new Session();
    }
}
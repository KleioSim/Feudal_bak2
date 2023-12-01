namespace Feudal.Interfaces;

public interface ITask
{
    public string Id { get; }
    string Desc { get; }
    int Percent { get; set; }
    string WorkHoodId { get; }
    string ClanId { get; }

    void OnNextTurn();
}
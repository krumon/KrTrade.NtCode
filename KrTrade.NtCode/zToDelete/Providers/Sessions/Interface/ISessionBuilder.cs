namespace KrTrade.Nt.Console
{
    public interface ISessionsBuilder
    {
        ISessionProvider Build();
    }
}
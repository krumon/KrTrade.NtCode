namespace KrTrade.NtCode
{
    public interface ISessionsBuilder
    {
        ISessionProvider Build();
    }
}
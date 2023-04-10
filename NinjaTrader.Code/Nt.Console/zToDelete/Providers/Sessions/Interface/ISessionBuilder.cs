namespace ConsoleApp
{
    public interface ISessionsBuilder
    {
        ISessionProvider Build();
    }
}
namespace KrTrade.Nt.DI.Hosting.Internal
{
    public interface IConfigureContainerAdapter
    {
        void ConfigureContainer(HostBuilderContext hostContext, object containerBuilder);
    }
}

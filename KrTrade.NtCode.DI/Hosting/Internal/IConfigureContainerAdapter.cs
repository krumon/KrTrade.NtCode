namespace KrTrade.NtCode.Hosting.Internal
{
    public interface IConfigureContainerAdapter
    {
        void ConfigureContainer(HostBuilderContext hostContext, object containerBuilder);
    }
}

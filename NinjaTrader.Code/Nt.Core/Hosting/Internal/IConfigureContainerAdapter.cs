namespace Nt.Core.Hosting.Internal
{
    public interface IConfigureContainerAdapter
    {
        void ConfigureContainer(HostBuilderContext hostContext, object containerBuilder);
    }
}

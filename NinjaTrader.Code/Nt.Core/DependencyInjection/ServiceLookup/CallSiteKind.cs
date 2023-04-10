namespace Nt.Core.DependencyInjection.ServiceLookup
{
    internal enum CallSiteKind
    {
        Factory,
        Constructor,
        Constant,
        IEnumerable,
        ServiceProvider,
        Scope,
        Transient,
        Singleton
    }
}

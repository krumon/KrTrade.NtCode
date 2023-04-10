namespace Nt.Core.FileSystemGlobbing.Internal
{
    //
    // Resumen:
    //     This API supports infrastructure and is not intended to be used directly from
    //     your code. This API may change or be removed in future releases.
    public interface IPathSegment
    {
        bool CanProduceStem { get; }

        bool Match(string value);
    }
}

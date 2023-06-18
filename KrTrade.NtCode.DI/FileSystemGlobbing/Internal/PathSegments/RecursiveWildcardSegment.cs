namespace KrTrade.Nt.DI.FileSystemGlobbing.Internal.PathSegments
{
    public class RecursiveWildcardSegment : IPathSegment
    {
        public bool CanProduceStem { get { return true; } }

        public bool Match(string value)
        {
            return false;
        }
    }
}

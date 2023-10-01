using System;

namespace KrTrade.Nt.DI.FileSystemGlobbing.Internal.PathSegments
{
    public class ParentPathSegment : IPathSegment
    {
        private const string LiteralParent = "..";

        public bool CanProduceStem { get { return false; } }

        public bool Match(string value)
        {
            return string.Equals(LiteralParent, value, StringComparison.Ordinal);
        }
    }
}

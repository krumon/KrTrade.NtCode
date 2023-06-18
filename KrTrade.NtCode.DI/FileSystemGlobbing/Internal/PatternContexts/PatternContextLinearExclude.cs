using KrTrade.Nt.DI.FileSystemGlobbing.Abstractions;
using System;

namespace KrTrade.Nt.DI.FileSystemGlobbing.Internal.PatternContexts
{
    public class PatternContextLinearExclude : PatternContextLinear
    {
        public PatternContextLinearExclude(ILinearPattern pattern)
            : base(pattern)
        {
        }

        public override bool Test(DirectoryInfoBase directory)
        {
            if (IsStackEmpty())
            {
                throw new InvalidOperationException("CannotTestDirectory");
            }

            if (Frame.IsNotApplicable)
            {
                return false;
            }

            return IsLastSegment() && TestMatchingSegment(directory.Name);
        }
    }
}

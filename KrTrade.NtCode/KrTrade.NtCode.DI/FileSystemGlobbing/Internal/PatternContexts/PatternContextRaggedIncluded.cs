using KrTrade.NtCode.FileSystemGlobbing.Abstractions;
using KrTrade.NtCode.FileSystemGlobbing.Internal.PathSegments;
using System;

namespace KrTrade.NtCode.FileSystemGlobbing.Internal.PatternContexts
{
    public class PatternContextRaggedInclude : PatternContextRagged
    {
        public PatternContextRaggedInclude(IRaggedPattern pattern)
            : base(pattern)
        {
        }

        public override void Declare(Action<IPathSegment, bool> onDeclare)
        {
            if (IsStackEmpty())
            {
                throw new InvalidOperationException("CannotDeclarePathSegment");
            }

            if (Frame.IsNotApplicable)
            {
                return;
            }

            if (IsStartingGroup() && Frame.SegmentIndex < Frame.SegmentGroup.Count)
            {
                onDeclare(Frame.SegmentGroup[Frame.SegmentIndex], false);
            }
            else
            {
                onDeclare(WildcardPathSegment.MatchAll, false);
            }
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

            if (IsStartingGroup() && !TestMatchingSegment(directory.Name))
            {
                // deterministic not-included
                return false;
            }

            return true;
        }
    }
}

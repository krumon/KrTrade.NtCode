using Nt.Core.FileSystemGlobbing.Abstractions;
using System;

namespace Nt.Core.FileSystemGlobbing.Internal.PatternContexts
{
    public class PatternContextLinearInclude : PatternContextLinear
    {
        public PatternContextLinearInclude(ILinearPattern pattern)
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

            if (Frame.SegmentIndex < Pattern.Segments.Count)
            {
                onDeclare(Pattern.Segments[Frame.SegmentIndex], IsLastSegment());
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

            return !IsLastSegment() && TestMatchingSegment(directory.Name);
        }
    }
}

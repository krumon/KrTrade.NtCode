using System;
using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    public interface ISessions :
        IComparable,
        IComparable<ISessions>,
        IComparer,
        IComparer<ISessions>,
        IList<ISessions>,
        ICollection<ISessions>,
        IEnumerable<ISessions>,
        IEnumerable

    {
        //ISessionBuilder CreateDefaultTradingSessionBuilder();
        ITradingSessionCollection Sessions { get; }
        SessionCompareResult CompareSessionTo(ISessions otherSession);
    }
}
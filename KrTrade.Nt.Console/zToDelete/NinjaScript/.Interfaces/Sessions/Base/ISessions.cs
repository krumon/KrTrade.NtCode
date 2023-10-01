using KrTrade.Nt.DI.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Console
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
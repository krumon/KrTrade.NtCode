using System.Collections;
using System.Collections.Generic;

namespace KrTrade.NtCode
{
    public interface ITradingSessionCollection :
        IList<ISessions>,
        ICollection<ISessions>,
        IEnumerable<ISessions>,
        IEnumerable
    {
    }
}
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.NtCode.Data
{
    public interface ITradingSessionCollection :
        IList<ISessions>,
        ICollection<ISessions>,
        IEnumerable<ISessions>,
        IEnumerable
    {
    }
}
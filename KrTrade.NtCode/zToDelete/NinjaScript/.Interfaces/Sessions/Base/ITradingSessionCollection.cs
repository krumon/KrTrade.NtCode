using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Console
{
    public interface ITradingSessionCollection :
        IList<ISessions>,
        ICollection<ISessions>,
        IEnumerable<ISessions>,
        IEnumerable
    {
    }
}
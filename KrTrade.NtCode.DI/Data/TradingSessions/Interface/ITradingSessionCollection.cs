using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.DI.Data
{
    public interface ITradingSessionCollection :
        IList<ISessions>,
        ICollection<ISessions>,
        IEnumerable<ISessions>,
        IEnumerable
    {
    }
}
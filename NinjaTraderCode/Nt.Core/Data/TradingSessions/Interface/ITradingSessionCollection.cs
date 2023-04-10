using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    public interface ITradingSessionCollection :
        IList<ISessions>,
        ICollection<ISessions>,
        IEnumerable<ISessions>,
        IEnumerable
    {
    }
}
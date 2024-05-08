using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Series;

namespace KrTrade.Nt.Services.Series
{

    public class NinjascriptSeriesInfo : BaseSeriesInfo<SeriesType>
    {
        protected override string GetInputsKey() => string.Empty;
        protected override object[] GetParameters() => null;
    }
}

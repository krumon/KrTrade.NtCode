using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Elements;

namespace KrTrade.Nt.Services.Series
{

    public class NinjascriptSeriesInfo : SeriesInfo<SeriesType>
    {
        protected override string GetInputsKey() => string.Empty;
        protected override object[] GetParameters() => null;
    }
}

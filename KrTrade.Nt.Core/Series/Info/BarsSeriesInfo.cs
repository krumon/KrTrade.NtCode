namespace KrTrade.Nt.Core.Series
{

    public class BarsSeriesInfo : BaseSeriesInfo<BarsSeriesType>, IBarsSeriesInfo
    {
        protected override string GetInputsKey() => string.Empty;
        protected override object[] GetParameters() => null;
    }
}

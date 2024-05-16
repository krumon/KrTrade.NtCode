namespace KrTrade.Nt.Core.Series
{

    public class SeriesCollectionInfo : BaseSeriesInfo<SeriesCollectionType>, ISeriesCollectionInfo
    {
        //protected override string GetName() => Type.ToString();
        protected override string GetInputsKey() => string.Empty;
        protected override object[] GetParameters() => null;
    }
}

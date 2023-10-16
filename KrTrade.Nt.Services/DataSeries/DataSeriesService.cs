using KrTrade.Nt.Core;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services
{
    public class DataSeriesService : BaseNinjaScript
    {
        private DataSeriesInfo[] _seriesInfo;

        public DataSeriesService(NinjaScriptBase ninjascript) : base (ninjascript, NinjaScriptName.DataSeries, NinjaScriptType.Service)
        {
        }

        public override void DataLoaded()
        {
            _seriesInfo = new DataSeriesInfo[NinjaScript.BarsArray.Length];
            for (int i =0; i < NinjaScript.BarsArray.Length; i++) 
            {
                var serieInfo = new DataSeriesInfo();
                serieInfo.Load(NinjaScript.BarsArray[i]);
                _seriesInfo[i] = serieInfo;
            }
        }
    }
}

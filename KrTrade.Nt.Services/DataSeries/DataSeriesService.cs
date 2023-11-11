using KrTrade.Nt.Core;
using KrTrade.Nt.Services.Bars;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    //public class DataSeriesService : BaseNinjaScript
    //{
    //    private DataSeriesInfo[] _seriesInfo;

    //    public DataSeriesService(NinjaScriptBase ninjascript) : base (ninjascript, NinjaScriptName.DataSeries, NinjaScriptType.Service)
    //    {
    //    }

    //    public override void DataLoaded()
    //    {
    //        _seriesInfo = new DataSeriesInfo[NinjaScript.BarsArray.Length];
    //        for (int i =0; i < NinjaScript.BarsArray.Length; i++) 
    //        {
    //            var serieInfo = new DataSeriesInfo();
    //            serieInfo.Load(NinjaScript.BarsArray[i]);
    //            _seriesInfo[i] = serieInfo;
    //        }
    //    }
    //}

    public class DataSeriesService
    {

        #region Private members

        private int _index;
        private readonly BarsService _barsService;

        #endregion

        #region Public properties

        //public InstrumentCode

        ///// <summary>
        ///// Indicates if the last bar of the data series is closed.
        ///// </summary>
        //public bool IsLastBarClosed {  get; }

        #endregion

        #region Constructors

        public DataSeriesService(BarsService barsService) 
        { 
            _barsService = barsService ?? throw new ArgumentNullException(nameof(barsService));
        }

        #endregion

        #region Public methods


        #endregion

        #region Private methods


        #endregion
    }
}

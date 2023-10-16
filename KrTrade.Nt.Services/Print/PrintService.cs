using KrTrade.Nt.Core.Core;
using KrTrade.Nt.Core.Print;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Ninjascript service that print any object in the output window.
    /// </summary>
    public class PrintService : BasePrint
    {
        private readonly NinjaScriptBase _ninjascript;

        /// <summary>
        /// Create a new instance of <see cref="PrintService"/> with ninjascript injected.
        /// </summary>
        /// <param name="ninjascript">The ninjascript injected.</param>
        /// <exception cref="ArgumentNullException">The ninjascript cannot be null.</exception>
        public PrintService(NinjaScriptBase ninjascript) : base(ninjascript.Print, ninjascript.ClearOutputWindow)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
        }

        /// <inheritdoc/>
        public override State State => _ninjascript.State;
        /// <inheritdoc/>
        public override int BarsInProgress => _ninjascript.BarsInProgress;
        /// <inheritdoc/>
        public override int CurrentBar => _ninjascript.CurrentBars[BarsInProgress];
        /// <inheritdoc/>
        public override Instrument Instrument => _ninjascript.BarsArray[BarsInProgress].Instrument;
        /// <inheritdoc/>
        public override BarsPeriod BarsPeriod => _ninjascript.BarsPeriods[BarsInProgress];

        /// <summary>
        /// Prints in the ninjascript output window the OPEN price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void PrintOpen(int barsAgo = 0, int barsInProgress = 0)
        {
            string label = FormatLength == FormatLength.Long ? "Open" : "O";
            Write(label, _ninjascript.Opens[barsInProgress][barsAgo]);
        }
        /// <summary>
        /// Prints in the ninjascript output window the HIGH price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void PrintHigh(int barsAgo = 0, int barsInProgress = 0)
        {
            string label = FormatLength == FormatLength.Long ? "High" : "H";
            Write(label, _ninjascript.Highs[barsInProgress][barsAgo]);
        }
        /// <summary>
        /// Prints in the ninjascript output window the LOW price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void PrintLow(int barsAgo = 0, int barsInProgress = 0)
        {
            string label = FormatLength == FormatLength.Long ? "Low" : "L";
            Write(label, _ninjascript.Lows[barsInProgress][barsAgo]);
        }
        /// <summary>
        /// Prints in the ninjascript output window the CLOSE price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void PrintClose(int barsAgo = 0, int barsInProgress = 0)
        {
            string label = FormatLength == FormatLength.Long ? "Close" : "C";
            Write(label, _ninjascript.Closes[barsInProgress][barsAgo]);
        }
        /// <summary>
        /// Prints in the ninjascript output window the INPUT price of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void PrintInput(int barsAgo = 0, int barsInProgress = 0)
        {
            string label = FormatLength == FormatLength.Long ? "Input" : "I";
            Write(label, _ninjascript.Inputs[barsInProgress][barsAgo]);
        }
        /// <summary>
        /// Prints in the ninjascript output window the VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void PrintVolume(int barsAgo = 0, int barsInProgress = 0)
        {
            string label = FormatLength == FormatLength.Long ? "Volume" : "V";
            Write(label, _ninjascript.Volumes[barsInProgress][barsAgo].ToString(DoubleFormat.Volume));
        }
        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW AND CLOSE prices of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void PrintOHLC(int barsAgo = 0, int barsInProgress = 0)
        {
            string labels = FormatLength == FormatLength.Long ? "Open, High, Low, Close" : "O, H, L, C";
            Write(labels, 
                _ninjascript.Opens[barsInProgress][barsAgo], 
                _ninjascript.Highs[barsInProgress][barsAgo], 
                _ninjascript.Lows[barsInProgress][barsAgo],
                _ninjascript.Closes[barsInProgress][barsAgo]
                );
        }
        /// <summary>
        /// Prints in the ninjascript output window the OPEN, HIGH, LOW, CLOSE AND VOLUME of the specific <paramref name="barsAgo"/> bar and
        /// the specific <paramref name="barsInProgress"/> data series. The default values for <paramref name="barsAgo"/> and
        /// <paramref name="barsInProgress"/> parameters is 0.
        /// </summary>
        /// <param name="barsAgo">The specific bar.</param>
        /// <param name="barsInProgress">The specific data series.</param>
        public void PrintOHLCV(int barsAgo = 0, int barsInProgress = 0)
        {
            string labels = FormatLength == FormatLength.Long ? "Open, High, Low, Close, Volume" : "O, H, L, C, V";
            Write(labels,
                _ninjascript.Opens[barsInProgress][barsAgo],
                _ninjascript.Highs[barsInProgress][barsAgo],
                _ninjascript.Lows[barsInProgress][barsAgo],
                _ninjascript.Closes[barsInProgress][barsAgo],
                _ninjascript.Volumes[barsInProgress][barsAgo].ToString(DoubleFormat.Volume)
                );
        }
    }
}

using NinjaTrader.NinjaScript;

namespace KrTrade.NtCode
{

    /// <summary>
    /// Controller the stats of any session.
    /// </summary>
    public class SessionStats : BaseSession<SessionStats, SessionStatsConfiguration,SessionStatsBuilder>, ISessionStats
    {

        #region Private members

        private double rangeSum;
        private double rangeCount;

        #endregion

        #region Public properties

        public double RangeAvg => rangeSum / rangeCount;
        public double Range1Dv { get; set; }
        public double Range2Dv { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionStats"/> default instance.
        /// </summary>
        protected SessionStats() : base()
        {
        }

        #endregion

        #region Implementation methods

        /// <summary>
        /// Creates the <see cref="SessionHoursBuilder"/> to construct the <see cref="SessionHours"/> object.
        /// </summary>
        /// <returns>The <see cref="SessionHoursBuilder"/> to construct the <see cref="SessionHours"/> object.</returns>
        public ISessionStatsBuilder CreateSessionStatsBuilder() => CreateBuilder<SessionStats, SessionStatsBuilder>();

        #endregion

        #region Public methods

        public override void OnBarUpdate()
        {
            base.OnBarUpdate();
            rangeCount++;
            rangeSum += ninjascript.High[0] - ninjascript.Low[0];
        }

        #endregion


    }
}

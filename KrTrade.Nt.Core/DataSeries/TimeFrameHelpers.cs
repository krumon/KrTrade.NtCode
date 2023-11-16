using NinjaTrader.Data;
using System;

namespace KrTrade.Nt.Core.DataSeries
{
    /// <summary>
    /// Helper methods to converts from <see cref="TimeFrame"/> to other objects or converts any object to <see cref="TimeFrame"/>.
    /// </summary>
    public static class TimeFrameHelpers
    {
        /// <summary>
        /// Converts from <see cref="TimeFrame"/> to <see cref="BarsPeriodType"/>.
        /// </summary>
        /// <param name="timeFrame">The time frame.</param>
        /// <returns>The <see cref="BarsPeriodType"/> converted.</returns>
        /// <exception cref="Exception">The <see cref="TimeFrame"/> to convert, has not been implemented.</exception>
        public static BarsPeriodType ToPeriodType(this TimeFrame timeFrame)
        {
            switch (timeFrame)
            {
                case TimeFrame.t1:
                case TimeFrame.t150:
                    return BarsPeriodType.Tick;
                case TimeFrame.s15:
                    return BarsPeriodType.Second;
                case TimeFrame.m1:
                case TimeFrame.m5:
                case TimeFrame.m15:
                case TimeFrame.m30:
                case TimeFrame.h1:
                case TimeFrame.h4:
                    return BarsPeriodType.Minute;
                case TimeFrame.d1:
                    return BarsPeriodType.Day;
                case TimeFrame.w1:
                    return BarsPeriodType.Week;
                default: throw new Exception("The TimeFrame conversion has not yet been implemented.");

            }
        }

        /// <summary>
        /// Converts from <see cref="TimeFrame"/> to period value.
        /// </summary>
        /// <param name="timeFrame">The time frame.</param>
        /// <returns>The period value.</returns>
        /// <exception cref="Exception">The <see cref="TimeFrame"/> to convert, has not been implemented.</exception>
        public static int ToPeriodValue(this TimeFrame timeFrame)
        {
            switch (timeFrame)
            {
                case TimeFrame.t1:
                case TimeFrame.m1:
                case TimeFrame.h1:
                case TimeFrame.d1:
                case TimeFrame.w1:
                    return 1;
                case TimeFrame.h4:
                    return 4;
                case TimeFrame.m5:
                    return 5;
                case TimeFrame.s15:
                case TimeFrame.m15:
                    return 15;
                case TimeFrame.m30:
                    return 30;
                case TimeFrame.t150:
                    return 150;
                default: throw new Exception("The TimeFrame conversion has not yet been implemented.");

            }
        }

        /// <summary>
        /// Converts from <see cref="TimeFrame"/> to <see cref="BarsPeriod"/>.
        /// </summary>
        /// <param name="timeFrame">The time frame.</param>
        /// <returns><see cref="BarsPeriod"/> instance.</returns>
        /// <exception cref="Exception">The <see cref="TimeFrame"/> to convert, has not been implemented.</exception>
        public static BarsPeriod ToBarsPeriod(this TimeFrame timeFrame)
        {
            switch (timeFrame)
            {
                case TimeFrame.t1:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Tick,
                        Value = 1,
                    };
                case TimeFrame.m1:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Minute,
                        Value = 1,
                    };
                case TimeFrame.h1:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Minute,
                        Value = 60,
                    };
                case TimeFrame.d1:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Day,
                        Value = 1,
                    };
                case TimeFrame.w1:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Week,
                        Value = 1,
                    };
                case TimeFrame.h4:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Minute,
                        Value = 240,
                    };
                case TimeFrame.m5:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Minute,
                        Value = 5,
                    };
                case TimeFrame.s15:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Second,
                        Value = 15,
                    };
                case TimeFrame.m15:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Minute,
                        Value = 15,
                    };
                case TimeFrame.m30:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Minute,
                        Value = 30,
                    };
                case TimeFrame.t150:
                    return new BarsPeriod
                    {
                        BarsPeriodType = BarsPeriodType.Tick,
                        Value = 150,
                    };
                default: throw new Exception("The TimeFrame conversion has not yet been implemented.");

            }
        }

    }
}

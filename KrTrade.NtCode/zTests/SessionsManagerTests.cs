using NinjaTrader.NinjaScript;
using System;
using KrTrade.Nt.DI.Data;
using KrTrade.Nt.DI.Events;
using KrTrade.Nt.Core.Tests;

namespace KrTrade.Nt.Console.Tests
{
    internal class SessionsManagerTests : BaseConsoleTests
    {

        #region Private members


        #endregion

        #region Public Properties


        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="TradingSessionTests"/> default instance.
        /// </summary>
        public SessionsManagerTests()
        {

        }

        #endregion

        #region Public methods

        public override void Run()
        {
            InstanceTests();
            WaitAndClear();
        }

        #endregion

        #region Private methods

        private void InstanceTests()
        {
            // Create a custom instance.
            Title("Instance tests.");

            IManager sessionsManager = (IManager)SessionsManager.CreateDefaultBuilder()
            .AddSessionFilters((op) =>
            {
                op.Name = "Session Filters";
                op.Calculate = Calculate.OnEachTick;
                op.BarsRequiredToPlot = 50;
                op.AddDateFilters(year: 2020, isInitial: true);
                op.AddDateFilters(year: 2022, isInitial: false);
                op.AddDateFilters(new DateTime(2020, 6, 12), new DateTime(2022, 9, 20));
                op.AddOrder(EventType.Configure, 5);
                op.AddOrder(EventType.BarUpdate, 1);
            })
            .AddSessionHours((op) =>
            {
                op.Name = "Session Hours 1";
                op.AddOrder(EventType.Configure, 4);
                op.AddOrder(EventType.BarUpdate, 2);
            })
            .AddSessionHours((op) =>
            {
                op.Name = "Session Hours 2";
                op.AddOrder(EventType.Configure, 3);
                op.AllowManagerMultiUse = true;
                op.AddOrder(EventType.BarUpdate, 3);
            })
            .AddSessionStats((op) =>
            {
                op.Name = "Session Stats 1";
                op.AddOrder(EventType.Configure, 2);
                op.AddOrder(EventType.BarUpdate, 4);
            })
            .AddSessionStats((op) =>
            {
                op.Name = "Session Stats 2";
                op.AddOrder(EventType.Configure, 1);
                op.AddOrder(EventType.BarUpdate, 5);
                op.AllowManagerMultiUse = true;
            })
            .Configure((op) =>
            {
                op.Name = "My Sessions Manager";
            })
            .Build();

        }

        private void ToStringTests(SessionType type)
        {
        }

        #endregion

    }
}

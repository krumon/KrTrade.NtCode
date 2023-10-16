using System;
using System.Timers;

namespace KrTrade.Nt.Console.Tests
{
    internal class NtConnectionTests : BaseConsoleTests
    {

        #region Private members

        //private static Client client;
        private static Timer timer;

        #endregion

        #region Public Properties


        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="NtConnectionTests"/> default instance.
        /// </summary>
        public NtConnectionTests()
        {

        }

        #endregion

        #region Public methods

        public override void Run()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods


        private static void ATIConnection()
        {
            //client = new Client();
            //int connect = client.Connected(1);
            //client.SubscribeMarketData("MES");

            //Console.WriteLine(String.Format("{0} | Connected to NT8: {1}", DateTime.Now, connect.ToString()));
            //Console.WriteLine(String.Format("Cash Value: {0}", client.CashValue("")));
            //Console.WriteLine(String.Format("Buying Power: {0}", client.BuyingPower("")));

            timer = new System.Timers.Timer()
            {
                Interval = 1000
            };

            timer.Elapsed += ATI_Timer_Elapsed;
            timer.Enabled = true;

            System.Console.ReadKey();

            //client.UnsubscribeMarketData("MES");
            timer.Elapsed -= ATI_Timer_Elapsed;
            timer.Enabled = false;
            //timer.Terminated();
            timer.Close(); // Creo que no es necesario

            //int disconnect = client.TearDown();
            //Console.WriteLine(String.Format("{0} | Disconnected to NT8: {1}", DateTime.Now, disconnect.ToString()));
            System.Console.ReadKey();

        }

        private static void ATI_Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //if (client == null)
            //    return;

            //double lastPrice = client.MarketData("MES", 0);

            //Console.WriteLine(string.Format("{0} | Last: {1}", DateTime.Now, lastPrice));
        }


        #endregion

    }
}

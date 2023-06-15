using KrTrade.NtCode.Core.Data;
using System;
using System.Timers;

namespace KrTrade.NtCode.Connect
{
    public class NtSimulator
    {

        #region Events

        public event Action<TradingBar> BarUpdated = (currentBar) => { };

        #endregion

        #region Fields

        private int interval = 1000;
        private int speedFactor=0;
        
        protected Timer timer;
        protected TradingBar bar;

        #endregion

        #region Properties

        public bool ShowTimeInConsole { get; set; } = true;
        public bool ShowBarInConsole { get; set; } = true;
        public string ShowText { get; set; }

        public int Interval { get { return interval; } set { interval = value; if (timer != null) timer.Interval = interval; } } 
        public int SpeddFactor { get { return speedFactor; } set { speedFactor = value; } }
        public int Speed => Interval * SpeddFactor;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of <see cref="NtSimulator"/>.
        /// </summary>
        public NtSimulator()
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="NtSimulator"/> with specific parameters.
        /// </summary>
        /// <param name="interval">The interval of time in milliseconds to update the bar.</param>
        /// <param name="speedFactor">The factor to multiply the spedd of the simulation timer.</param>
        public NtSimulator(int interval, int speedFactor)
        {
            this.interval = interval;
            this.speedFactor = speedFactor;
        }

        #endregion

        #region Public methods

        public void Start()
        {
            timer = new Timer()
            {
                Interval = interval,
            };

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

        }

        public void Dispose()
        {
            timer.Elapsed -= Timer_Elapsed;
            timer.Enabled = false;
            timer.Dispose();
            timer.Close(); // Creo que no es necesario
        }

        #endregion

        #region Private methods

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Update the bar.
            BarUpdate();

            // Call to listeners
            OnBarUpdate(bar);

            // Raise the event
            BarUpdated?.Invoke(bar);


            Console.Clear();

            if (ShowTimeInConsole)
                PrintTimer();

            if (ShowBarInConsole)
                PrintBar();

            PrintToConsole(ShowText);

        }

        #endregion

        #region Virtual methods

        public virtual void OnBarUpdate(TradingBar currentBar)
        {
        }

        #endregion

        #region Helper methods

        private void BarUpdate()
        {
            if (bar == null)
                bar = new TradingBar(0, 0, 0, 0, 0, 0, DateTime.Now);
            else
            {
                bar.Idx++;
                bar.Time += TimeSpan.FromMilliseconds(Speed);
                bar.Open += 1;
            }
        }

        private void PrintTimer()
        {
            Console.WriteLine("\t\t\t\t\t-----------------------------------");
            Console.WriteLine("\t\t\t\t\t\t" + bar.Time.ToString());
            Console.WriteLine("\t\t\t\t\t-----------------------------------");
        }

        private void PrintBar()
        {
            Console.WriteLine(bar.ToString());
        }

        private void PrintToConsole(string text)
        {
            Console.WriteLine(text);
        }

        #endregion

    }
}

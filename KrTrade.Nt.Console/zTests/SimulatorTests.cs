using KrTrade.Nt.Core.Tests;
using System;
using System.Threading;

namespace KrTrade.Nt.Console.Tests
{
    internal class SimulatorTests : BaseConsoleTests
    {

        #region Private members

        //public NtSimulator simulator;
        //public TradingSessions session;

        public static DateTime currentDateTime = DateTime.MinValue;
        public static DateTime beginDateTime = DateTime.MinValue;
        public static DateTime endDateTime = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="SimulatorTests"/> default instance.
        /// </summary>
        public SimulatorTests()
        {
        }

        #endregion

        #region Public methods

        public override void Run()
        {
            // Inicializo el simulador con un intervalos de un segundo y un factor de tiempo de un minuto.
            //simulator = new NtSimulator
            //{
            //    Interval = 1000,            // Reloj virtual que lanza un evento cada segundo.
            //    SpeddFactor = 300,          // Cada segundo hago que pase un minute en el reloj de simulación.
            //    ShowTimeInConsole = true,   // Muestro en consola el tiempo.
            //    ShowBarInConsole = true,    // Muestro en pantalla los valores de la barra.
            //};

            //// Me subscribo al evento BarUpdated para ejecutar las pruebas.
            //simulator.BarUpdated += Simulator_BarUpdated;

            // Inicializo el indicador maestro de sesiones.
            //session = new NsTradingHours();

            // Comienzo la simulación.
            //simulator.Start();

            // Pauso el hilo principal para que de tiempo a ejecutar todas las incializaciones antes de comenzar
            // el método de simulación.
            Thread.Sleep(3000);

            // Comienzo el método de simulación.
            KrSessionHoursIteratorTest();

            // Paro el hilo principal de la aplicación para poder ver las pruebas en la consola.
            System.Console.ReadKey();

            // Libero la memoria.
            Dispose();

        }

        #endregion

        #region Private methods

        private void Simulator_BarUpdated(TradingBar bar)
        {
            currentDateTime = bar.Time;
            // TODO: Borrar. Es KrSession el que debe tener el iterador.
            //session.TradingSessionInfo.SessionHours[0].Iterator((s) =>
            //{
            //    //if (s.IsInSession(currentDateTime))
            //    //    simulator.ShowText = s.Description;
            //});
        }

        private void Dispose()
        {
            //simulator.BarUpdated -= Simulator_BarUpdated;
            //simulator.Dispose();
        }

        private static void KrSessionHoursIteratorTest()
        {
            //NsTradingHours session = new NsTradingHours();
            
            // TODO: Borrar. Es KrSession el que debe tener el iterador.
            //session.TradingSessionInfo.SessionHours[0].Iterator((s) =>
            //{
            //    //s.IsInSession(currentDateTime);
            //        //simulator.ShowText += s.Description + Environment.NewLine;
            //});
        }

        #endregion
    }
}

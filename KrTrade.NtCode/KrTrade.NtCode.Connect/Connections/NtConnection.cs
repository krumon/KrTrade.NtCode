using NinjaTrader.Client;
using Nt.Core.Data;
using System;
using System.Timers;

namespace Nt.Connect
{
    /// <summary>
    /// Use to connect to Ninjatrader platafform by the API.
    /// </summary>
    public class NtConnection
    {
        #region Events

        public event Action ConnectionUpdated = () => { };

        #endregion

        #region Private members

        private string masterInstrument;
        private int interval = 1000;
        private int isConnected = -1;
        private int isSubscribeToMarketData = -1;

        protected Timer timer;
        protected Client client;

        private string logString;

        #endregion

        #region Properties

        public int Interval { get { return interval; } set { interval = value; if (timer != null) timer.Interval = interval; } }
        public bool IsConnected => isConnected == 0;
        public bool IsDisconnect => !IsConnected;
        public bool IsSubscribeToMarketData => isSubscribeToMarketData == 0;
        public bool IsUnsubscribeToMarketData => !IsUnsubscribeToMarketData;

        #endregion

        #region Constructors

        public NtConnection(InstrumentCode instrumentKey)
        {
            masterInstrument = instrumentKey.ToString();
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

            Log(NtApiFunction.Connect);
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Elapsed -= Timer_Elapsed;
                timer.Enabled = false;
                timer.Dispose();
                timer.Close(); // Creo que no es necesario
            }

            if (IsConnected)
            {
                client.UnsubscribeMarketData("MES");
                int disconnect = client.TearDown();
                Log(NtApiFunction.TearDown);
            }
        }

        #endregion

        #region Handler methods

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            // Call to listeners
            OnConnectionUpdate();

            // Raise the event
            ConnectionUpdated?.Invoke();
        }

        #endregion

        #region Virtual methods

        public virtual void OnConnectionUpdate()
        {
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Devuelve un valor de cero si la DLL ha establecido una conexión con el servidor NinjaTrader (aplicación) y si la ATI 
        /// está actualmente habilitada o, -1 si está desconectada. Llamar a cualquier función en la DLL iniciará automáticamente una conexión al servidor. 
        /// El parámetro showMessage indica si se muestra un cuadro de mensaje en caso de que no se pueda establecer la conexión. Un valor de 1 = mostrar cuadro de mensaje, cualquier otro valor = no mostrar cuadro de mensaje.
        /// </summary>
        private void Connect()
        {
            client = new Client();
            isConnected = client.Connected(showMessage: 1);
        }

        /// <summary>
        /// Inicia un flujo de datos de mercado para el instrumento específico. Llame a la función MarketData() para recuperar 
        /// los precios. Asegúrese de llamar a la función UnSubscribeMarketData() para cerrar la secuencia de datos. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        private void SubscribeMarketData(string masterInstrument)
        {
            isSubscribeToMarketData = client.SubscribeMarketData(masterInstrument);
        }

        /// <summary>
        /// Detiene un flujo de datos de mercado para el instrumento específico. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        public void UnsubscribeMarketData (string masterInstrument)
        {
            if (client.UnsubscribeMarketData(masterInstrument) == 0)
                isSubscribeToMarketData = -1;
        }

        #endregion

        #region Log Methods

        private void Log(NtApiFunction function)
        {
            switch (function)
            {
                case (NtApiFunction.Connect):
                    logString = String.Format("{0} | Connected to NT8: {1}", DateTime.Now, IsConnected.ToString()); break;

                case (NtApiFunction.SubscribeMarketData):
                    logString = String.Format("{0} | Subscribe to NT8: {1}", DateTime.Now, IsConnected.ToString()); break;

                case (NtApiFunction.TearDown):
                    logString =  String.Format("{0} | Disconnected to NT8: {1}", DateTime.Now, IsDisconnect.ToString()); break;

                default:
                    throw new Exception("The API function doesn't exists.");
            }
        }

        #endregion

    }
}

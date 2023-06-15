namespace Nt.Connect
{

    /// <summary>
    /// Represents the Ninjatrader API functions.
    /// </summary>
    public enum NtApiFunction
    {

        #region Connection functions

        /// <summary>
        /// Devuelve un valor de cero si la DLL ha establecido una conexión con el servidor NinjaTrader (aplicación) y si la ATI 
        /// está actualmente habilitada o, -1 si está desconectada. Llamar a cualquier función en la DLL iniciará automáticamente una conexión al servidor. 
        /// El parámetro showMessage indica si se muestra un cuadro de mensaje en caso de que no se pueda establecer la conexión. Un valor de 1 = mostrar cuadro de mensaje, cualquier otro valor = no mostrar cuadro de mensaje.
        /// </summary>
        Connect,

        /// <summary>
        /// Función opcional para configurar el host y el número de puerto. De forma predeterminada, el host se establece 
        /// en "localhost" y el puerto se establece en 36973. El número de puerto predeterminado se puede configurar a través 
        /// de la pestaña General en Opciones . Si cambia estos valores predeterminados, esta función debe llamarse antes 
        /// que cualquier otra función. Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        SetUp,

        /// <summary>
        /// Inicia un flujo de datos de mercado para el instrumento específico. Llame a la función MarketData() para recuperar 
        /// los precios. Asegúrese de llamar a la función UnSubscribeMarketData() para cerrar la secuencia de datos. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        SubscribeMarketData,

        /// <summary>
        /// El parámetro de confirmación indica si aparecerá un mensaje de confirmación de pedido. Esto alterna la opción 
        /// global que se puede configurar manualmente en el Centro de Control de NinjaTrader seleccionando el menú 
        /// Herramientas y las opciones del elemento de menú , luego marcando la casilla de verificación 
        /// "Confirmar la colocación del pedido". Un valor de 1 establece esta opción en verdadero, cualquier otro 
        /// valor establece esta opción en falso.
        /// </summary>
        ConfirmOrders,

        /// <summary>
        /// Detiene un flujo de datos de mercado para el instrumento específico. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        UnSubscribeMarketData,

        /// <summary>
        /// Desconecta la DLL del servidor NinjaTrader. Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        TearDown,

        #endregion

        #region Account functions

        /// <summary>
        /// Obtiene el valor en efectivo de la cuenta especificada. * No todas las tecnologías de corretaje soportan este valor.
        /// </summary>
        CashValue,

        /// <summary>
        /// Obtiene la ganancia y pérdida realizada de una cuenta.
        /// </summary>
        RealizedPnL,

        /// <summary>
        /// Obtiene el poder de compra para la cuenta especificada. * No todas las tecnologías de corretaje soportan este valor.
        /// </summary>
        BuyingPower,

        /// <summary>
        /// Obtiene una cadena de ID de pedido de todos los pedidos de una cuenta separados por '|'. 
        /// *Si un ID de pedido definido por el usuario no se proporcionó originalmente, se utiliza 
        /// el valor de ID de token interno, ya que se garantiza que es único.
        /// </summary>
        Orders,

        /// <summary>
        /// Obtiene una serie de ID de estrategia de todas las Estrategias ATM de una cuenta separada por '|'. 
        /// Se pueden devolver valores de ID duplicados si las estrategias se iniciaron fuera de la ATI.
        /// </summary>
        Strategies,

        /// <summary>
        /// Obtiene el precio de entrada promedio para la combinación de instrumento / cuenta especificada.
        /// </summary>
        AvgEntryPrice,

        #endregion

        #region Order functions

        /// <summary>
        /// Obtiene el precio de entrada promedio para el ID de orden especificado.
        /// </summary>
        AvgFillPrice,

        /// <summary>
        /// Obtiene un nuevo valor de order ID único.
        /// </summary>
        NewOrderId,

        /// <summary>
        /// Obtiene el estado del pedido (ver definiciones) para el ID de pedido. 
        /// Devuelve una cadena vacía si el valor de ID de pedido proporcionado no devuelve un pedido.
        /// </summary>
        OrderStatus,

        /// <summary>
        /// Obtiene la posición de mercado para una combinación de instrumento / cuenta. 
        /// Devuelve 0 para plano, valor negativo para corto valor positivo para largo.
        /// </summary>
        MarketPosition,

        /// <summary>
        /// Obtiene el número de contratos / acciones completadas para orderId.
        /// </summary>
        Filled,


        #endregion

        #region Strategy orders

        /// <summary>
        /// Obtiene la posición para una estrategia. Devuelve 0 para plano, valor negativo para corto y valor positivo para largo.
        /// </summary>
        StrategyPosition,

        /// <summary>
        /// Obtiene una cadena de ID de orden de todas las órdenes de Profit Target de una estrategia de cajero automático 
        /// separadas por '|'. 
        /// Se utiliza el valor de ID de token interno ya que se garantiza que es único.
        /// </summary>
        TargetOrders,

        /// <summary>
        /// Obtiene una serie de ID de orden de todas las órdenes de Stop Loss de una estrategia de cajero automático 
        /// separadas por '|'. 
        /// Se utiliza el valor de ID de token interno ya que se garantiza que es único.
        /// </summary>
        StopOrders,

        #endregion

        #region Market data functions

        /// <summary>
        /// Establece el último precio y tamaño para el instrumento especificado. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        Last,

        /// <summary>
        /// Establece el precio y el tamaño del pedido para el instrumento especificado. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        Ask,

        /// <summary>
        /// Establece el precio y el tamaño de la oferta para el instrumento especificado. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error.
        /// </summary>
        Bid,

        /// <summary>
        /// Establece el último precio y tamaño para el instrumento especificado para usar al sincronizar 
        /// la reproducción de NinjaTrader con una reproducción externa de la aplicación. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error. 
        /// El formato de los parámetros de marca de tiempo es el siguiente "yyyyMMddHHmmss".
        /// </summary>
        LastPlayback,

        /// <summary>
        /// Establece el precio y el tamaño de pedido para el instrumento especificado para usar al sincronizar 
        /// la reproducción de NinjaTrader con una reproducción de aplicación externa. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error. 
        /// El formato de los parámetros de marca de tiempo es el siguiente "yyyyMMddHHmmss".
        /// </summary>
        AskPlayback,

        /// <summary>
        /// Establece el precio y el tamaño de la oferta para el instrumento especificado para su uso al sincronizar 
        /// la reproducción de NinjaTrader con una reproducción externa de la aplicación. 
        /// Un valor de retorno de 0 indica éxito y -1 indica un error. 
        /// El formato de los parámetros de marca de tiempo es el siguiente "yyyyMMddHHmmss".
        /// </summary>
        BidPlayback,

        /// <summary>
        /// Obtiene el precio más reciente para el instrumento y el tipo de datos especificados. 0 = last, 1 = bid, 2 = ask. 
        /// Primero debe llamar a la función SubscribeMarketData() antes de llamar a esta función.
        /// </summary>
        MarketData,

        #endregion

    }
}


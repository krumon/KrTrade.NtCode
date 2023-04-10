using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using Nt.Core.Events;

namespace ConsoleApp
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class SessionsManager : BaseManager<SessionsManager, SessionsManagerConfiguration,SessionsManagerBuilder>, ISessionsManager
    {

        #region Private members

        /// <summary>
        /// Gets the <see cref="SessionsIterator"/> funcionality.
        /// </summary>
        private SessionsIterator sessionsIterator;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionsManager"/> default instance.
        /// </summary>
        protected SessionsManager() : base()
        {
        }

        #endregion

        #region StateChanged methods

        /// <summary>
        /// Loaded <see cref="SessionsManager"/> in "OnStateChanged" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call the parent method to load.
            base.Load(ninjascript, bars);

            // If we need the session iterator...create him.
            // TODO: Make Sure we need the session iterator object.
            if (Get<SessionsIterator>() == null)
                sessionsIterator =
                    (SessionsIterator)CreateManagerBuilderInstance()
                    .Add<SessionsIterator, SessionsIteratorConfiguration, SessionsIteratorBuilder>(op => op.AddOrder(EventType.Configure, 0)).Build();

            // TODO: Configure the order.
            ExecuteHandlerMethod(EventType.DataLoaded);

            // Aggregate the delegates
            ((ISessionsIterator)Get<SessionsIterator>()).SessionChanged += OnSessionChanged;

        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Terminated()
        {
            // Disaggregate the delegates.
            sessionsIterator.SessionChanged -= OnSessionChanged;

            // Terminated the elements
            // TODO: Configure the order.
            ExecuteHandlerMethod(EventType.Terminated);

            //if (HasSessionFilters)
            //    sessionFilters.Terminated();
            //if (HasSessionHours)
            //    sessionHoursList.Terminated();
            //if (HasSessionStats)
            //    sessionStats.Terminated();

        }

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            // First...update the session iterator

            // TODO: Configure the order.
            ExecuteHandlerMethod(EventType.BarUpdate);

            //// Update the filters
            //if (HasSessionFilters)
            //    sessionFilters.OnBarUpdate();

            //// Opcion 1
            //if (HasSessionFilters && sessionFilters.OnBarUpdateAndCheckFilters())
            //{
            //    // Enter the code
            //}

            //// Opcion 2            
            //if (sessionFilters.CanEntry())
            //{
            //    // Enter the code
            //}
            
            // TODO: No funcionan los filtros. Arreglarlo!!!!!
            // Entry to the method if the filters are ok.
            //if (HasSessionFilters && !(sessionFilters.CanEntry()))
            //    return;

            ExecuteInBarUpdateMethod(BarUpdateAction);
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            // TODO: Configure the order.
            ExecuteHandlerMethod(EventType.MarketData);

            ExecuteInMarketDataMethod(MarketDataAction);
        }

        /// <summary>
        /// Changed any object or property when the session changed.
        /// </summary>
        /// <param name="e"></param>
        public void OnSessionChanged(SessionUpdateArgs e)
        {
            ExecuteHandlerMethod(EventType.SessionChanged, e);

            //// Update Holiday filters
            //if (HasSessionFilters)
            //    sessionFilters.OnSessionChanged(e);

            //// Update the session hours list.
            //if (HasSessionHours)
            //    sessionHoursList.OnSessionChanged(e);

            //// Execute delegate method.
            //ExecuteInSessionHoursChangedMethod(SessionHoursChangedAction);
        }

        #endregion

        #region Configure methods

        /// <summary>
        /// Creates the <see cref="SessionsManagerBuilder"/> to construct the <see cref="SessionsManager"/> object.
        /// </summary>
        /// <returns>The <see cref="SessionsManagerBuilder"/> to construct the <see cref="SessionsManager"/> object.</returns>
        public ISessionsManagerBuilder CreateSessionsManagerBuilder() => CreateBuilder<SessionsManager, SessionsManagerBuilder>();

        ///// <summary>
        ///// Add <see cref="SessionsManagerOptions"/> to <see cref="SessionsManager"/> configure.
        ///// </summary>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSession<TScript,TOptions,TBuilder>(Action<TOptions> options = null)
        //    where TScript : BaseManager<TScript,TOptions,TBuilder>, IManager
        //    where TOptions : BaseManagerOptions<TOptions>, IManagerOptions
        //    where TBuilder : BaseManagerBuilder<TScript,TOptions,TBuilder>, IManagerBuilder
        //{
        //    TScript script;

        //    if (!sessions.ContainsKey(typeof(TScript)))
        //        sessions.Add(typeof(TScript), Activator.CreateInstance<TScript>());
            
        //    script = (TScript)sessions[typeof(TScript)];

        //    script.Configure((TScript)options);

        //    return this;
        //}

        //public SessionsManager ConfigureSession<TOptions>(Action<TOptions> options = null)
        //    where TOptions : SessionsManagerOptions, new()
        //{
        //    if (options is Action<SessionHoursListOptions> shOptions)
        //    {
        //        // Make sure session filters is not null.
        //        if (!HasSessionHours)
        //            sessionHoursList = new SessionHoursList();

        //        // Configure....
        //        sessionHoursList.Configure(shOptions);

        //    }
        //    //else if (options is Action<SessionFiltersOptions> fOptions)
        //    //{
        //    //    // Make sure session filters is not null.
        //    //    if (!HasSessionFilters)
        //    //        sessionFilters = new SessionFilters();

        //    //    // Configure....
        //    //    sessionFilters.Configure(fOptions);

        //    //}

        //    return this;
        //}

        /// <summary>
        /// Add filters funcionality to session manager.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSession<TOptions>(TOptions options = null)
            where TOptions : SessionsManagerConfiguration, new()

        {
            //if (options is SessionHoursListOptions shOptions)
            //{
            //    // Make sure session filters is not null.
            //    if (!HasSessionHours)
            //        sessionHoursList = new SessionHoursList();

            //    // Configure....
            //    sessionHoursList.SetOptions(shOptions);

            //}
            //else if (options is SessionFiltersOptions fOptions)
            //{
            //    // Make sure session filters is not null.
            //    if (!HasSessionFilters)
            //        sessionFilters = new SessionFilters();

            //    // Configure....
            //    sessionFilters.SetOptions(fOptions);

            //}

            return this;
        }

        #endregion

        #region Extension methods
        
        

        /// <summary>
        /// Create <see cref="SessionsManagerBuilder"/> to build <see cref="SessionsManager"/> object.
        /// </summary>
        /// <returns></returns>
        //public static SessionsManagerBuilder CreateSessionManagerBuilder()
        //{
        //    return new SessionsManagerBuilder();
        //}

        #endregion

        #region ToString methods

        //public override string ToString()
        //{
        //    if (HasSessionHours)
        //        return sessionHoursList.ToString();
        //    else 
        //        return $"{nameof(SessionsManager)} is empty";
        //}

        #endregion

    }
}

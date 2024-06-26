using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Reflection;
using KrTrade.Nt.Core.Sessions;
using KrTrade.Nt.Core.Exceptions;
using KrTrade.Nt.Core.Data;

namespace KrTrade.Nt.Console
{

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    public abstract class BaseNinjascript : BaseElement
    {

        #region Protected members

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        protected NinjaScriptBase ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        protected Bars bars;

        #endregion

        #region Public properties

        /// <summary>
        /// Indicates if the session is configured.
        /// </summary>
        public bool IsLoaded { get; protected set; }

        #endregion

        #region State changed methods

        /// <summary>
        /// Method to set default properties in the script in "OnStateChanged.Configure" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        protected virtual void SetDefault(NinjaScriptBase ninjascript)
        {
        }

        /// <summary>
        /// AddValues the <see cref="BaseNinjascript"/>.
        /// </summary>
        /// <param name="ninjascript">The parent ninjascript.</param>
        /// <param name="bars">The chart bars object.</param>
        public virtual void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            try
            {
                // Try load the ninjascript object
                IsLoaded = OnTryLoad(ninjascript, bars);
            }
            catch (LoadException e)
            {
                // TODO: ILogger implementation to register errors.
                System.Console.WriteLine("Unhandler Exception on BaseNinjascript.Load method.");
                System.Console.WriteLine(e);
                System.Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Free the memory of the script.
        /// </summary>
        public virtual void Terminated() 
        { 
            Dispose();
        }

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public virtual void OnBarUpdate()
        {
            try
            {
                if (!IsLoaded)
                    throw new Exception(); //throw new OnBarUpdateException(ExceptionMessages.BarUpdateLoadException);
            }
            catch 
            {
                // TODO: ILogger implementation to register errors.
                System.Console.WriteLine("Unhandler Exception on OnBarUpdate.Load method.");
            }
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public virtual void OnMarketData()
        {
            // Make sure the ninjascript is loaded
            try
            {
                if (!IsLoaded)
                    throw new Exception(); //throw new OnBarUpdateException(ExceptionMessages.LoadException);
            }
            catch(OnBarUpdateException e) 
            {
                // TODO: ILogger implementation to register errors.
                System.Console.WriteLine(e);
                System.Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to try load the ninjascript object. If the object is loaded
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="bars"></param>
        public virtual void TryLoad(NinjaScriptBase ninjascript, Bars bars)
        {
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to make sure if the Load method parameters is not null.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="bars"></param>
        /// <returns>True if the method doesn't throw an exception.</returns>
        /// <exception cref="LoadException">If any Load method parameter is null, throw an exception.</exception>
        private bool OnTryLoad(NinjaScriptBase ninjascript, Bars bars)
        {
            try
            {
                // Sets ninjascript value.
                this.ninjascript = ninjascript ?? throw new Exception(); //throw new LoadException(ExceptionMessages.LoadParameterException, new ArgumentNullException(), typeof(NinjaScriptBase).ToString());
                // Sets bars value.
                this.bars = bars ?? throw new Exception(); //throw new LoadException(ExceptionMessages.LoadParameterException, new ArgumentNullException(), typeof(Bars).ToString());

                // Call to parent
                TryLoad(ninjascript, bars);

                return true;
            }
            catch
            {
                // TODO: Delete. Borrar throw y habilitar return. 
                throw;
                //return false;
            }
        }

        #endregion

        #region Action delegates

        /// <summary>
        /// Delegate to execute in "OnBarUpdate" method.
        /// </summary>
        public Action BarUpdateAction;

        /// <summary>
        /// Delegate to execute in "OnMarketData" method.
        /// </summary>
        public Action MarketDataAction;

        #endregion

        #region Delegate methods

        /// <summary>
        /// Execute the delegate in the OnBarUpdate method
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        protected void ExecuteInBarUpdateMethod(Action action)
        {
            action?.Invoke();
        }

        /// <summary>
        /// Execute the delegate in the OnBarUpdate method
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        protected void ExecuteInMarketDataMethod(Action action)
        {
            action?.Invoke();
        }

        #endregion

    }


    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript object.</typeparam>
    /// <typeparam name="TConfiguration">The ninjascript options object.</typeparam>
    /// <typeparam name="TBuilder">The ninjascript bulder object.</typeparam>
    public abstract class BaseNinjascript<TScript, TConfiguration,TBuilder> : BaseNinjascript, INinjascript
        where TScript : BaseNinjascript<TScript,TConfiguration,TBuilder>, INinjascript
        where TConfiguration : BaseConfiguration<TConfiguration>, IConfiguration
        where TBuilder : BaseBuilder<TScript,TConfiguration,TBuilder>, IBuilder
    {

        #region Private members

        /// <summary>
        /// The script configure.
        /// </summary>
        protected TConfiguration configuration;

        #endregion

        #region Public properties

        /// <summary>
        /// Indicates if the session is configured.
        /// </summary>
        public bool IsConfigured => configuration != null;

        /// <summary>
        /// Inidicates if allow more than one element in the manager collection.
        /// </summary>
        public bool AllowManagerMultiUse => configuration.AllowManagerMultiUse;

        #endregion

        #region Configure methods

        /// <summary>
        /// Method to set default properties in the script in "OnStateChanged.Configure" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        protected override void SetDefault(NinjaScriptBase ninjascript)
        {
            try
            {
                if (configuration == null)
                    throw new Exception(); //throw new SetDefaultException(ExceptionMessages.SetDefaultConfigureException, new NullReferenceException(), typeof(TConfiguration).ToString());

                if (ninjascript == null)
                    throw new Exception(); //throw new SetDefaultException(ExceptionMessages.SetDefaultNinjaScriptException, new NullReferenceException(), typeof(TConfiguration).ToString());

                // Copy the new options
                ninjascript.Name = configuration.Name;
                ninjascript.Calculate = configuration.Calculate;
                ninjascript.BarsRequiredToPlot = configuration.BarsRequiredToPlot;
            }
            catch(SetDefaultException e)
            {
                // TODO: Implementar ILogger para registrar los errores.
                System.Console.WriteLine(e.Message);
                // TODO: Controlar la excepción.
                throw;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates ninjascript builder to construct the script.
        /// </summary>
        /// <typeparam name="Script">The ninhascript to construct.</typeparam>
        /// <typeparam name="Builder">The ninjascript builder.</typeparam>
        /// <returns>The ninjascript builder.</returns>
        /// <exception cref="Exception">Any type is wrong.</exception>
        public virtual Builder CreateBuilder<Script,Builder>()
            where Script : INinjascript
            where Builder : IBuilder
        {
            // Make sure the types are correct.
            try
            {
                if (typeof(Script).IsAssignableFrom(typeof(TScript)) && typeof(Builder).IsAssignableFrom(typeof(TBuilder)))
                    return (Builder)CreateNinjascriptBuilderInstance((TScript)this);

                throw new Exception(); //throw new CreateBuilderException(ExceptionMessages.CreateBuilderAssignableException, typeof(Script), typeof(TScript), typeof(Builder), typeof(TBuilder));

            }
            catch
            {
                // TODO: ILogger implementation to register errors.
                throw;
            }

        }

        /// <summary>
        /// Gets an order for an event driven method.
        /// </summary>
        /// <param name="eventType">The event type.</param>
        /// <returns>The ninjascript order in the event driven method.</returns>
        public int GetOrder(EventType eventType) =>
            configuration.GetOrder(eventType);

        /// <summary>
        /// Execute the ninjascript handler methods.
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="e"></param>
        public virtual void ExecuteHandlerMethod(EventType eventType, SessionUpdateArgs e = null)
        {
            switch (eventType)
            {
                case EventType.SetDefaults:
                    SetDefault(null);
                    break;
                case EventType.DataLoaded:
                    Load(ninjascript, bars);
                    break;
                case EventType.BarUpdate:
                    OnBarUpdate();
                    break;
                case EventType.MarketData:
                    OnMarketData();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Returns the type of the script.
        /// </summary>
        /// <returns></returns>
        public Type GetScriptType()
        {
            return typeof(TScript);
        }

        /// <summary>
        /// Returns the type of the options.
        /// </summary>
        /// <returns></returns>
        public Type GetConfigurationType()
        {
            return typeof(TConfiguration);
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Creates a new <see cref="IBuilder"/> instance.
        /// </summary>
        /// <returns>The constructor can not be invoke.</returns>
        protected static IBuilder CreateNinjascriptBuilderInstance(TScript script)
        {
            try
            {
                ConstructorInfo construct = typeof(TBuilder).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(TScript) }, null);
                if (construct != null)
                {
                    try
                    {
                        return (TBuilder)construct.Invoke(new object[] { script });
                    }
                    catch(Exception e)
                    {
                        // TODO: ILogger implementation to register errors.
                        throw new Exception(); //throw new Exception(); throw new CreateBuilderException(ExceptionMessages.CreateBuilderCtorInvokeException,e);
                    }
                }
                else
                    // TODO: ILogger implementation to register errors.
                    throw new Exception(); //throw new CreateBuilderException(ExceptionMessages.CreateBuilderGetConstructorException, new NullReferenceException());
            }
            catch
            {
                // TODO: ILogger implementation to register errors.
                throw;
            }
        }

        /// <summary>
        /// Creates a new <see cref="INinjascript"/> instance.
        /// </summary>
        /// <returns></returns>
        protected static TScript CreateNinjascriptInstance()
        {
            ConstructorInfo construct = typeof(TScript).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (construct != null)
                return (TScript)construct.Invoke(new object[] { });
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// Creates a new <see cref="INinjascript"/> instance.
        /// </summary>
        /// <returns></returns>
        protected static T CreateNinjascriptInstance<T>()
            where T : INinjascript
        {
            ConstructorInfo construct = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (construct != null)
                return (T)construct.Invoke(new object[] { });
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// Creates <see cref="TBuilder"/> for the <see cref="TScript"/> object default instance.
        /// </summary>
        /// <returns>The <see cref="TBuilder"/> default instance.</returns>
        public static TBuilder CreateDefaultBuilder()
        {
            TScript script = CreateNinjascriptInstance();
            return (TBuilder)CreateNinjascriptBuilderInstance(script); 
        }

        #endregion

    }

}

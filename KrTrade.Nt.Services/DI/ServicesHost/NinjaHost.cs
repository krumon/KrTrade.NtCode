using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public class NinjaHost : INinjaHost
    {
        
        #region Implementation

        public IServiceProvider Services { get; set; }
        public PrintService PrintService { get; set; }

        ///// <summary>
        ///// Gets the Ninjatrader NinjaScript.
        ///// </summary>
        //public NinjaScriptBase Ninjascript { get; protected set; }

        protected Dictionary<Type, INinjascriptService> ServiceCollection { get; set; }
        
        #endregion

        #region Private members

        private List<Exception> Exceptions { get; set; }

        #endregion

        #region Constructors

        public NinjaHost(IServiceProvider services, NinjaScriptBase ninjascript,  PrintService printService)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            PrintService = printService ?? throw new ArgumentNullException(nameof(printService));
        }

        #endregion

        #region Public methods

        //public void Add<TService>()
        //    where TService : BaseService, new()
        //{
        //    Add(typeof(TService), () => CreateService<TService>(this));
        //}

        //public void Add<TService, TOptions>(TOptions options)
        //    where TService : BaseService<TOptions>, new()
        //    where TOptions : BaseOptions, new()
        //{
        //    Add(typeof(TService), () => CreateService<TService,TOptions>(this, options));
        //}

        //public void Add<TService, TOptions>(Action<TOptions> configureOptions)
        //    where TService : BaseService<TOptions>, new()
        //    where TOptions : BaseOptions, new()
        //{
        //    Add(typeof(TService), () => CreateService<TService, TOptions>(this, configureOptions));
        //}

        public T Get<T>()
            where T : BaseNinjascriptService
        {
            
            if (ServiceCollection.TryGetValue(typeof(T), out var service))
                return (T)service;
            else return default;
        }

        public void Build()
        {
            if (ServiceCollection == null || ServiceCollection.Count == 0)
                return;
            //PrintService print = Get<PrintService>();
            //if (print != null)
            //    Recursive(print);
        }

        #endregion

        #region Private methods

        private void Recursive(BaseNinjascriptService service)
        {
            //foreach (var key in ServiceCollection.Keys)
            //{
            //    ServiceCollection[key].PrintService = (PrintService)service;
            //    //if (service[key] is IChildService serviceChild)
            //}
        }

        private void Add(Type type, BaseNinjascriptService service)
        {
            if (service == null)
            {
                if (Exceptions == null)
                    Exceptions = new List<Exception>();
                Exceptions.Add(new Exception($"The {type.Name} service could not be added because 'service' parameter is null."));
                return;
            }

            if (ServiceCollection == null) 
                ServiceCollection = new Dictionary<Type, INinjascriptService>();

            if (ServiceCollection.ContainsKey(type))
            {
                if (Exceptions == null)
                    Exceptions = new List<Exception>();
                Exceptions.Add(new Exception($"The {service.Name} could not be added because it already exists."));
                return;
            }
            ServiceCollection.Add(type, service);
            //if (type == typeof(PrintService))
            //    PrintService = Get<PrintService>();
        }

        private void Add(Type type, Func<BaseNinjascriptService> delegateServiceCtor)
        {
            if (delegateServiceCtor == null)
            {
                if (Exceptions == null)
                    Exceptions = new List<Exception>();
                Exceptions.Add(new Exception($"The {type.Name} service could not be added because 'delegate parameter constructor' is null."));
                return;
            }

            if (ServiceCollection == null) 
                ServiceCollection = new Dictionary<Type, INinjascriptService>();

            if (ServiceCollection.ContainsKey(type))
            {
                if (Exceptions == null)
                    Exceptions = new List<Exception>();
                Exceptions.Add(new Exception($"The {type.Name} service could not be added because it already exists."));
                return;
            }
            ServiceCollection.Add(type, delegateServiceCtor());
            //if (type == typeof(PrintService))
            //    PrintService = Get<PrintService>();

        }

        #endregion

    }
}

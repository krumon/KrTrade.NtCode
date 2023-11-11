using KrTrade.Nt.Core.Events;
using KrTrade.Nt.Services.Bars;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class NinjaScriptService //: INinjaScriptService
    {
        private readonly NinjaScriptBase _ninjascript;
        private readonly NinjaScriptServiceOptions _options;
        private PrintService _printService;
        private BarsService _barService;

        private NinjaScriptService(NinjaScriptBase ninjascript) : this(ninjascript, null) { }
        private NinjaScriptService(NinjaScriptBase ninjascript, NinjaScriptServiceOptions options)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _options = options ?? new NinjaScriptServiceOptions();

            if (_ninjascript.State != State.Configure)
                throw new Exception("The 'NinjaScriptService' instance must be created in 'NinjaScript.OnStateChanged' method when 'State = State.Configure'");

            _printService = new PrintService(_ninjascript);
            _barService = new BarsService(_ninjascript, _printService);

            Configure();
        }

        //public static INinjaScriptService Configure(NinjaScriptBase ninjascript)
        //{
        //    INinjaScriptService service = new NinjaScriptService(ninjascript);
        //    service.Configure();
        //    return service;
        //}
        //public static INinjaScriptService Configure(NinjaScriptBase ninjascript, Action<NinjaScriptServiceOptions> delegateOptions)
        //{
        //    NinjaScriptServiceOptions options = new NinjaScriptServiceOptions();
        //    delegateOptions?.Invoke(options);
        //    INinjaScriptService service = new NinjaScriptService(ninjascript,options);
        //    service.Configure();
        //    return service;
        //}
        
        public void Configure()
        {
            //_options.NormalizeOptions();
        }
        public void DataLoaded()
        {
        }

        public void OnBarUpdate()
        {

        }

        public virtual void OnLastBarRemoved(){}
        public virtual void OnBarClosed(){}
        public virtual void OnPriceChanged(PriceChangedEventArgs args){}
        public virtual void OnEachTick(){}
        public virtual void OnFirstTick(){}

        private void LastBarRemovedHandler() 
        {
            // Call to parent
            OnLastBarRemoved();

            ////Call to listeners
            //LastBarRemoved?.Invoke();
        }
        private void BarClosedHandler() 
        {
            // Call to parent
            OnBarClosed();

            ////Call to listeners
            //BarClosed?.Invoke();
        }
        private void PriceChangedHandler(PriceChangedEventArgs args) 
        { 
            // Make sure the arguments is not null.
            if (args ==  null)
                throw new ArgumentNullException("args");

            // Call to parent
            OnPriceChanged(args);

            ////Call to listeners
            //PriceChanged?.Invoke(args);
        }
        private void TickHandler(TickEventArgs args) 
        {
            // Make sure the arguments is not null.
            if (args == null)
                throw new ArgumentNullException("args");

            // Call to parents
            if (args.IsFirstTick)
                OnFirstTick();

            OnEachTick();

            ////Call to listeners
            //Tick?.Invoke(args);

        }
    }
}

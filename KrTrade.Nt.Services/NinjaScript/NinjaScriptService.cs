using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Services
{
    public class NinjaScriptService : BaseNinjaScript, INinjaScriptService
    {
        private double _minGapValue;
        private double _maxGapValue;
        private int[] _saveCurrentBars;
        private double[] _lastPrices;
        private double[] _currentPrices;
        private NinjaScriptEvent[] _nsEvents;
        private readonly NinjaScriptServiceOptions _options;

        public event Action BarClosed;
        public event Action<PriceChangedEventArgs> PriceChanged;
        public event Action<TickEventArgs> Tick;
        public event Action LastBarRemoved;

        private NinjaScriptService(NinjaScriptBase ninjascript) : this(ninjascript, null) { }
        private NinjaScriptService(NinjaScriptBase ninjascript, NinjaScriptServiceOptions options) : base(ninjascript,NinjaScriptName.Ninjascript, NinjaScriptType.Service)
        {
            _options = options ?? new NinjaScriptServiceOptions(); 
        }

        public static INinjaScriptService Configure(NinjaScriptBase ninjascript)
        {
            INinjaScriptService service = new NinjaScriptService(ninjascript);
            service.Configure();
            return service;
        }
        public static INinjaScriptService Configure(NinjaScriptBase ninjascript, Action<NinjaScriptServiceOptions> delegateOptions)
        {
            NinjaScriptServiceOptions options = new NinjaScriptServiceOptions();
            delegateOptions?.Invoke(options);
            INinjaScriptService service = new NinjaScriptService(ninjascript,options);
            service.Configure();
            return service;
        }
        
        public override void Configure()
        {
            _options.NormalizeOptions();
            _minGapValue = GetMinGapValue();
            _maxGapValue = GetMaxGapValue();
        }
        public override void DataLoaded()
        {
            _saveCurrentBars = new int[NinjaScript.BarsArray.Length];
            _lastPrices = new double[NinjaScript.BarsArray.Length];
            _currentPrices = new double[NinjaScript.BarsArray.Length];
            _nsEvents = new NinjaScriptEvent[NinjaScript.BarsArray.Length];
            InitializeArray(_saveCurrentBars, -1);
            InitializeArray(_lastPrices, 0);
            InitializeArray(_currentPrices, 0);
            InitializeArray(_nsEvents, 0);
        }

        public void OnBarUpdate()
        {
            SetState(PriceState.Bar);
            double currentPrice = NinjaScript.Inputs[NinjaScript.BarsInProgress][0];
            int idx = NinjaScript.BarsInProgress;

            if (NinjaScript.CurrentBars[idx] < _saveCurrentBars[idx])
            {
                LastBarRemovedHandler();
                return;
            }
            else if (NinjaScript.CurrentBars[idx] != _saveCurrentBars[idx])
            {
                BarClosedHandler();
                if (NinjaScript.Calculate != Calculate.OnBarClose)
                {
                    if (NinjaScript.Calculate == Calculate.OnPriceChange && _currentPrices[idx] != currentPrice)
                    {
                        SetState(PriceState.Price);
                        PriceChangedHandler(new PriceChangedEventArgs(_currentPrices[idx], currentPrice));
                    }
                    else if (NinjaScript.Calculate == Calculate.OnEachTick)
                    {
                        SetState(PriceState.Tick);
                        TickHandler(new TickEventArgs(true));
                    }
                }
            }
            if (NinjaScript.Calculate == Calculate.OnPriceChange && _currentPrices[idx] != currentPrice)
            {
                SetState(PriceState.Price);
                PriceChangedHandler(new PriceChangedEventArgs(_currentPrices[idx], currentPrice));
            }
            else
            {
                SetState(PriceState.Tick);
                TickHandler(new TickEventArgs(false));
            }
            _saveCurrentBars[idx] = NinjaScript.CurrentBar;
            _currentPrices[idx] = currentPrice;
            SetState(PriceState.None);
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

            //Call to listeners
            LastBarRemoved?.Invoke();
        }
        private void BarClosedHandler() 
        {
            // Call to parent
            OnBarClosed();

            //Call to listeners
            BarClosed?.Invoke();
        }
        private void PriceChangedHandler(PriceChangedEventArgs args) 
        { 
            // Make sure the arguments is not null.
            if (args ==  null)
                throw new ArgumentNullException("args");

            // Call to parent
            OnPriceChanged(args);

            //Call to listeners
            PriceChanged?.Invoke(args);
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

            //Call to listeners
            Tick?.Invoke(args);

        }

        private void InitializeArray(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private void InitializeArray(double[] array, double value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private void InitializeArray(NinjaScriptEvent[] array, NinjaScriptEvent value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
        private double GetMinGapValue() => NinjaScript.TickSize * (double)_options.MinGapSize;
        private double GetMaxGapValue() => NinjaScript.TickSize * (double)_options.MaxGapSize;

    }
}

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

        public bool BarClosed => IsBarClosed(NinjaScript.BarsInProgress);
        public bool BarTick => !BarClosed;
        public bool LastBarRemoved => IsLastBarRemoved(NinjaScript.BarsInProgress);
        public bool PriceChanged => IsPriceChanged(NinjaScript.BarsInProgress);
        public bool Gap => IsGap(NinjaScript.BarsInProgress);
        public bool FirstTick => IsFirstTick(NinjaScript.BarsInProgress);

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
                OnLastBarRemoved();
                return;
            }
            else if (NinjaScript.CurrentBars[idx] != _saveCurrentBars[idx])
            {
                OnBarClosed();
                if (NinjaScript.Calculate != Calculate.OnBarClose)
                {
                    if (NinjaScript.Calculate == Calculate.OnPriceChange && _currentPrices[idx] != currentPrice)
                    {
                        SetState(PriceState.Price);
                        OnPriceChanged();
                    }
                    else if (NinjaScript.Calculate == Calculate.OnEachTick)
                    {
                        SetState(PriceState.Tick);
                        OnFirstTick();
                        OnEachTick();
                    }
                }
            }
            if (NinjaScript.Calculate == Calculate.OnPriceChange && _currentPrices[idx] != currentPrice)
            {
                SetState(PriceState.Price);
                OnPriceChanged();
            }
            else
            {
                SetState(PriceState.Tick);
                OnEachTick();
            }
            _saveCurrentBars[idx] = NinjaScript.CurrentBar;
            _currentPrices[idx] = currentPrice;
            SetState(PriceState.None);
        }

        public virtual void OnLastBarRemoved(){}
        public virtual void OnBarClosed(){}
        public virtual void OnPriceChanged(){}
        public virtual void OnEachTick(){}
        public virtual void OnFirstTick(){}

        public bool IsBarClosed(int barsInProgress = 0) => _saveCurrentBars[barsInProgress] != NinjaScript.CurrentBars[barsInProgress];
        public bool IsBarTick(int barsInProgress = 0) => !IsBarClosed(barsInProgress);
        public bool IsLastBarRemoved(int barsInProgress = 0) => NinjaScript.BarsArray[0].BarsType.IsRemoveLastBarSupported && NinjaScript.CurrentBars[barsInProgress] < _saveCurrentBars[barsInProgress];
        public bool IsPriceChanged(int barsInProgress = 0) => _lastPrices[barsInProgress] != _currentPrices[barsInProgress];
        public bool IsGap(int barsInProgress = 0) => IsPriceChanged(barsInProgress) && _currentPrices[barsInProgress] - _lastPrices[barsInProgress] >= _minGapValue;
        public bool IsFirstTick(int barsInProgress = 0) => NinjaScript.Calculate == Calculate.OnEachTick && IsBarTick(barsInProgress);

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

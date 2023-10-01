using KrTrade.Nt.Core.Interfaces;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core.Data
{
    public abstract class BaseNinjaScript : IBaseNinjaScript
    {
        private readonly NinjaScriptBase _ninjaScriptBase;
        private readonly NinjaScriptName _ninjaScriptName;
        private readonly NinjaScriptType _ninjaScriptType;
        private PriceState _priceState;

        public NinjaScriptBase NinjaScript => _ninjaScriptBase;
        public NinjaScriptName Name => _ninjaScriptName;
        public NinjaScriptType Type => _ninjaScriptType;
        public NinjaScriptState State => GetNinjaScriptState();
        public PriceState PriceState => _priceState;

        public BaseNinjaScript(NinjaScriptBase ninjaScriptBase, NinjaScriptName ninjaScriptName, NinjaScriptType ninjaScriptType)
        {
            _ninjaScriptBase = ninjaScriptBase ?? throw new Exception("The ninjascript argument cannot be null. The argument is necesary to configure the service.");
            _ninjaScriptName = ninjaScriptName;
            _ninjaScriptType = ninjaScriptType;
        }

        public override string ToString()
        {
            return Name == NinjaScriptName.Unknown ? this.GetType().Name : Name.ToString();
        }
        public virtual string ToLogString()
        {
            return Name.ToLogString() + "[" + Type.ToLogString() + "]";
        }
        public void SetState(PriceState state) => _priceState = state;

        public virtual void SetDefaults(){}
        public virtual void Configure(){}
        public virtual void Active(){}
        public virtual void DataLoaded(){}
        public virtual void Historical(){}
        public virtual void Transition(){}
        public virtual void Realtime(){}
        public virtual void Terminated(){}

        private NinjaScriptState GetNinjaScriptState()
        {
            if (NinjaScript.State == NinjaTrader.NinjaScript.State.Historical) { return NinjaScriptState.Historical; }
            if (NinjaScript.State == NinjaTrader.NinjaScript.State.Realtime) { return NinjaScriptState.Realtime; }
            if (NinjaScript.State != NinjaTrader.NinjaScript.State.SetDefaults) { return NinjaScriptState.None; }

            return NinjaScriptState.Configure;
        }
    }
}

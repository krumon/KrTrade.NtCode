using NinjaTrader.NinjaScript;
using KrTrade.Nt.DI.Data;

namespace KrTrade.Nt.Scripts.NinjatraderObjects
{
    public abstract class BaseNinjaScript : INinjaScript
    {
        //private readonly NinjaScriptBase _ninjascript;

        //public State State => _ninjascript.State;
        public State State => State.Configure;
        public NinjaScriptEvent NinjaScriptEvent { get; private set; }

        public BaseNinjaScript()
        {
        }

        public void OnNinjaScriptChange(NinjaScriptEvent ninjascriptEvent)
        {
            NinjaScriptEvent = ninjascriptEvent;

            if (NinjaScriptEvent == NinjaScriptEvent.BarUpdate)
                OnBarUpdate();
        }

        public void OnStateChange()
        {
            //if (_ninjascript.State == State.SetDefaults)
            //    SetDafaults();
            //else if (_ninjascript.State == State.Configure)
            //    Configure();
            //else if (_ninjascript.State == State.Active)
            //    Active();
            //else if (_ninjascript.State == State.DataLoaded)
            //    DataLoaded();
            //else if (_ninjascript.State == State.Historical)
            //    Historical();
            //else if (_ninjascript.State == State.Transition)
            //    Transition();
            //else if (_ninjascript.State == State.Realtime)
            //    Realtime();
            //else if (_ninjascript.State == State.Terminated)
            //    Terminated();
        }

        public virtual void SetDafaults() { }
        public virtual void Configure() { }
        public virtual void Active() { }
        public virtual void DataLoaded() { }
        public virtual void Historical() { }
        public virtual void Transition() { }
        public virtual void Realtime() { }
        public virtual void Terminated() { }
        
        public virtual void OnBarUpdate() { }
        public virtual void OnSessionUpdate() { }
    }
}

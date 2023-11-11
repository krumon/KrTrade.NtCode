using NinjaTrader.NinjaScript;
using KrTrade.Nt.Core.NinjaScript;

namespace KrTrade.Nt.Scripts.NinjatraderObjects
{
    public interface INinjaScript
    {
        State State { get; }
        NinjaScriptEvent NinjaScriptEvent { get; }
        void OnStateChange();
        void OnNinjaScriptChange(NinjaScriptEvent ninjascriptEvent);
    }
}

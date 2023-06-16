using NinjaTrader.NinjaScript;
using KrTrade.NtCode.Data;

namespace KrTrade.NtCode.NinjatraderObjects
{
    public interface INinjaScript
    {
        State State { get; }
        NinjaScriptEvent NinjaScriptEvent { get; }
        void OnStateChange();
        void OnNinjaScriptChange(NinjaScriptEvent ninjascriptEvent);
    }
}

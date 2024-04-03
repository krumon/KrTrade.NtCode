using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Services.Series
{
    public interface INinjaSeries<TElement,TInput> : INumericSeries<TElement,TInput,NinjaScriptBase> where TElement: struct { }
    public interface INinjaSeries<TElement,TInput1,TInput2> : INumericSeries<TElement,TInput1,TInput2,NinjaScriptBase> where TElement : struct { }
}

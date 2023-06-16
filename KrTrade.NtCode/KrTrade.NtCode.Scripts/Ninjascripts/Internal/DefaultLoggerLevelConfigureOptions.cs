using KrTrade.NtCode.Options;

namespace KrTrade.NtCode.Ninjascripts.Internal
{
    internal sealed class DefaultNinjascriptLevelConfigureOptions : ConfigureOptions<NinjascriptFilterOptions>
    {
        public DefaultNinjascriptLevelConfigureOptions(NinjascriptLevel level) : base(options => options.MinLevel = level)
        {
        }
    }
}

using Nt.Core.Options;

namespace Nt.Scripts.Ninjascripts.Internal
{
    internal sealed class DefaultNinjascriptLevelConfigureOptions : ConfigureOptions<NinjascriptFilterOptions>
    {
        public DefaultNinjascriptLevelConfigureOptions(NinjascriptLevel level) : base(options => options.MinLevel = level)
        {
        }
    }
}

using System.Collections.Generic;

namespace Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// The options for a NinjascriptFilter.
    /// </summary>
    public class NinjascriptFilterOptions
    {
        /// <summary>
        /// Creates a new <see cref="NinjascriptFilterOptions"/> instance.
        /// </summary>
        public NinjascriptFilterOptions() { }

        /// <summary>
        /// Gets or sets the minimum level of ninjascript service if none of the rules match.
        /// </summary>
        public NinjascriptLevel MinLevel { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="NinjascriptFilterRule"/> used for filtering ninjascript services.
        /// </summary>
        public IList<NinjascriptFilterRule> Rules => RulesInternal;

        // Concrete representation of the rule list
        internal List<NinjascriptFilterRule> RulesInternal { get; } = new List<NinjascriptFilterRule>();
    }
}

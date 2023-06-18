using System;

namespace KrTrade.Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// Defines a rule used to filter ninjascript services
    /// </summary>
    public class NinjascriptFilterRule
    {
        /// <summary>
        /// Creates a new <see cref="NinjascriptFilterRule"/> instance.
        /// </summary>
        /// <param name="providerName">The provider name to use in this filter rule.</param>
        /// <param name="categoryName">The category name to use in this filter rule.</param>
        /// <param name="logLevel">The <see cref="NinjascriptLevel"/> to use in this filter rule.</param>
        /// <param name="filter">The filter to apply.</param>
        public NinjascriptFilterRule(string providerName, string categoryName, NinjascriptLevel? logLevel, Func<string, string, NinjascriptLevel, bool> filter)
        {
            ProviderName = providerName;
            CategoryName = categoryName;
            NinjascriptLevel = logLevel;
            Filter = filter;
        }

        /// <summary>
        /// Gets the ninjascript provider type or alias this rule applies to.
        /// </summary>
        public string ProviderName { get; }

        /// <summary>
        /// Gets the ninjascript category this rule applies to.
        /// </summary>
        public string CategoryName { get; }

        /// <summary>
        /// Gets the minimum <see cref="Ninjascripts.NinjascriptLevel"/> of service.
        /// </summary>
        public NinjascriptLevel? NinjascriptLevel { get; }

        /// <summary>
        /// Gets the filter delegate that would be applied to service that passed the <see cref="Ninjascripts.NinjascriptLevel"/>.
        /// </summary>
        public Func<string, string, NinjascriptLevel, bool> Filter { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(ProviderName)}: '{ProviderName}', {nameof(CategoryName)}: '{CategoryName}', {nameof(NinjascriptLevel)}: '{NinjascriptLevel}', {nameof(Filter)}: '{Filter}'";
        }
    }
}

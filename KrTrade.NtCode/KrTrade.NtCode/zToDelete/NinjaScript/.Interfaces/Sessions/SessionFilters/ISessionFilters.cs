using System;

namespace ConsoleApp
{

    /// <summary>
    /// Interface for any session filters.
    /// </summary>
    public interface ISessionFilters : ISession
    {

        /// <summary>
        /// Creates the <see cref="ISessionFiltersBuilder"/> to construct the <see cref="ISessionFilters"/> object.
        /// </summary>
        /// <returns>The <see cref="ISessionFiltersBuilder"/> to construct the <see cref="ISessionFilters"/> object.</returns>
        ISessionFiltersBuilder CreateSessionFiltersBuilder();

    }
}

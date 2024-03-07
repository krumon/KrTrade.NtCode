namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built <see cref="IFiltersService"/> objects. 
    /// </summary>
    public interface IFiltersBuilder : IBarUpdateBuilder<IFiltersService,FiltersOptions, IFiltersBuilder>
    {
    }
}

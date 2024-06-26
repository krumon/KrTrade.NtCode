﻿using KrTrade.Nt.Core.Services;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create a filter service.
    /// </summary>
    public interface IFiltersService : IBarUpdateService<FiltersInfo,FiltersOptions>
    {
    }
}

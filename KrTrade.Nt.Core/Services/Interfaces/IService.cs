﻿using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Infos;
using KrTrade.Nt.Core.Options;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService : IElement<ServiceType>
    {
        /// <summary>
        /// Gets the calculate mode of the service.
        /// </summary>
        Calculate CalculateMode { get; }

        /// <summary>
        /// Gets the service calculation mode when another series is updated. 
        /// </summary>
        MultiSeriesCalculateMode MultiSeriesCalculateMode { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabOrder"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="description"></param>
        /// <param name="index"></param>
        /// <param name="isDescriptionBracketsVisible"></param>
        /// <param name="isIndexVisible"></param>
        /// <param name="separator"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        string ToString(int tabOrder, string title, string subtitle, string description, int index, bool isDescriptionBracketsVisible, bool isIndexVisible, string separator, string state);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        string ToString(string state);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabOrder"></param>
        /// <param name="state"></param>
        /// <param name="index"></param>
        /// <param name="separator"></param>
        /// <param name="isTitleVisible"></param>
        /// <param name="isSubtitleVisible"></param>
        /// <param name="isDescriptionVisible"></param>
        /// <param name="isDescriptionBracketsVisible"></param>
        /// <param name="isIndexVisible"></param>
        /// <returns></returns>
        string ToString(int tabOrder, string state, int index = 0, string separator = ": ", bool isTitleVisible = true, bool isSubtitleVisible = false, bool isDescriptionVisible = true, bool isDescriptionBracketsVisible = true, bool isIndexVisible = false);
    }

    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService<TInfo,TOptions> : IService, IElement<ServiceType,TInfo,TOptions>
        where TInfo : IServiceInfo
        where TOptions : IServiceOptions
    {
    }

}

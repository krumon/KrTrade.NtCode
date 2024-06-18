﻿using KrTrade.Nt.Core.Data;
using NinjaTrader.NinjaScript;

namespace KrTrade.Nt.Core
{
    /// <summary>
    /// Defines properties and methods for any ninjascript service.
    /// </summary>
    public interface IService : IElement<ServiceType,IServiceInfo,IServiceOptions>
    {
        /// <summary>
        /// Indicates that the logger service is enabled.
        /// </summary>
        bool IsLogEnable { get; }

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

}

﻿using KrTrade.Nt.Core.Core;
using KrTrade.Nt.Core.Print;
using NinjaTrader.NinjaScript;
using System;

namespace KrTrade.Nt.Core
{

    /// <summary>
    /// Extensions methods of <see cref="State"/> object.
    /// </summary>
    public static class StateExtensions
    {
        /// <summary>
        /// Converts from <see cref="State"/> enum to string with the <see cref="FormatType"/> and the <see cref="FormatLength"/> indicated.
        /// </summary>
        /// <param name="state">The <see cref="State"/>object to convert.</param>
        /// <param name="formatType">The <see cref="FormatType"/> of the string to returns.</param>
        /// <param name="formatLength">The <see cref="FormatLength"/> of the string to returns.</param>
        /// <returns>A string with the <see cref="FormatType"/> and the <see cref="FormatLength"/> indicated.</returns>
        /// <exception cref="NotImplementedException">The <see cref="FormatType"/> specificated is not implemented yet.</exception>
        public static string ToString(this State state, FormatType formatType, FormatLength formatLength)
        {
            switch (formatType)
            {
                case FormatType.Default:
                case FormatType.File:
                case FormatType.Chart:
                    return state.ToString();
                case FormatType.Log:
                    return ToLogString(state, formatLength);
                default: 
                    throw new NotImplementedException(state.ToString(state.ToString()));
            }
        }

        /// <summary> 
        /// Converts from <see cref="State"/> enum to short string.
        /// </summary>
        /// <param name="state"><see cref="State"/> object to convert.</param>
        /// <param name="formatLength">The format length.</param>
        /// <returns>The state short or long string.</returns>
        public static string ToLogString(this State state, FormatLength formatLength)
        {
            if (formatLength == FormatLength.Long)
            {
                switch (state)
                {
                    case State.SetDefaults: return "stdefault";
                    case State.Configure: return "configure";
                    case State.Active: return "active___";
                    case State.DataLoaded: return "dtaloaded";
                    case State.Historical: return "hstorical";
                    case State.Transition: return "trnsition";
                    case State.Realtime: return "real_time";
                    case State.Terminated: return "trminated";
                    default: throw new NotImplementedException(state.ToString());
                }
            }
            else if (formatLength == FormatLength.Short)
            {
                switch (state)
                {
                    case State.SetDefaults: return "dflt";
                    case State.Configure: return "cnfg";
                    case State.Active: return "actv";
                    case State.DataLoaded: return "dtld";
                    case State.Historical: return "hstc";
                    case State.Transition: return "tnst";
                    case State.Realtime: return "rltm";
                    case State.Terminated: return "trtd";
                    default: throw new NotImplementedException(state.ToString());
                }
            }
            else 
                throw new NotImplementedException(formatLength.ToString());
        }

        /// <summary>
        /// Converts from <see cref="State"/> object to <see cref="PrintLevel"/> object.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static PrintLevel ToPrintLevel(this State state)
        {
            switch (state)
            {
                case State.SetDefaults:
                case State.Configure:
                case State.Active:
                case State.DataLoaded:
                case State.Terminated:
                    return PrintLevel.Configuration;
                case State.Historical:
                case State.Transition:
                    return PrintLevel.Historical;
                case State.Realtime:
                    return PrintLevel.Realtime;
                default: return PrintLevel.None;
            }
        }
    }
}

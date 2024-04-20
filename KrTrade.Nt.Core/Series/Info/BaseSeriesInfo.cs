using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Info;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Core.Series
{
    public abstract class BaseSeriesInfo : BaseKeyInfo
    {

        /// <summary>
        /// Gets or sets the type of the series.
        /// </summary>
        public SeriesType Type { get; set; }

        /// <summary>
        /// Gets the maximum values to store in cache.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets the maximum old values to store in cache.
        /// </summary>
        public int OldValuesCapacity { get; set; }

        /// <summary>
        /// Gets or sets the inputs series.
        /// </summary>
        public List<BaseSeriesInfo> Inputs { get; internal set; }

        /// <summary>
        /// Adds the input series to the series object.
        /// </summary>
        /// <typeparam name="TInfo">The type of the series options.</typeparam>
        /// <param name="configureSeriesInfo">Delegate to configure the series options.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="configureSeriesInfo"/> cannot be null.</exception>
        public void AddInputSeries<TInfo>(Action<TInfo> configureSeriesInfo)
            where TInfo : BaseSeriesInfo, new()
        {
            if (configureSeriesInfo == null)
                throw new ArgumentNullException(nameof(configureSeriesInfo));

            TInfo info = new TInfo();
            configureSeriesInfo(info);

            // +++ Comprobar que la información de la serie es válida.

            // Comprobar si es una 'NinjaScript' series y el usuario le ha añadido un Input.
            if ((info.Type == SeriesType.INDEX || info.Type == SeriesType.TIME || info.Type == SeriesType.OPEN || info.Type == SeriesType.HIGH ||
                info.Type == SeriesType.LOW || info.Type == SeriesType.CLOSE || info.Type == SeriesType.VOLUME || info.Type == SeriesType.TICK)
                && info.Inputs != null && info.Inputs.Count > 0)
                // Lanzar un error
                return;

            // Comprobar las series de 1 INPUT Series.
            if ((info.Type == SeriesType.AVG || info.Type == SeriesType.DEVSTD || info.Type == SeriesType.MAX || info.Type == SeriesType.MIN ||
                info.Type == SeriesType.SUM || info.Type == SeriesType.SWING_HIGH || info.Type == SeriesType.SWING_LOW)
                && info.Inputs != null && info.Inputs.Count != 1)
                // Lanzar un error
                return;

            // Comprobar las series de 2 INPUT Series.
            if ((info.Type == SeriesType.RANGE)
                && info.Inputs != null && info.Inputs.Count != 2)
                // Lanzar un error
                return;

            if (Inputs == null)
                Inputs = new List<BaseSeriesInfo>();

            Inputs.Add(info);
        }

        /// <summary>
        /// Gets the key of the series configured.
        /// </summary>
        /// <returns>The unique key of the series configured.</returns>
        protected override string GetKey() => $"{GetRootKey()}({GetInputKey()}{GetParametersKey()})";

        protected string GetRootKey() => $"{Type}(";
        protected string GetInputKey()
        {
            if (Inputs == null || Inputs.Count == 0)
                return string.Empty;
            string inputKey = string.Empty;
            for (int i = 0; i < Inputs.Count; i++)
            {
                inputKey += $"{Inputs[i].GetKey()}";
                if (i != Inputs.Count - 1)
                    inputKey += ";";
            }
            return inputKey;
        }
        protected string GetParametersKey()
        {
            object[] parameters = GetParameters();
            string key = string.Empty;
            if (parameters != null && parameters.Length > 0)
            {
                key += ",";
                for (int i = 0; i < parameters.Length; i++)
                {
                    key += parameters[i].ToString();
                    if (i != parameters.Length - 1) 
                        key += ",";
                }
            }
            return key;
        }

        protected abstract object[] GetParameters();

        public static bool operator ==(BaseSeriesInfo series1, BaseSeriesInfo series2) => 
            (series1 is null && series2 is null) || 
            (!(series1 is null) && !(series2 is null) && series1.GetKey() == series2.GetKey());
        public static bool operator !=(BaseSeriesInfo series1, BaseSeriesInfo series2) => !(series1 == series2);

        public override bool Equals(object obj) => obj is BaseSeriesInfo other && this == other;
        public override int GetHashCode() => GetKey().GetHashCode();

    }

}

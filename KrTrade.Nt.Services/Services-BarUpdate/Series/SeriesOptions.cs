using KrTrade.Nt.Core.Data;
using System;
using System.Collections.Generic;

namespace KrTrade.Nt.Services
{
    public abstract class BaseSeriesOptions : BarUpdateServiceOptions
    {

        /// <summary>
        /// Gets or sets the type of the series.
        /// </summary>
        public SeriesType Type { get; set; }

        /// <summary>
        /// Gets the maximum values to store in cache.
        /// </summary>
        public int Capacity { get; internal set; }

        /// <summary>
        /// Gets the maximum old values to store in cache.
        /// </summary>
        public int OldValuesCapacity { get; internal set; }

        /// <summary>
        /// Gets or sets the inputs series.
        /// </summary>
        public List<SeriesOptions> Inputs { get; internal set; }

        /// <summary>
        /// Adds the input series to the series object.
        /// </summary>
        /// <typeparam name="TOptions">The type of the series options.</typeparam>
        /// <param name="configureSeriesOptions">Delegate to configure the series options.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="configureSeriesOptions"/> cannot be null.</exception>
        public void AddInputSeries<TOptions>(Action<TOptions> configureSeriesOptions)
            where TOptions : SeriesOptions, new()
        {
            if (configureSeriesOptions == null)
                throw new ArgumentNullException(nameof(configureSeriesOptions));

            if (Inputs == null)
                Inputs = new List<SeriesOptions>();

            TOptions options = new TOptions();
            Inputs.Add(options);
            configureSeriesOptions(options);
        }

        /// <summary>
        /// Gets the key of the series configured.
        /// </summary>
        /// <returns>The unique key of the series configured.</returns>
        public string GetKey() => $"{GetRootKey()}({GetInputKey()},{GetParametersKey()})";

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
        protected abstract string GetParametersKey();

    }

    public class SeriesOptions : BaseSeriesOptions
    {

        /// <summary>
        /// Gets series period.
        /// </summary>
        public int Period { get; internal set; }

        protected override string GetParametersKey() => Period.ToString();

    }
    public class SwingSeriesOptions : BaseSeriesOptions
    {

        /// <summary>
        /// Gets swing left strength.
        /// </summary>
        public int LeftStrength { get; internal set; }

        /// <summary>
        /// Gets swing right strength.
        /// </summary>
        public int RightStrength { get; internal set; }

        protected override string GetParametersKey() => $"{LeftStrength},{RightStrength}";

    }
}

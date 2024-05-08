using KrTrade.Nt.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrTrade.Nt.Core.Series
{

    public abstract class SeriesInfo : BaseSeriesInfo<SeriesType>, ISeriesInfo
    {

        public List<ISeriesInfo> Inputs { get; set; }
        public void AddInputSeries<TInfo>(Action<TInfo> configureSeriesInfo)
            where TInfo : ISeriesInfo, new()
        {
            if (configureSeriesInfo == null)
                throw new ArgumentNullException(nameof(configureSeriesInfo));

            TInfo info = new TInfo();
            configureSeriesInfo(info);

            //// +++ Comprobar que la información de la serie es válida.

            //// Comprobar si es una 'NinjaScript' series y el usuario le ha añadido una Input series.
            //if ((info.Type == SeriesType.CURRENT_BAR || info.Type == SeriesType.TIME || info.Type == SeriesType.OPEN || info.Type == SeriesType.HIGH ||
            //    info.Type == SeriesType.LOW || info.Type == SeriesType.CLOSE || info.Type == SeriesType.VOLUME || info.Type == SeriesType.TICK)
            //    && info.Inputs != null && info.Inputs.Count > 0)
            //    // Lanzar un error
            //    return;

            //// Comprobar las series de 1 INPUT Series.
            //if ((info.Type == SeriesType.AVG || info.Type == SeriesType.DEVSTD || info.Type == SeriesType.MAX || info.Type == SeriesType.MIN ||
            //    info.Type == SeriesType.SUM || info.Type == SeriesType.SWING_HIGH || info.Type == SeriesType.SWING_LOW)
            //    && info.Inputs != null && info.Inputs.Count != 1)
            //    // Lanzar un error
            //    return;

            //// Comprobar las series de 2 INPUT Series.
            //if ((info.Type == SeriesType.RANGE)
            //    && info.Inputs != null && info.Inputs.Count != 2)
            //    // Lanzar un error
            //    return;

            if (Inputs == null)
                Inputs = new List<ISeriesInfo>();

            Inputs.Add(info);
        }

        protected override string GetKey() 
        { 
            // Represento la clave con "SeriesType(Input1.Key,Input2.Key,...,Parameter1,Parameter2,...)"
            string key = $"{GetRootKey()}({GetInputsKey()}{GetParametersKey()})";
            // Compruebo si los paréntesis de la clave están vacíos. En caso de que así sea los elimino
            if (key.Substring(key.Length - 2, key.Length - 1) == "()")
                key.Remove(key.Length - 2);

            return key;
        } 

        protected override string GetInputsKey()
        {
            if (Inputs == null || Inputs.Count == 0)
                return string.Empty;
            string inputKey = string.Empty;
            for (int i = 0; i < Inputs.Count; i++)
            {
                inputKey += $"{Inputs[i].Key}";
                if (i != Inputs.Count - 1)
                    inputKey += ",";
            }
            return inputKey;
        }

        protected IEnumerable<string> GetKeys(bool orderByAscending = true)
        {
            if (Inputs == null || Inputs.Count == 0)
                return null;
            IList<string> keys = new List<string>();
            for (int i = 0; i < Inputs.Count; i++)
                AddKey(keys, Inputs[i], orderByAscending);
            return orderByAscending
                ? keys
                .OrderBy(key => key)
                .Select(key => key)
                .ToList()
                : keys
                .OrderByDescending(key => key)
                .Select(key => key)
                .ToList();
        }
        private void AddKey(IList<string> keys, IBaseSeriesInfo info, bool orderByAscending)
        {
            keys.Add(info.Key);

            if (Inputs != null && Inputs.Count != 0)
                for (int i = 0; i < Inputs.Count; i++)
                    AddKey(keys, Inputs[i], orderByAscending);
        }

        public static bool operator ==(SeriesInfo series1, SeriesInfo series2) => 
            (series1 is null && series2 is null) || 
            (!(series1 is null) && !(series2 is null) && series1.GetKey() == series2.GetKey());
        public static bool operator !=(SeriesInfo series1, SeriesInfo series2) => !(series1 == series2);

        public override bool Equals(object obj) => obj is ISeriesInfo other && this == other;
        public override int GetHashCode() => GetKey().GetHashCode();

    }

    public abstract class SeriesInfo<T> : SeriesInfo, ISeriesInfo<T>
        where T : Enum
    {
        public new T Type { get; set; }
    }

}

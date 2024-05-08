using KrTrade.Nt.Core.Data;
using KrTrade.Nt.Core.Info;
using System;

namespace KrTrade.Nt.Core.Series
{
    public abstract class BaseSeriesInfo : BaseKeyInfo, IBaseSeriesInfo
    {

        public SeriesType Type { get; set; }
        public int Capacity { get; set; }
        public int OldValuesCapacity { get; set; }

        protected override string GetKey() 
        { 
            // Represento la clave con "SeriesType(Input1.Key,Input2.Key,...,Parameter1,Parameter2,...)"
            string key = $"{GetRootKey()}({GetInputsKey()}{GetParametersKey()})";
            // Compruebo si los paréntesis de la clave están vacíos. En caso de que así sea los elimino
            if (key.Substring(key.Length - 2, key.Length - 1) == "()")
                key.Remove(key.Length - 2);

            return key;
        }

        protected abstract string GetRootKey(); // => Type.ToString();
        protected abstract string GetInputsKey(); // => string.Empty;
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

        public override bool Equals(object obj) => obj is IBaseSeriesInfo other && this == other;
        public override int GetHashCode() => GetKey().GetHashCode();

    }

    public abstract class BaseSeriesInfo<T> : BaseSeriesInfo, IBaseSeriesInfo<T>
        where T : Enum
    {
        public new T Type { get; set; }

        protected override string GetKey()
        {
            // Represento la clave con "SeriesType(Input1.Key,Input2.Key,...,Parameter1,Parameter2,...)"
            string key = $"{GetRootKey()}({GetInputsKey()}{GetParametersKey()})";
            // Compruebo si los paréntesis de la clave están vacíos. En caso de que así sea los elimino
            if (key.Substring(key.Length - 2, key.Length - 1) == "()")
                key.Remove(key.Length - 2);

            return key;
        }

        protected override string GetRootKey() => Type.ToString();

        public static bool operator ==(BaseSeriesInfo<T> series1, BaseSeriesInfo<T> series2) =>
            (series1 is null && series2 is null) ||
            (!(series1 is null) && !(series2 is null) && series1.GetKey() == series2.GetKey());
        public static bool operator !=(BaseSeriesInfo<T> series1, BaseSeriesInfo<T> series2) => !(series1 == series2);

        public override bool Equals(object obj) => obj is IBaseSeriesInfo other && this == other;
        public override int GetHashCode() => GetKey().GetHashCode();

    }
}

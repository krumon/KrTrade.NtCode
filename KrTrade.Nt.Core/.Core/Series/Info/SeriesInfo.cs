using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core
{
    public abstract class SeriesInfo : Info<SeriesType>, ISeriesInfo
    {

        //new public SeriesType Type { get => base.Type.ToSeriesType(); set => base.Type = value.ToElementType(); }
        public int Capacity { get; set; }
        public int OldValuesCapacity { get; set; }

        public override string ToString()
        {
            string name = string.IsNullOrEmpty(Name) ? Type.ToString() : Name;
            string key = $"{name}({GetInputsKey()}{GetParametersKey()})";
            return (key.Substring(key.Length - 2) == "()") ? key.Remove(key.Length - 2) : key;
        }
        public string ToString(string ownerString)
        {
            string name = string.IsNullOrEmpty(Name) ? Type.ToString() : Name;
            string key = $"{name}({ownerString})({GetInputsKey()}{GetParametersKey()})";
            return (key.Substring(key.Length - 2) == "()") ? key.Remove(key.Length - 2) : key;
        }
        protected string ToUniqueString() 
        { 
            // Represento la clave con "SeriesType(Input1.Key,Input2.Key,...,Parameter1,Parameter2,...)"
            string key = $"{GetRootKey()}({GetInputsKey()}{GetParametersKey()})";
            // Compruebo si los paréntesis de la clave están vacíos. En caso de que así sea los elimino
            return (key.Substring(key.Length - 2) == "()") ? key.Remove(key.Length - 2) : key;
        }
        
        protected virtual string GetRootKey() => Type.ToString();
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

        public static bool operator ==(SeriesInfo series1, SeriesInfo series2) => 
            (series1 is null && series2 is null) || 
            (!(series1 is null) && !(series2 is null) && series1.ToUniqueString() == series2.ToUniqueString());
        public static bool operator !=(SeriesInfo series1, SeriesInfo series2) => !(series1 == series2);

        public override bool Equals(object obj) => obj is ISeriesInfo other && this == other;
        public override int GetHashCode() => ToUniqueString().GetHashCode();

    }

    public abstract class SeriesInfo<T> : SeriesInfo, ISeriesInfo<T>
        where T : Enum
    {
        private T _type;
        new public T Type 
        { 
            get => _type;
            set
            {
                base.Type = value.ToElementType().ToSeriesType();
                _type = value;
            }
        }

        //protected string ToUniqueString()
        //{
        //    // Represento la clave con "SeriesType(Input1.Key,Input2.Key,...,Parameter1,Parameter2,...)"
        //    string key = $"{GetRootKey()}({GetInputsKey()}{GetParametersKey()})";
        //    // Compruebo si los paréntesis de la clave están vacíos. En caso de que así sea los elimino
        //    return (key.Substring(key.Length - 2) == "()") ? key.Remove(key.Length - 2) : key;
        //}

        protected override string GetRootKey() => Type.ToString();

        public static bool operator ==(SeriesInfo<T> series1, SeriesInfo<T> series2) =>
            (series1 is null && series2 is null) ||
            (!(series1 is null) && !(series2 is null) && series1.ToUniqueString() == series2.ToUniqueString());
        public static bool operator !=(SeriesInfo<T> series1, SeriesInfo<T> series2) => !(series1 == series2);

        public override bool Equals(object obj) => obj is ISeriesInfo other && this == other;
        public override int GetHashCode() => ToUniqueString().GetHashCode();

    }

}

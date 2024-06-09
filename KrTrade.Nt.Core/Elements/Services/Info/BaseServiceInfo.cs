using KrTrade.Nt.Core.Data;
using System;

namespace KrTrade.Nt.Core.Elements
{
    public abstract class BaseServiceInfo : BaseInfo, IServiceInfo
    {
        new public ServiceType Type { get => base.Type.ToServiceType(); set => base.Type = value.ToElementType(); }

        protected BaseServiceInfo() { }
        protected BaseServiceInfo(ServiceType type) { Type = type; }

        protected override string ToUniqueString() => null;

    }

    public abstract class BaseServiceInfo<T> : BaseServiceInfo, IBaseServiceInfo<T>
    where T : Enum
    {
        private T _type;
        new public T Type
        {
            get => _type;
            set
            {
                base.Type = value.ToElementType().ToServiceType();
                _type = value;
            }
        }

        protected override string ToUniqueString()
        {
            //// Represento la clave con "SeriesType(Input1.Key,Input2.Key,...,Parameter1,Parameter2,...)"
            //string key = $"{GetRootKey()}({GetInputsKey()}{GetParametersKey()})";
            //// Compruebo si los paréntesis de la clave están vacíos. En caso de que así sea los elimino
            //return (key.Substring(key.Length - 2) == "()") ? key.Remove(key.Length - 2) : key;
            throw new NotImplementedException();
        }

        //protected override string GetRootKey() => Type.ToString();

        //public static bool operator ==(BaseSeriesInfo<T> series1, BaseSeriesInfo<T> series2) =>
        //    (series1 is null && series2 is null) ||
        //    (!(series1 is null) && !(series2 is null) && series1.ToUniqueString() == series2.ToUniqueString());
        //public static bool operator !=(BaseSeriesInfo<T> series1, BaseSeriesInfo<T> series2) => !(series1 == series2);

        //public override bool Equals(object obj) => obj is IBaseSeriesInfo other && this == other;
        //public override int GetHashCode() => ToUniqueString().GetHashCode();

    }

}

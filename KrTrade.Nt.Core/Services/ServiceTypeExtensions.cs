//using System;

//namespace KrTrade.Nt.Core.Services
//{

//    /// <summary>
//    /// Extensions methods of <see cref="SeriesType"/> object.
//    /// </summary>
//    public static class ServiceTypeExtensions
//    {

//        //public static string ToString(this ServiceType serviceType, bool appendServiceWord)
//        //    => appendServiceWord ? serviceType.ToLongString() : serviceType.ToString();

//        //public static string ToShortString(this ServiceType serviceType)
//        //    => serviceType.ToString().Replace('_', ' ');

//        //public static string ToLongString(this ServiceType serviceType)
//        //    => $"{serviceType.ToShortString()} SERVICE";

//        ///// <summary>
//        ///// Converts from <typeparamref name="T"/> enum to <see cref="ServiceType"/> enum.
//        ///// </summary>
//        ///// <param name="type">The specified enum to convert.</param>
//        ///// <returns>A <see cref="ServiceType"/> thats represents the specified <paramref name="type"/>.</returns>
//        //public static ServiceType ToServiceType<T>(this T type)
//        //    where T : Enum
//        //{
//        //    if (type is ServiceType serviceType)
//        //        return serviceType;

//        //    if (type is ServiceCollectionType serviceCollectionType)
//        //        return serviceCollectionType.ToServiceType();

//        //    return ServiceType.UNKNOWN;
//        //}

//        //public static ServiceType ToServiceType(this ServiceCollectionType type)
//        //{
//        //    switch (type)
//        //    {
//        //        case ServiceCollectionType.BARS_COLLECTION:
//        //            return ServiceType.BARS_SERVICES_COLLECTION;
//        //        default:
//        //            return ServiceType.UNKNOWN;
//        //    }
//        //}
//        //public static ServiceCollectionType ToServiceCollectionType(this ServiceType type)
//        //{
//        //    switch (type)
//        //    {
//        //        case ServiceType.BARS_SERVICES_COLLECTION:
//        //            return ServiceCollectionType.BARS_COLLECTION;
//        //        default:
//        //            throw new NotImplementedException();
//        //    }
//        //}

//    }
//}

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nt.Core.DependencyInjection.ServiceLookup
{
    /// <summary>
    /// Summary description for ServiceCallSite
    /// </summary>
    internal abstract class ServiceCallSite
    {
        protected ServiceCallSite(ResultCache cache)
        {
            Cache = cache;
        }

        public abstract Type ServiceType { get; }
        public abstract Type ImplementationType { get; }
        public abstract CallSiteKind Kind { get;}
        public ResultCache Cache { get; }
        public object Value { get; set; }

        public bool CaptureDisposable =>
            ImplementationType == null ||
            typeof(IDisposable).IsAssignableFrom(ImplementationType) ||
            typeof(IAsyncDisposable).IsAssignableFrom(ImplementationType);
    }

    internal sealed class ConstructorCallSite : ServiceCallSite
    {
        internal ConstructorInfo ConstructorInfo { get; }
        internal ServiceCallSite[] ParameterCallSites { get; }

        public ConstructorCallSite(
            ResultCache cache,
            Type serviceType, 
            ConstructorInfo constructorInfo) 
            : this(cache, serviceType, constructorInfo, Array.Empty<ServiceCallSite>())
        {
        }

        public ConstructorCallSite(
            ResultCache cache,
            Type serviceType,
            ConstructorInfo constructorInfo,
            ServiceCallSite[] parameters) 
            : base(cache)
        {
            if (!serviceType.IsAssignableFrom(constructorInfo.DeclaringType))
            {
                throw new ArgumentException("Implementation PeriodType Cant Be Converted To Service PeriodType, the constructor type is not assignable from serviceType");
            }

            ServiceType = serviceType;
            ConstructorInfo = constructorInfo;
            ParameterCallSites = parameters;
        }

        public override Type ServiceType { get; }
        public override Type ImplementationType => ConstructorInfo.DeclaringType;
        public override CallSiteKind Kind { get; } = CallSiteKind.Constructor;

    }
    internal sealed class IEnumerableCallSite : ServiceCallSite
    {
        internal Type ItemType { get; }
        internal ServiceCallSite[] ServiceCallSites { get; }

        public IEnumerableCallSite(
            ResultCache cache,
            Type itemType, 
            ServiceCallSite[] serviceCallSites) 
            : base(cache)
        {
            ItemType = itemType;
            ServiceCallSites = serviceCallSites;
        }

        public override Type ServiceType => typeof(IEnumerable<>).MakeGenericType(ItemType);
        public override Type ImplementationType => ItemType.MakeArrayType();
        public override CallSiteKind Kind { get; } = CallSiteKind.IEnumerable;
    }
    internal sealed class ConstantCallSite : ServiceCallSite
    {
        private readonly Type _serviceType;
        internal object DefaultValue => Value;

        public ConstantCallSite(
            Type serviceType, 
            object defaultValue) 
            : base(ResultCache.None)
        {
            _serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            if (defaultValue != null && !serviceType.IsInstanceOfType(defaultValue))
            {
                throw new ArgumentException("Constant Cant Be Converted To Service PeriodType, defaultValue.GetType(), serviceType");
            }

            Value = defaultValue;
        }

        public override Type ServiceType => _serviceType;
        public override Type ImplementationType => DefaultValue?.GetType() ?? _serviceType;
        public override CallSiteKind Kind { get; } = CallSiteKind.Constant;
    }
    internal sealed class ServiceProviderCallSite : ServiceCallSite
    {
        public ServiceProviderCallSite() : base(ResultCache.None)
        {
        }

        public override Type ServiceType { get; } = typeof(IServiceProvider);
        public override Type ImplementationType { get; } = typeof(ServiceProvider);
        public override CallSiteKind Kind { get; } = CallSiteKind.ServiceProvider;
    }
    internal sealed class FactoryCallSite : ServiceCallSite
    {
        public Func<IServiceProvider, object> Factory { get; }

        public FactoryCallSite(
            ResultCache cache,
            Type serviceType, 
            Func<IServiceProvider, object> factory) 
            : base(cache)
        {
            Factory = factory;
            ServiceType = serviceType;
        }

        public override Type ServiceType { get; }
        public override Type ImplementationType => null;
        public override CallSiteKind Kind { get; } = CallSiteKind.Factory;
    }

}
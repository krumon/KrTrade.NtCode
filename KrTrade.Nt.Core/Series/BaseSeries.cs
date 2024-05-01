//using KrTrade.Nt.Core.Collections;
//using KrTrade.Nt.Core.Info;
//using KrTrade.Nt.Core.Series;
//using System;

//namespace KrTrade.Nt.Services.Series
//{
//    public abstract class BaseSeries<T> : Series<T>, IBaseSeries<T>, IHasKeyInfo, IKeyItem
//    {

//        /// <summary>
//        /// Create <see cref="BaseSeries{T}"/> default instance with specified information.
//        /// </summary>
//        protected BaseSeries(IKeyInfo info) : base()
//        {
//            Info = info ?? throw new ArgumentNullException(nameof(info)); 
//        }

//        public string Name => Info.Name;
//        public bool Equals(IHasKey other) => other is IHasKey key && Key == key.Key;

//        public string Key => Info.Key;
//        public IKeyInfo Info { get; set; }

//        public abstract bool Add();
//        public abstract bool Update();

//    }
//}

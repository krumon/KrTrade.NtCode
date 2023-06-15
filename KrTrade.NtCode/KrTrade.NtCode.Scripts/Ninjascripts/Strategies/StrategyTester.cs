//using Nt.Core.Logging;
//using KrTrade.NtCode.Indicators;
//using KrTrade.NtCode.Ninjascripts;
//using System;

//namespace KrTrade.NtCode.Strategies
//{
//    public class StrategyTester : IConfigurable, IRecalculableOnBarUpdate, IRecalculableOnSessionChanged
//    {

//        private readonly ILogger _logger;
//        private readonly INinjascript _indicator;

//        public StrategyTester(IIndicator<StrategyTester> indicator, ILogger<StrategyTester> logger)
//        {
//            _indicator = indicator ?? throw new ArgumentNullException(nameof(indicator));
//            _logger = logger ?? throw new ArgumentNullException(nameof(indicator));
//        }

//        public bool IsConfigured => throw new NotImplementedException();

//        public void Configure()
//        {
//            _indicator.Configure();
//        }

//        public void OnBarUpdate()
//        {
//            _indicator?.OnBarUpdate();
//        }

//        public void OnSessionChanged(SessionChangedEventArgs args)
//        {
//            _indicator.OnSessionChanged(args);
//        }
//    }
//}

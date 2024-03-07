//namespace KrTrade.Nt.Services
//{
//    public abstract class DoubleCalculateSeries<TInput> : DoubleSeries<TInput>, ICalculateSeries
//    {
//        public int Period {  get; protected set; }
        
//        protected DoubleCalculateSeries(TInput input, int period = 1, int capacity = 20, int oldValuesCapacity = 1, int barsIndex = 0) : base(input, capacity, oldValuesCapacity, barsIndex)
//        {
//            Period = period > capacity ? capacity : period < 1 ? 1 : period;
//        }

//    }
//}

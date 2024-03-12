using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Cache to store the lastest market range price.
    /// </summary>
    public class RangeSeries : IndicatorSeries<MaxSeries,MinSeries>
    {

        //protected IIndicatorSeries _max;
        //protected IIndicatorSeries _min;

        ///// <summary>
        ///// Create <see cref="RangeSeries"/> default instance with specified properties.
        ///// </summary>
        ///// <param name="input">The <see cref="IBarsSeries"/> instance used to calculate the <see cref="RangeSeries"/>.</param>
        ///// <param name="name">The short name of indicator series.</param>
        ///// <param name="period">The specified period to calculate values in cache.</param>
        ///// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        ///// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        ///// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        ///// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        //public RangeSeries(IBarsSeries input, string name, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(input, name, period, capacity, oldValuesCapacity, barsIndex)
        //{
        //}

        /// <summary>
        /// Create <see cref="RangeSeries"/> default instance with specified properties.
        /// </summary>
        /// <param name="input">The <see cref="IBarsSeries"/> instance used to calculate the <see cref="RangeSeries"/>.</param>
        /// <param name="name">The short name of indicator series.</param>
        /// <param name="period">The specified period to calculate values in cache.</param>
        /// <param name="capacity">The series capacity. When pass a number minor or equal than 0, the capacity will be the DEFAULT(20).</param>
        /// <param name="oldValuesCapacity">The length of the removed values cache. This values are at the end of cache.</param>
        /// <param name="barsIndex">The index of NinjaScript.Bars used to gets cache elements.</param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="input"/> cannot be null.</exception>
        public RangeSeries(MaxSeries maxSeries, MinSeries minSeries, string name, int period, int capacity, int oldValuesCapacity, int barsIndex) : base(maxSeries, minSeries, name, period, capacity, oldValuesCapacity, barsIndex)
        {

            //_max = maxSeries ?? throw new ArgumentNullException(nameof(maxSeries));
            //_min = minSeries ?? throw new ArgumentNullException(nameof(minSeries));
            //if (_max.Period != _min.Period)
            //    throw new Exception("Los indicadores 'MAX' y 'MIN' deben tener el mismo periodo.");
            //Period = _max.Period;
        }

        public override INumericSeries<double> GetInput(object input)
        {            
            if (input is IBarsSeries series)
            {
                // TODO: Comprobar que en IBarsService existen los indicadores. Si existen los uso,
                //       en caso contrario, los creo y los añado a IBarsService antes de añadir este
                //       indicador para asegurarnos que tanto MAX como MIN son ejecutados antes de
                //       que este indicador se ejecute.
                return series;
            }

            return null;
        }
        protected override double GetCandidateValue(int barsAgo, bool isCandidateValueForUpdate)
            => Input1[0] - Input2[0];

        protected override double GetInitValuePreviousRecalculate() 
            => 0;

        protected override bool CheckAddConditions(double currentValue, double candidateValue)
            => candidateValue >= currentValue; 

        protected override bool CheckUpdateConditions(double currentValue, double candidateValue)
            => candidateValue >= currentValue;

    }
}

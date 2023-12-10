using System;

namespace KrTrade.Nt.Services
{
    public class CacheOptions : NinjascriptServiceOptions
    {

        private int _capacity;
        private int _displacement;

        /// <summary>
        /// Tha maximum cache capacity.
        /// </summary>
        public const int MaxCapacity = 255;

        /// <summary>
        /// Represents the cache capacity.
        /// </summary>
        public int Capacity 
        {
            get => _capacity;
            set
            {
                if (_capacity == value) return;

                if (value <= 0)
                    throw new ArgumentOutOfRangeException("The cache capacity must be greater than 0.");
                if (value > MaxCapacity)
                    throw new ArgumentOutOfRangeException("The cache capacity must be minor than 'NinjaScript.MaximumBarsLookUp'(256).");
                _capacity = value;
            }
        }

        /// <summary>
        /// Represents the next value displacement in NinjaScript Series.
        /// </summary>
        public int Displacement
        {
            get => _capacity;
            set
            {
                if (_displacement == value) return;

                if (_displacement < 0)
                    throw new ArgumentOutOfRangeException("The displacement must be greater or equal to 0.");
                if (value > MaxCapacity)
                    throw new ArgumentOutOfRangeException("The displacement must be minor than 'NinjaScript.MaximumBarsLookUp'(256).");

                _displacement = value;
            }
}
    }
}

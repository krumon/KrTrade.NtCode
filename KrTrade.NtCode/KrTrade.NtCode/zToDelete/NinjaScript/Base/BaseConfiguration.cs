using KrTrade.NtCode.Helpers;
using NinjaTrader.NinjaScript;
using KrTrade.NtCode.Events;
using System.Collections.Generic;

namespace KrTrade.NtCode
{

    /// <summary>
    /// The base class for any ninjascripts configuration.
    /// </summary>
    public abstract class BaseConfiguration<TConfiguration> : IConfiguration
        where TConfiguration : BaseConfiguration<TConfiguration>
    {

        #region Private members / Default values

        /// <summary>
        /// The element order.
        /// </summary>
        private readonly Dictionary<EventType, int> orders;

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        private string description = @"Script created by kRuMoN.";

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        private string name = "kRuMoN Script";

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        private Calculate calculate = Calculate.OnBarClose;

        /// <summary>
        /// Represents the minimum bars required to plot.
        /// </summary>
        private int barsRequiredToPlot = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Represents the ninjascript description.
        /// </summary>
        public string Description 
        { 
            get => description; 
            set 
            {
                // make sure value changes
                if (description == value)
                    return;

                if (string.IsNullOrEmpty(value))
                    description = @"Script created by kRuMoN.";
                else
                    description = value; 
            } 
        }

        /// <summary>
        /// Represents the ninjascript name.
        /// </summary>
        public string Name 
        { 
            get => name; 
            set 
            {
                // make sure value changes
                if (name == value)
                    return;

                if (string.IsNullOrEmpty(value))
                    name = "kRuMoN Script";
                else
                    name = value; 
            } 
        }

        /// <summary>
        /// Represents the ninjascript calculate mode.
        /// </summary>
        public Calculate Calculate { get { return calculate; } set { calculate = value; } }

        /// <summary>
        /// Represents the minimum bars required to plot.
        /// </summary>
        public int BarsRequiredToPlot { get { return barsRequiredToPlot; } set { barsRequiredToPlot = value; } }

        /// <summary>
        /// Inidicates if allow more than one element in the manager collection.
        /// </summary>
        public bool AllowManagerMultiUse { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create default <see cref="BaseConfiguration{TOptions}"/> instance.
        /// </summary>
        public BaseConfiguration()
        {
            // If orders is null Initialize the orders dictionary.
            if (orders == null)
            {
                orders = new Dictionary<EventType, int>();
                EnumHelpers.ForEach<EventType>((t) => orders.Add(t, 99));
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Sets order to an event driven method.
        /// </summary>
        /// <param name="eventType">The event type.</param>
        /// <param name="order">The ninjascript order in the event driven meythod.</param>
        public void AddOrder(EventType eventType, int order)
        {
            orders[eventType] = order;
        }

        /// <summary>
        /// Gets an order for an event driven method.
        /// </summary>
        /// <param name="eventType">The event type.</param>
        /// <returns>The ninjascript order in the event driven method.</returns>
        public int GetOrder(EventType eventType)
        {
            return orders[eventType];
        }

        #endregion
    }
}

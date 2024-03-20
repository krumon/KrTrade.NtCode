namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and methods that are necesary to create an indicator series service.
    /// </summary>
    public interface IIndicatorSeriesService : ISeriesService<IIndicatorSeries>, IHasNumericCalculateValues<double>
    {
        /// <summary>
        /// Gets the element of a sepecific index.
        /// </summary>
        /// <param name="index">The specific index.</param>
        /// <returns>Series element located at specified index.</returns>
        new double this[int index] { get; }

    }
}

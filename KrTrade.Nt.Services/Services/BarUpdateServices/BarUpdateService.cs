namespace KrTrade.Nt.Services
{
    public abstract class BarUpdateService<TOptions> : NinjascriptService<TOptions>, IBarUpdateService
    where TOptions : NinjascriptServiceOptions, new()
    {
        private readonly IDataSeriesService _dataSeriesService;

        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <see cref="IDataSeriesService"/> necesary for the <see cref="BarService"/>.
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        public BarUpdateService(IDataSeriesService dataSeriesService) : base(dataSeriesService?.Ninjascript, dataSeriesService?.PrintService)
        {
            _dataSeriesService = dataSeriesService;
        }

        /// <summary>
        /// Create <see cref="BarService"/> instance and configure it.
        /// </summary>
        /// <see cref="IDataSeriesService"/> necesary for the <see cref="BarService"/>.
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        /// <param name="configureOptions">The configure options of the service.</param>
        /// <exception cref="ArgumentNullException">The <see cref="IDataSeriesService"/> cannot be null.</exception>
        public BarUpdateService(IDataSeriesService dataSeriesService, IConfigureOptions<TOptions> configureOptions) : base(dataSeriesService?.Ninjascript, dataSeriesService?.PrintService, configureOptions)
        {
            _dataSeriesService = dataSeriesService;
        }

        public IDataSeriesService DataSeriesService => _dataSeriesService;

        public abstract void LogUpdatedState();

        public abstract void Update();
    }
}

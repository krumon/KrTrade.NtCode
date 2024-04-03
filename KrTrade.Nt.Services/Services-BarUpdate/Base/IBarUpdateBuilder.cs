using System;

namespace KrTrade.Nt.Services
{
    /// <summary>
    /// Defines properties and method to built any <see cref="IBarUpdateService{TOptions}"/> object. 
    /// </summary>
    public interface IBarUpdateBuilder<TService,TOptions,TSelf> 
        where TService : IBarUpdateService<TOptions>
        where TOptions : BarUpdateServiceOptions, new()
        where TSelf : IBarUpdateBuilder<TService,TOptions,TSelf>
    {

        /// <summary>
        /// Sets up the options for the <typeparamref name="TService"/> objects. This can be called multiple times and
        /// the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the options that will be used
        /// to construct the <typeparamref name="TService"/> object.</param>
        /// <returns>The same instance of the builder for chaining.</returns>
        TSelf ConfigureOptions(Action<TOptions> configureDelegate);

        /// <summary>
        /// Run the given actions to initialize the <typeparamref name="TService"/>. This can only be called once.
        /// </summary>
        /// <returns>An initialized <typeparamref name="TService"/>.</returns>
        /// <exception cref="InvalidOperationException">The service can only be built once.</exception>
        TService Build(IBarsManager barsService);

    }
}

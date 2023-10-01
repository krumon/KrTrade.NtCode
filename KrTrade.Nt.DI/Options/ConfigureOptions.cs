using System;

namespace KrTrade.Nt.DI.Options
{
    /// <summary>
    /// Implementation of <see cref="IConfigureOptions{TOptions}"/>.
    /// </summary>
    /// <typeparam name="TOptions">Options type being configured.</typeparam>
    public class ConfigureOptions<TOptions> : IConfigureOptions<TOptions> 
        where TOptions : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="action">The action to register.</param>
        public ConfigureOptions(Action<TOptions> action)
        {
            Action = action;
        }

        /// <summary>
        /// The configuration action.
        /// </summary>
        public Action<TOptions> Action { get; }

        /// <summary>
        /// Invokes the registered configure <see cref="Action"/>.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        public virtual void Configure(TOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            Action?.Invoke(options);
        }
    }
}

using NinjaTrader.NinjaScript;
using System;
using System.Reflection;

namespace ConsoleApp
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseBuilder<TScript, TConfiguration,TBuilder>: IBuilder
        where TScript : BaseNinjascript<TScript, TConfiguration,TBuilder>, INinjascript
        where TConfiguration : BaseConfiguration<TConfiguration>, IConfiguration
        where TBuilder : BaseBuilder<TScript,TConfiguration,TBuilder>, IBuilder
    {

        #region Protected members

        /// <summary>
        /// The script to build.
        /// </summary>
        protected TScript script;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseBuilder(INinjascript script)
        {
            // Make sure parameter is not null.
            if (script == null)
                throw new ArgumentNullException(nameof(script));

            // Update the field
            this.script = (TScript)script;

            // Make sure configuration is not null
            if (!script.IsConfigured)
            {
                TConfiguration op = Activator.CreateInstance<TConfiguration>();
                //Script.SetConfiguration(op);
                Configure(op);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <returns>The script object created by the builder.</returns>
        public INinjascript Build(NinjaScriptBase ninjascript = null)
        {
            // Set the default ninjatrader properties by reflection
            if (ninjascript != null)
                SetDefault(ninjascript);

            // Configuration method to the listeners
            Build();

            // Return the script.
            return script;
        }

        /// <summary>
        /// Configure options into the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IBuilder Configure<Script, Options>(Action<Options> options)
        {
            // Make sure options is a valid object and configure the script.
            if (typeof(Script) == typeof(TScript) && options is Action<TConfiguration> op)
            {
                // Add custom options by reflection
                //FieldInfo fieldInfo = script.GetType().GetField("configuration", BindingFlags.Instance | BindingFlags.NonPublic);
                TConfiguration actualOptions = GetScriptFieldValue("_options",script);

                op?.Invoke(actualOptions);
            }
            return (TBuilder)this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure<Script, Options>(Options options)
            where Options : IConfiguration
        {
            // Make sure options is a valid object and configure the script.
            if (typeof(Script) == typeof(TScript) && typeof(Options) == typeof(TConfiguration))
                // Add custom options by reflection
                //FieldInfo fieldInfo = script.GetType().GetField("configuration", BindingFlags.Instance | BindingFlags.NonPublic);
                //fieldInfo.SetValue(script, options);
                SetScriptFieldValue("_options", script, options);

            return (TBuilder)this;
        }

        /// <summary>
        /// Configure the ninjascript with the options passed by <see cref="Action"/> delegate.
        /// </summary>
        /// <param name="options">the options to configure the ninjascript.</param>
        /// <returns>The builder to continue construction the ninjascript.</returns>
        public TBuilder Configure(Action<TConfiguration> options) =>
            (TBuilder)Configure<TScript,TConfiguration>(options);

        /// <summary>
        /// Configure the ninjascript with the options passed by <see cref="SessionFiltersConfiguration"/> object.
        /// </summary>
        /// <param name="options">The options to configure the ninjascript.</param>
        /// <returns>The builder to continue construction the ninjascript.</returns>
        public TBuilder Configure(TConfiguration options) =>
            (TBuilder)Configure<TScript, TConfiguration>(options);

        #endregion

        #region Private methods

        /// <summary>
        /// Driven method to construct the ninjascript object.
        /// </summary>
        protected virtual void Build() { }

        /// <summary>
        /// Gets the configuration value from Ninhascript by reflection.
        /// </summary>
        /// <param name="script">The script configuration to gets.</param>
        /// <returns>The script configuration.</returns>
        protected TConfiguration GetScriptFieldValue(string name, TScript script)
        {
            FieldInfo fieldInfo = script.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return (TConfiguration)fieldInfo.GetValue(script);
        }

        /// <summary>
        /// Sets the configuration value in Ninjascript by reflection.
        /// </summary>
        /// <param name="script">The script where sets the value.</param>
        /// <param name="options">The configuration to sets.</param>
        protected void SetScriptFieldValue(string name, TScript script, IConfiguration options)
        {
            FieldInfo fieldInfo = script.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(script, options);
        }

        /// <summary>
        /// Sets the NinjaTrader.NinjaScript properties by reflection.
        /// </summary>
        /// <param name="ninjascript"></param>
        private void SetDefault(NinjaScriptBase ninjascript) =>
            script.GetType().GetMethod("SetDefault", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(null, new object[] { ninjascript });

        #endregion

    }
}

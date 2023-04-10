using System.Collections.Generic;
using System;
using NinjaTrader.NinjaScript;
using Nt.Scripts.Services;
using Nt.Scripts.NinjatraderObjects;

namespace Nt.Scripts.Ninjascripts.Internal
{
    internal class Ninjascript : INinjascript
    {
        private readonly INinjaScriptBase _script;
        private bool _isDisposed;
        private bool _isConfigured;

        public NinjascriptInfo[] Ninjascripts { get; set; }
        public NinjascriptConfig[] ConfigureNinjascripts { get; set; }

        public bool IsConfigured => _isConfigured;

        //private Dictionary<Type, List<NinjascriptConfig>> _config = new Dictionary<Type, List<NinjascriptConfig>>();

        public void Configure()
        {
            if (!_isDisposed)
            {
                //TODO: Configure all services

                _isConfigured = true;
            }
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                // TODO: Dispose the ninjascripts

                _isDisposed = true;
            }
        }

        public void Calculate()
        {
            var state = _script.State;
            NinjascriptLevel level = state.ToNinjascriptLevel();

            NinjascriptConfig[] ninjascripts = ConfigureNinjascripts;
            if (ninjascripts == null)
                return;

            List<Exception> exceptions = null;
            for (int i = 0; i < ninjascripts.Length; i++)
            {
                ref readonly NinjascriptConfig ninjascriptConfig = ref ninjascripts[i];
                if (!ninjascriptConfig.IsEnabled(level))
                    continue;

                NinjascriptCalculate(state, ninjascriptConfig.Ninjascript, ref exceptions);
            }

            if (exceptions != null && exceptions.Count > 0)
            {
                ThrowNinjascriptError(exceptions);
            }
        }
        public bool IsEnabled(NinjascriptLevel ninjascriptLevel)
        {
            NinjascriptConfig[] ninjascripts = ConfigureNinjascripts;
            if (ninjascripts == null)
            {
                return false;
            }

            List<Exception> exceptions = null;
            int i = 0;
            for (; i < ninjascripts.Length; i++)
            {
                ref readonly NinjascriptConfig ninjascriptConfig = ref ninjascripts[i];
                if (!ninjascriptConfig.IsEnabled(ninjascriptLevel))
                {
                    continue;
                }

                if (NinjascriptIsEnabled(ninjascriptLevel, ninjascriptConfig.Ninjascript, ref exceptions))
                {
                    break;
                }
            }

            if (exceptions != null && exceptions.Count > 0)
            {
                ThrowNinjascriptError(exceptions);
            }

            return i < ninjascripts.Length;

        }

        private static void ThrowNinjascriptError(List<Exception> exceptions)
        {
            throw new AggregateException(
                message: "An error occurred while calculate the ninjascript(s).", innerExceptions: exceptions);
        }

        private static void NinjascriptCalculate(State state, INinjascript ninjascript, ref List<Exception> exceptions)
        {
            if (state == State.Configure)
            {
                // Configure the ninjascript and make sure the ninjascript is configure.
            }

            try
            {
                ninjascript.Calculate();
            }
            catch (Exception ex)
            {
                if (exceptions == null)
                {
                    exceptions = new List<Exception>();
                }

                exceptions.Add(ex);
            }
        }

        private static bool NinjascriptIsEnabled(NinjascriptLevel ninjascriptLevel, INinjascript ninjascript, ref List<Exception> exceptions)
        {
            try
            {
                if (ninjascript.IsEnabled(ninjascriptLevel))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (exceptions == null)
                {
                    exceptions = new List<Exception>();
                }

                exceptions.Add(ex);
            }

            return false;
        }
    }
}

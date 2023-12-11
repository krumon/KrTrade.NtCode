using System;

namespace KrTrade.Nt.Services
{
    internal static class LoggingHelpers
    {
        #region OutOfRunninStates

        private static string OutOfRunningStatesToString(string name)
        {
            string serviceName = string.IsNullOrEmpty(name) ? "service" : name;
            serviceName = serviceName.Trim();
            if (serviceName.EndsWith("Service"))
                serviceName = serviceName.Substring(0, serviceName.Length - 8);
            return $"The '{serviceName} Service' must be executed when the state is equal to 'Historical' or 'Realtime'.";
        }
        public static void LogOutOfRunningStatesException(IPrintService printService, string name)
        {
            printService?.LogException(ThrowOutOfRunningStatesException(name));
        }
        public static Exception ThrowOutOfRunningStatesException(string name)
        {
            throw new Exception(OutOfRunningStatesToString(name));
        }

        #endregion

        #region IsNotConfigure

        private static string IsNotConfigureToString(string name)
        {
            string serviceName = string.IsNullOrEmpty(name) ? "service" : name;
            serviceName = serviceName.Trim();
            if (serviceName.EndsWith("Service"))
                serviceName = serviceName.Substring(0, serviceName.Length - 8);
            return $"The '{serviceName} Service' must be configured when the state is equal to 'Configure' or 'DataLoaded'.";
        }
        public static void LogIsNotConfigureException(IPrintService printService, string name)
        {
            printService?.LogException(ThrowIsNotConfigureException(name));
        }
        public static Exception ThrowIsNotConfigureException(string name)
        {
            throw new Exception(IsNotConfigureToString(name));
        }

        #endregion

        #region OutOfConfigureState

        private static string OutOfConfigureStateToString(string name)
        {
            string serviceName = string.IsNullOrEmpty(name) ? "service" : name;
            serviceName = serviceName.Trim();
            if (serviceName.EndsWith("Service"))
                serviceName = serviceName.Substring(0, serviceName.Length - 8);
            return $"The '{serviceName} Service' must be configured when the state is equal to 'Configure'.";
        }
        public static void LogOutOfConfigureStateException(IPrintService printService, string name)
        {
            printService?.LogException(ThrowOutOfConfigureStateException(name));
        }
        public static Exception ThrowOutOfConfigureStateException(string name)
        {
            throw new Exception(OutOfConfigureStateToString(name));
        }

        #endregion

        #region OutOfDataLoadedState

        private static string OutOfDataLoadedStateToString(string name)
        {
            string serviceName = string.IsNullOrEmpty(name) ? "service" : name;
            serviceName = serviceName.Trim();
            if (serviceName.EndsWith("Service"))
                serviceName = serviceName.Substring(0, serviceName.Length - 8);
            return $"The '{serviceName} Service' must be configured when the state is equal to 'DataLoaded'.";
        }
        public static void LogOutOfDataLoadedStateException(IPrintService printService, string name)
        {
            printService?.LogException(ThrowOutOfDataLoadedStateException(name));
        }
        public static void LogOutOfDataLoadedStateError(IPrintService printService, string name)
        {
            printService?.LogError(OutOfDataLoadedStateToString(name));
        }
        public static Exception ThrowOutOfDataLoadedStateException(string name)
        {
            throw new Exception(OutOfDataLoadedStateToString(name));
        }

        #endregion

        #region NinjascriptIndexesAvailable

        private static string NotAvailableNinjaScriptIndexToString(string indexName, int indexValue, string serviceName)
        {
            string name = string.IsNullOrEmpty(serviceName) ? "service" : serviceName;
            name = name.Trim();
            if (name.EndsWith("Service"))
                name = name.Substring(0, name.Length - 8);
            return $"The '{name} Service' cannot be updated because the '{indexName}' index is out of range, the wrong value is {indexValue}.";
        }
        public static void LogNotAvailableNinjaScriptIndexException(IPrintService printService, string indexName, int indexValue, string serviceName)
        {
            printService?.LogException(ThrowNotAvailableNinjaScriptIndexException(indexName,indexValue,serviceName));
        }
        public static void LogNotAvailableNinjaScriptIndexError(IPrintService printService, string indexName, int indexValue, string serviceName)
        {
            printService?.LogError(NotAvailableNinjaScriptIndexToString(indexName,indexValue,serviceName));
        }
        public static Exception ThrowNotAvailableNinjaScriptIndexException(string indexName, int indexValue, string serviceName)
        {
            throw new Exception(NotAvailableNinjaScriptIndexToString(indexName, indexValue, serviceName));
        }

        #endregion

    }
}

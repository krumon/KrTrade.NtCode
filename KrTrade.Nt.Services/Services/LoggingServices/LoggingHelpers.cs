using System;

namespace KrTrade.Nt.Services
{
    internal static class LoggingHelpers
    {
        #region OutOfRunninStates

        private static string OutOfRunningStatesText(string name)
        {
            string serviceName = string.IsNullOrEmpty(name) ? "service" : name;
            serviceName = serviceName.Trim();
            if (serviceName.EndsWith("Service"))
                serviceName = serviceName.Substring(0, serviceName.Length - 8);
            return $"The '{serviceName} Service' must be executed when the state is equal to 'Historical' or 'Realtime'.";
        }
        public static void OutOfRunningStatesException(IPrintService printService, string name)
        {
            printService?.LogException(OutOfRunningStatesException(name));
        }
        public static Exception OutOfRunningStatesException(string name)
        {
            throw new Exception(OutOfRunningStatesText(name));
        }

        #endregion

        #region OutOfConfigurationStates

        private static string OutOfConfigurationStatesText(string name)
        {
            string serviceName = string.IsNullOrEmpty(name) ? "service" : name;
            serviceName = serviceName.Trim();
            if (serviceName.EndsWith("Service"))
                serviceName = serviceName.Substring(0, serviceName.Length - 8);
            return $"The '{serviceName} Service' must be configured when the state is equal to 'Configure' or 'DataLoaded'.";
        }
        public static void OutOfConfigurationStatesException(IPrintService printService, string name)
        {
            printService?.LogException(OutOfConfigurationStatesException(name));
        }
        public static Exception OutOfConfigurationStatesException(string name)
        {
            throw new Exception(OutOfConfigurationStatesText(name));
        }

        #endregion

        #region OutOfConfigureState

        private static string OutOfConfigureStateText(string name)
        {
            string serviceName = string.IsNullOrEmpty(name) ? "service" : name;
            serviceName = serviceName.Trim();
            if (serviceName.EndsWith("Service"))
                serviceName = serviceName.Substring(0, serviceName.Length - 8);
            return $"The '{serviceName} Service' must be configured when the state is equal to 'Configure'.";
        }
        public static void OutOfConfigureStateException(IPrintService printService, string name)
        {
            printService?.LogException(OutOfConfigureStateException(name));
        }
        public static Exception OutOfConfigureStateException(string name)
        {
            throw new Exception(OutOfConfigureStateText(name));
        }

        #endregion

        #region OutOfDataLoadedState

        private static string OutOfDataLoadedStateText(string name)
        {
            string serviceName = string.IsNullOrEmpty(name) ? "service" : name;
            serviceName = serviceName.Trim();
            if (serviceName.EndsWith("Service"))
                serviceName = serviceName.Substring(0, serviceName.Length - 8);
            return $"The '{serviceName} Service' must be configured when the state is equal to 'DataLoaded'.";
        }
        public static void OutOfDataLoadedStateException(IPrintService printService, string name)
        {
            printService?.LogException(OutOfDataLoadedStateException(name));
        }
        public static void OutOfDataLoadedStateError(IPrintService printService, string name)
        {
            printService?.LogError(OutOfDataLoadedStateText(name));
        }
        public static Exception OutOfDataLoadedStateException(string name)
        {
            throw new Exception(OutOfDataLoadedStateText(name));
        }

        #endregion

        #region NinjascriptIndexesAvailable

        private static string NotAvailableNinjaScriptIndexText(string indexName, int indexValue, string serviceName)
        {
            string name = string.IsNullOrEmpty(serviceName) ? "service" : serviceName;
            name = name.Trim();
            if (name.EndsWith("Service"))
                name = name.Substring(0, name.Length - 8);
            return $"The '{name} Service' cannot be updated because the '{indexName}' index is out of range, the value is {indexValue}.";
        }
        public static void NotAvailableNinjaScriptIndexException(IPrintService printService, string indexName, int indexValue, string serviceName)
        {
            printService?.LogException(NotAvailableNinjaScriptIndexException(indexName,indexValue,serviceName));
        }
        public static void NotAvailableNinjaScriptIndexError(IPrintService printService, string indexName, int indexValue, string serviceName)
        {
            printService?.LogError(NotAvailableNinjaScriptIndexText(indexName,indexValue,serviceName));
        }
        public static Exception NotAvailableNinjaScriptIndexException(string indexName, int indexValue, string serviceName)
        {
            throw new Exception(NotAvailableNinjaScriptIndexText(indexName, indexValue, serviceName));
        }

        #endregion

    }
}

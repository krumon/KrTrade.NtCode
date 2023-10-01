namespace KrTrade.Nt.Scripts.Configuration
{
    /// <summary>
    /// Helpers methods to configure ninjascript services.
    /// </summary>
    public static class NinjascriptConfigHelpers
    {

        /// <summary>
        /// Gets the section name from key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLastSection(this string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            string[] sections = key.Split(':');

            return sections[sections.Length-1];
        }
    }
}

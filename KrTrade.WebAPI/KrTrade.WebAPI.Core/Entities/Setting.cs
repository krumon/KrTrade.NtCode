namespace KrTrade.WebApp.Core.Entities
{
    public class Setting
    {
        /// <summary>
        /// The unique Id for this entry
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The settings name
        /// </summary>
        /// <remarks>This column is indexed</remarks>
        public string Name { get; set; }

        /// <summary>
        /// The settings value
        /// </summary>
        public string Value { get; set; }
    }
}

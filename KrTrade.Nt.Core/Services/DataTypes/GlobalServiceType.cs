namespace KrTrade.Nt.Core.Services
{
    /// <summary>
    /// Represents the generic services types.
    /// </summary>
    public enum GlobalServiceType
    { 
   
        /// <summary>
        /// Print service for print in Ninjatrade output window.
        /// </summary>
        Print,

        /// <summary>
        /// File service for use files in the NinjaScript.
        /// </summary>
        File,

        /// <summary>
        /// Logging service.
        /// </summary>
        Log,

        /// <summary>
        /// Data base service to import or export data.
        /// </summary>
        DB,

        /// <summary>
        /// Drawing service for draw in the chart window.
        /// </summary>
        Drawing

    }
}

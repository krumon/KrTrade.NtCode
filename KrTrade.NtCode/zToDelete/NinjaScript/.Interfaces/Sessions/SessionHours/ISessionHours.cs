namespace KrTrade.Nt.Console
{

    /// <summary>
    /// Interface for any session hours.
    /// </summary>
    public interface ISessionHours : ISession
    {

        /// <summary>
        /// Creates the <see cref=ISessionHoursBuilder"/> to construct the <see cref="ISessionHours"/> object.
        /// </summary>
        /// <returns>The <see cref="ISessionHoursBuilder"/> to construct the <see cref="ISessionHours"/> object.</returns>
        ISessionHoursBuilder CreateSessionHoursBuilder();

    }
}

namespace KrTrade.Nt.Console
{

    /// <summary>
    /// Interface for any session stats.
    /// </summary>
    public interface ISessionStats : ISession
    {

        /// <summary>
        /// Creates the <see cref=ISessionStatsBuilder"/> to construct the <see cref="ISessionStats"/> object.
        /// </summary>
        /// <returns>The <see cref="ISessionStatsBuilder"/> to construct the <see cref="ISessionStats"/> object.</returns>
        ISessionStatsBuilder CreateSessionStatsBuilder();

    }
}

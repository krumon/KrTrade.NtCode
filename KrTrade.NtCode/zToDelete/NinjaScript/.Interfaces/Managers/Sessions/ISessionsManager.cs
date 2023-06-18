namespace KrTrade.Nt.Console
{

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManager : IManager
    {

        /// <summary>
        /// Creates the <see cref="SessionsManagerBuilder"/> to construct the <see cref="SessionsManager"/> object.
        /// </summary>
        /// <returns>The <see cref="SessionsManagerBuilder"/> to construct the <see cref="SessionsManager"/> object.</returns>
        ISessionsManagerBuilder CreateSessionsManagerBuilder();

    }

}

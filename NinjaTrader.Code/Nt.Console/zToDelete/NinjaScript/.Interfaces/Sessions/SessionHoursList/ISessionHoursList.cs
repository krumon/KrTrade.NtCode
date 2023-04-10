namespace ConsoleApp
{

    /// <summary>
    /// Interface for any session hours list.
    /// </summary>
    public interface ISessionHoursList : ISession
    {

        /// <summary>
        /// Creates the <see cref=ISessionHoursListBuilder"/> to construct the <see cref="ISessionHoursList"/> object.
        /// </summary>
        /// <returns>The <see cref="ISessionHoursListBuilder"/> to construct the <see cref="ISessionHoursList"/> object.</returns>
        ISessionHoursListBuilder CreateSessionHoursListBuilder();

    }
}

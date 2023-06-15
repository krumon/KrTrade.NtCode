namespace Nt.Core
{
    public interface ISession : IScript
    {
        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        void OnSessionChanged(SessionChangedEventArgs e);
    }
}

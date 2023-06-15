using Nt.Core.Events;

namespace ConsoleApp
{

    /// <summary>
    /// Interface for any session script.
    /// </summary>
    public interface ISession : INinjascript
    {
        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        void OnSessionChanged(SessionUpdateArgs e);

    }

}

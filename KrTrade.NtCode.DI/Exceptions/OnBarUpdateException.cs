using System;

namespace KrTrade.Nt.DI.Exceptions
{
    [Serializable]
    public class OnBarUpdateException : Exception
    {
        #region MyRegion

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="LoadException"/> default instance.
        /// </summary>
        public OnBarUpdateException(){ }

        /// <summary>
        /// Creates a <see cref="LoadException"/> with a default message instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        public OnBarUpdateException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a <see cref="LoadException"/> with a default message and inner exception instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        /// <param name="inner">The inner exception taht produce the error.</param>
        public OnBarUpdateException(string message, Exception inner) : base(message, inner)
        {
        }

        #endregion

    }
}

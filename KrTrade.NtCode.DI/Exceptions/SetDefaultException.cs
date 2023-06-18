using System;

namespace KrTrade.NtCode.Exceptions
{
    [Serializable]
    public class SetDefaultException : Exception
    {
        #region MyRegion

        /// <summary>
        /// Represents the parameter that throw a null reference exception.
        /// </summary>
        public string Parameter { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SetDefaultException"/> default instance.
        /// </summary>
        public SetDefaultException(){ }

        /// <summary>
        /// Creates a <see cref="SetDefaultException"/> with a default message instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        public SetDefaultException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a <see cref="SetDefaultException"/> with a default message and inner exception instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        /// <param name="inner">The inner exception taht produce the error.</param>
        public SetDefaultException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Creates a <see cref="SetDefaultException"/> with a default message and inner exception instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        /// <param name="inner">The inner exception taht produce the error.</param>
        /// <param name="parameter">The parameter that produce the exception.</param>
        public SetDefaultException(string message, Exception inner, string parameter) : base(message,inner)
        {
            Parameter = parameter;
        }

        #endregion

    }
}

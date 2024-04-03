using System;

namespace KrTrade.Nt.Core.Exceptions
{
    [Serializable]
    public class CreateBuilderException : Exception
    {

        #region Private members

        private readonly bool isScriptAssignable;
        private readonly bool isBuilderAssignable;

        #endregion

        #region Public Properties

        /// <summary>
        /// Represents the script that the builder going to build.
        /// </summary>
        public Type Script { get; }

        /// <summary>
        /// Represents the parent of the script thats the builder going to build.
        /// </summary>
        public Type ScriptParent { get; }

        /// <summary>
        /// Represents the builder thats going to create.
        /// </summary>
        public Type Builder { get; }

        /// <summary>
        /// Represents the parent of the builder thats going to create.
        /// </summary>
        public Type BuilderParent { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="LoadException"/> default instance.
        /// </summary>
        public CreateBuilderException() { }

        /// <summary>
        /// Creates a <see cref="LoadException"/> with a default message instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        public CreateBuilderException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a <see cref="LoadException"/> with a default message and inner exception instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        /// <param name="inner">The inner exception taht produce the error.</param>
        public CreateBuilderException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Creates a <see cref="LoadException"/> with a default message and inner exception instance.
        /// </summary>
        /// <param name="message">The default message to show.</param>
        /// <param name="script">The script thats the builder is going to create.</param>
        /// <param name="scriptParent">The script parent.</param>
        /// <param name="builder">The builder thats is going to create.</param>
        /// <param name="builderParent">The builder parent.</param>
        public CreateBuilderException(string message, Type script, Type scriptParent, Type builder, Type builderParent) : base(message)
        {
            Script = script;
            ScriptParent = scriptParent;
            Builder = builder;
            BuilderParent = builderParent;
            isScriptAssignable = Script.IsAssignableFrom(ScriptParent);
            isBuilderAssignable = Builder.IsAssignableFrom(BuilderParent);
        }

        #endregion

        #region Public methods

        public override string ToString()
        {
            string scriptText = isScriptAssignable ? string.Empty : $"The {Script} is not assignable from {ScriptParent}";
            string prep = isScriptAssignable ? "." : isBuilderAssignable ? "" : " and ";
            string builderText = isBuilderAssignable ? string.Empty : $"The {Builder} is not assignable from {BuilderParent}.";
            return $"{GetType()}: The {Builder} cannot be created. {scriptText}{prep}{builderText}";
        }


        #endregion

    }
}

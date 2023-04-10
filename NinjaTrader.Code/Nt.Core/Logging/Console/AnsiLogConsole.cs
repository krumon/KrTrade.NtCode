using System.IO;

namespace Nt.Core.Logging.Console
{
    /// <summary>
    /// For consoles which understand the ANSI escape code sequences to represent color
    /// </summary>
    internal sealed class AnsiLogConsole : IConsole
    {
        private readonly TextWriter _textWriter;

        public AnsiLogConsole(bool stdErr = false)
        {
            _textWriter = stdErr ? System.Console.Error : System.Console.Out;
        }

        public void Write(string message)
        {
            _textWriter.Write(message);
        }
    }
}

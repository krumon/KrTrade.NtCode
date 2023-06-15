using System;
using System.IO;

namespace KrTrade.NtCode.Logging.Internal
{
    /// <summary>
    /// Formatter to convert the named format items like {NamedformatItem} to <see cref="string.Format(IFormatProvider, string, object)"/> format.
    /// </summary>
    internal sealed class LogValuesSourceFormatter
    {
        private const string NullValue = "(null)";
        private readonly string _memberName;
        private readonly string _filePath;
        private readonly int _lineNumber;

        public LogValuesSourceFormatter(string memberName, string filePath, int lineNumber)
        {
            _memberName = memberName;
            _filePath = filePath;
            _lineNumber = lineNumber;
        }

        public string Format()
        {
            if (string.IsNullOrEmpty(_memberName) || string.IsNullOrEmpty(_filePath) || _lineNumber<1)
                return NullValue;
            else
                // Format the message string
                return $"{Path.GetFileName(_filePath)} > {_memberName}() > Line {_lineNumber}";
        }
    }
}

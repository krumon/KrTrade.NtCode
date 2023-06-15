using KrTrade.NtCode.Logging.Internal;

namespace KrTrade.NtCode.Logging
{
    public class SourceLogValues
    {

        private const string NullFormat = "[null]";
        private readonly LogValuesSourceFormatter _formatter;

        public string MemberName { get; private set; }
        public string FilePath { get; private set; }
        public int LineNumber { get; private set; }
        
        internal LogValuesSourceFormatter Formatter => _formatter;

        public SourceLogValues(string memberName, string filePath, int lineNumber)
        {
            MemberName = memberName;
            FilePath = filePath;
            LineNumber = lineNumber;
            _formatter = new LogValuesSourceFormatter(memberName, filePath, lineNumber);
        }

        public override string ToString()
        {
            if (_formatter == null)
            {
                return NullFormat;
            }

            return _formatter.Format();
        }

    }
}

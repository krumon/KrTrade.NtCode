using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Nt.Core.Logging.Internal
{
    /// <summary>
    /// LogValues to enable formatting options supported by <see cref="string.Format(IFormatProvider, string, object?)"/>.
    /// This also enables using {NamedformatItem} in the format string.
    /// </summary>
    internal readonly struct FormattedLogValues : IReadOnlyList<KeyValuePair<string, object>>
    {
        internal const int MaxCachedFormatters = 1024;
        private const string NullFormat = "[null]";
        private const char SourceSeparator = '|';
        private static int _count;
        private static ConcurrentDictionary<string, LogValuesFormatter> _formatters = new ConcurrentDictionary<string, LogValuesFormatter>();
        private readonly LogValuesFormatter _formatter;
        private readonly SourceLogValues _sourceLogValues;
        private readonly LogValuesSourceFormatter _sourceFormatter;
        private readonly object[] _values;
        private readonly string _originalMessage;
        private readonly string _sourceMessage;

        // for testing purposes
        internal LogValuesFormatter Formatter => _formatter;

        public FormattedLogValues(string format, params object[] values) : this(null,format,values)
        {
        }

        public FormattedLogValues(SourceLogValues sourceLogValues, string format, params object[] values)
        {
            if (values != null && values.Length != 0 && format != null)
            {
                if (_count >= MaxCachedFormatters)
                {
                    if (!_formatters.TryGetValue(format, out _formatter))
                    {
                        _formatter = new LogValuesFormatter(format);
                    }
                }
                else
                {
                    _formatter = _formatters.GetOrAdd(format, f =>
                    {
                        Interlocked.Increment(ref _count);
                        return new LogValuesFormatter(f);
                    });
                }
            }
            else
            {
                _formatter = null;
            }

            _originalMessage = format ?? NullFormat;
            _values = values;
            _sourceLogValues = sourceLogValues;
            _sourceFormatter = _sourceLogValues?.Formatter;
            if (_sourceFormatter != null)
                _sourceMessage = _sourceFormatter.Format();
            else
                _sourceMessage = string.Empty;
        }

        public KeyValuePair<string, object> this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                if (index == Count - 1)
                {
                    return new KeyValuePair<string, object>("{OriginalFormat}", _originalMessage);
                }
                //return _formatter!.GetValue(_values!, index);
                return _formatter.GetValue(_values, index);
            }
        }

        public int Count
        {
            get
            {
                if (_formatter == null)
                {
                    return 1;
                }

                return _formatter.ValueNames.Count + 1;
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return this[i];
            }
        }

        public override string ToString()
        {
            if (_formatter == null)
            {
                if (string.IsNullOrEmpty(_sourceMessage))
                    return _originalMessage;

                return _sourceMessage + SourceSeparator + _originalMessage;
            }
            if (string.IsNullOrEmpty(_sourceMessage))
                return _formatter.Format(_values);
            else
                return _sourceMessage + SourceSeparator + _formatter.Format(_values);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

using System;

namespace KrTrade.NtCode.Logging
{
    /// <summary>
    /// Flags to indicate which trace context parts should be included with the logging scopes.
    /// </summary>
    [Flags]
    public enum ActivityTrackingOptions
    {
        /// <summary>
        /// None of the trace context part wil be included in the logging.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Span Id will be included in the logging.
        /// </summary>
        SpanId = 0x1,
        /// <summary>
        /// Trace Id will be included in the logging.
        /// </summary>
        TraceId = 0x2,
        /// <summary>
        /// Parent Id will be included in the logging.
        /// </summary>
        ParentId = 0x4,
        /// <summary>
        /// Trace State will be included in the logging.
        /// </summary>
        TraceState = 0x8,
        /// <summary>
        /// Trace flags will be included in the logging.
        /// </summary>
        TraceFlags = 0x10,
        /// <summary>
        /// Tags will be included in the logging.
        /// </summary>
        Tags = 0x20,
        /// <summary>
        /// Items of baggage will be included in the logging.
        /// </summary>
        Baggage = 0x40
    }
}

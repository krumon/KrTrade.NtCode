using System;
using System.Runtime.CompilerServices;

namespace Nt.Core.Logging.Console
{
    internal sealed class AnsiParser
    {
        private readonly Action<string, int, int, ConsoleColor?, ConsoleColor?> _onParseWrite;
        public AnsiParser(Action<string, int, int, ConsoleColor?, ConsoleColor?> onParseWrite)
        {
            if (onParseWrite == null)
            {
                throw new ArgumentNullException(nameof(onParseWrite));
            }
            _onParseWrite = onParseWrite;
        }

        /// <summary>
        /// Parses a subset of display attributes
        /// Set Display Attributes
        /// Set Attribute Mode [{attr1};...;{attrn}m
        /// Sets multiple display attribute settings. The following lists standard attributes that are getting parsed:
        /// 1 Bright
        /// Foreground Colours
        /// 30 Black
        /// 31 Red
        /// 32 Green
        /// 33 Yellow
        /// 34 Blue
        /// 35 Magenta
        /// 36 Cyan
        /// 37 White
        /// Background Colours
        /// 40 Black
        /// 41 Red
        /// 42 Green
        /// 43 Yellow
        /// 44 Blue
        /// 45 Magenta
        /// 46 Cyan
        /// 47 White
        /// </summary>
        public void Parse(string message)
        {
            int startIndex = -1;
            int length = 0;
            int escapeCode;
            ConsoleColor? foreground = null;
            ConsoleColor? background = null;
            var span = message.AsSpan();
            const char EscapeChar = '\x1B';
            ConsoleColor? color = null;
            bool isBright = false;
            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] == EscapeChar && span.Length >= i + 4 && span[i + 1] == '[')
                {
                    if (span[i + 3] == 'm')
                    {
                        // Example: \x1B[1m
                        if (IsDigit(span[i + 2]))
                        {
                            escapeCode = (int)(span[i + 2] - '0');
                            if (startIndex != -1)
                            {
                                _onParseWrite(message, startIndex, length, background, foreground);
                                startIndex = -1;
                                length = 0;
                            }
                            if (escapeCode == 1)
                                isBright = true;
                            i += 3;
                            continue;
                        }
                    }
                    else if (span.Length >= i + 5 && span[i + 4] == 'm')
                    {
                        // Example: \x1B[40m
                        if (IsDigit(span[i + 2]) && IsDigit(span[i + 3]))
                        {
                            escapeCode = (int)(span[i + 2] - '0') * 10 + (int)(span[i + 3] - '0');
                            if (startIndex != -1)
                            {
                                _onParseWrite(message, startIndex, length, background, foreground);
                                startIndex = -1;
                                length = 0;
                            }
                            if (TryGetForegroundColor(escapeCode, isBright, out color))
                            {
                                foreground = color;
                                isBright = false;
                            }
                            else if (TryGetBackgroundColor(escapeCode, out color))
                            {
                                background = color;
                            }
                            i += 4;
                            continue;
                        }
                    }
                }
                if (startIndex == -1)
                {
                    startIndex = i;
                }
                int nextEscapeIndex = -1;
                if (i < message.Length - 1)
                {
                    nextEscapeIndex = message.IndexOf(EscapeChar, i + 1);
                }
                if (nextEscapeIndex < 0)
                {
                    length = message.Length - startIndex;
                    break;
                }
                length = nextEscapeIndex - startIndex;
                i = nextEscapeIndex - 1;
            }
            if (startIndex != -1)
            {
                _onParseWrite(message, startIndex, length, background, foreground);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsDigit(char c) => (uint)(c - '0') <= ('9' - '0');

        internal const string DefaultForegroundColor = "\x1B[39m\x1B[22m"; // reset to default foreground color
        internal const string DefaultBackgroundColor = "\x1B[49m"; // reset to the background color

        internal static string GetForegroundColorEscapeCode(ConsoleColor color)
        {
            switch(color)
            {
                case ConsoleColor.Black:         return "\x1B[30m";
                case ConsoleColor.DarkRed:       return "\x1B[31m";
                case ConsoleColor.DarkGreen:     return "\x1B[32m";
                case ConsoleColor.DarkYellow:    return "\x1B[33m";
                case ConsoleColor.DarkBlue:      return "\x1B[34m";
                case ConsoleColor.DarkMagenta:   return "\x1B[35m";
                case ConsoleColor.DarkCyan:      return "\x1B[36m";
                case ConsoleColor.Gray:          return "\x1B[37m";
                case ConsoleColor.Red:           return "\x1B[1m\x1B[31m";
                case ConsoleColor.Green:         return "\x1B[1m\x1B[32m";
                case ConsoleColor.Yellow:        return "\x1B[1m\x1B[33m";
                case ConsoleColor.Blue:          return "\x1B[1m\x1B[34m";
                case ConsoleColor.Magenta:       return "\x1B[1m\x1B[35m";
                case ConsoleColor.Cyan:          return "\x1B[1m\x1B[36m";
                case ConsoleColor.White:         return "\x1B[1m\x1B[37m";
                default:                         return DefaultForegroundColor; // default foreground color
            };
        }

        internal static string GetBackgroundColorEscapeCode(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black:        return "\x1B[40m";
                case ConsoleColor.DarkRed:      return "\x1B[41m";
                case ConsoleColor.DarkGreen:    return "\x1B[42m";
                case ConsoleColor.DarkYellow:   return "\x1B[43m";
                case ConsoleColor.DarkBlue:     return "\x1B[44m";
                case ConsoleColor.DarkMagenta:  return "\x1B[45m";
                case ConsoleColor.DarkCyan:     return "\x1B[46m";
                case ConsoleColor.Gray:         return "\x1B[47m";
                default: return DefaultForegroundColor; // default foreground color
            };
        }

        private static bool TryGetForegroundColor(int number, bool isBright, out ConsoleColor? color)
        {
            switch (number)
            {
                case 30: color = ConsoleColor.Black; break;
                case 31: color = isBright? ConsoleColor.Red : ConsoleColor.DarkRed; break;
                case 32: color = isBright ? ConsoleColor.Green : ConsoleColor.DarkGreen; break;
                case 33: color = isBright? ConsoleColor.Yellow: ConsoleColor.DarkYellow; break;
                case 34: color = isBright? ConsoleColor.Blue: ConsoleColor.DarkBlue; break;
                case 35: color = isBright? ConsoleColor.Magenta: ConsoleColor.DarkMagenta; break;
                case 36: color = isBright? ConsoleColor.Cyan: ConsoleColor.DarkCyan; break;
                case 37: color = isBright? ConsoleColor.White: ConsoleColor.Gray; break;
                default: color = null; break;
            }

            return color != null || number == 39;
        }

        private static bool TryGetBackgroundColor(int number, out ConsoleColor? color)
        {
            switch (number)
            {
                case 40: color = ConsoleColor.Black; break;
                case 41: color = ConsoleColor.DarkRed; break;
                case 42: color = ConsoleColor.DarkGreen; break;
                case 43: color = ConsoleColor.DarkYellow; break;
                case 44: color = ConsoleColor.DarkBlue; break;
                case 45: color = ConsoleColor.DarkMagenta; break;
                case 46: color = ConsoleColor.DarkCyan; break;
                case 47: color = ConsoleColor.Gray; break;
                default: color = null; break;
            }

            return color != null || number == 49;
        }
    }
}

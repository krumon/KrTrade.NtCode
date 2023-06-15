using Kr.Core.Helpers;
using Kr.Core.Tests;
using KrTrade.NtCode;
using KrTrade.NtCode.Internal;
using System;
using Nt.Core.Data;

namespace KrTrade.NtCode
{
    public class TradingSessionTests : BaseConsoleTests
    {

        #region Private members

        //private TradingSession ts;
        private TradingSession ts;
        private SessionType[] types = 
            new SessionType[] 
            {
                SessionType.European,
                SessionType.AmericanAndEuropean,
                SessionType.American_RS_EOD,
                SessionType.Asian,
                SessionType.Electronic
            };

        #endregion

        #region Public Properties


        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="TradingSessionTests"/> default instance.
        /// </summary>
        public TradingSessionTests()
        {

        }

        #endregion

        #region Public methods

        public override void Run()
        {
            AddTests();
            //InstanceTests();
            //WaitAndClear();
            //ToStringTests(TradingSessionType.American_RS);
            //WaitAndClear();
            //TradingSessionEnumTest();
            //WaitAndClear();
            //OperatorTests(TradingSession.Asian, TradingSession.European);
            //WaitAndClear();
        }

        #endregion

        #region Private methods

        private void InstanceTests()
        {
            //Create a custom instance.
            Title("Instance tests");
            Subtitle("Custom instance with trading session types");
            ts = TradingSession.CreateCustomTradingSession(
                InstrumentCode.MES,
                TradingTimeType.American_Open,
                TradingTimeType.Asian_Close,
                "My Custom TradingSession");
            WriteLine(ts.ToString());
            NewLine();

            Subtitle("Custom instance with Time Span, Time Zone Info and Trading Session PeriodType");
            ts = TradingSession.CreateCustomTradingSession(InstrumentCode.MES, new TimeSpan(12, 15, 0), TimeZoneInfo.Local, TradingTimeType.European_Close);
            WriteLine(ts.ToShortString("u"));
            NewLine();

            Subtitle("Instance by generic types");
            ts = TradingSession.CreateTradingSessionByType(SessionType.American, InstrumentCode.MES);
            WriteLine(ts.ToLongString("l"));
            NewLine();
        }

        private void ToStringTests(SessionType type)
        {
            // Create a trading session by type.
            TradingSession ts = TradingSession.CreateTradingSessionByType(type, InstrumentCode.MES);

            Title("Test of To String methods");
            Console.WriteLine($"Method ToString() => {ts}");
            Console.WriteLine($"Method ToShortString('u') => {ts.ToShortString("u")}");
            Console.WriteLine($"Method ToLongString('l') => {ts.ToLongString("l")}");
            Console.WriteLine($"Method ToShortString('UTC') => {ts.ToShortString("UTC")}");
            Console.WriteLine($"Method ToLongString('LOCAL') => {ts.ToLongString("LOCAL")}");
        }

        private void TradingSessionEnumTest()
        {
            Title("Test of iteration and check methods");
            EnumHelpers.Writer<SessionType>();

            NewLine();
            EnumHelpers.ForEach<SessionType>((t) =>
            {
                if (t != SessionType.Custom)
                    Console.WriteLine(t.ToSessionHours(InstrumentCode.MES).ToString());
            });

            NewLine();
            TradingSession ts = TradingSession.CreateTradingSessionByType(SessionType.American, InstrumentCode.MES);
            //bool exist = ts.Exist();

            //string s = exist ? "exist" : "don't exist";
            //Write(ts.ToString());
            //Write($"{ts.Description} {s} in {nameof(TradingTime)} enum.");

        }

        private void AddTests()
        {
            ts = TradingSession.CreateTradingSessionByType(SessionType.European, InstrumentCode.MES);
            //ts.Add(types);
        }

        private void OperatorTests(SessionType t1, SessionType t2)
        {
            TradingSession sh1 = TradingSession.CreateTradingSessionByType(t1, InstrumentCode.MES);
            TradingSession sh2 = TradingSession.CreateTradingSessionByType(t2, InstrumentCode.MES);
            int i;
            bool b;
            string s = string.Empty;
            string method = string.Empty;

            NewLine();
            Console.WriteLine($"{nameof(sh1)} = {sh1.ToString()}");
            Console.WriteLine($"{nameof(sh2)} = {sh2.ToString()}");

            #region Compare tests

            Title("Test of Compare methods.");
            i = sh1.CompareTo(sh2);
            method = "CompareTo(st1,st2) =>";

            if (i < 0)
                s = $"{method} {sh1.Code} is minor than {sh2.Code}.";
            if (i > 0)
                s = $"{method} {sh1.Code} is major than {sh2.Code}.";
            if (i == 0)
                s = $"{method} {sh1.Code} and {sh2.Code} have the same time.";
            WriteLine(s);

            i = sh1.CompareTo(sh2);
            s = string.Empty;
            method = "CompareTo(st2) =>";

            if (i < 0)
                s = $"{method} {sh1.Code} is minor than {sh2.Code}.";
            if (i > 0)
                s = $"{method} {sh1.Code} is major than {sh2.Code}.";
            if (i == 0)
                s = $"{method} {sh1.Code} and {sh2.Code} have the same time.";
            WriteLine(s);

            #endregion

            #region Equal tests

            Title("Test of Equal methods.");
            b = TradingTime.Equals(sh1, sh2);
            s = string.Empty;
            method = "Equals(st1,st2) =>";

            if (b)
                s = $"{method} {sh1.Code} and {sh2.Code} are equals.";
            else
                s = $"{method} {sh1.Code} and {sh2.Code} are not equals.";
            WriteLine(s);

            b = sh1.Equals(sh2);
            s = string.Empty;
            method = "st1.Equals(st2) =>";

            if (sh1.Equals(sh2))
                s = $"{method} {nameof(sh1)} and {nameof(sh2)} are equals.";
            else
                s = $"{method} {nameof(sh1)} and {nameof(sh2)} are not equals.";

            WriteLine(s);

            #endregion

            #region Method operators

            Title("Test of Operator methods.");
            s = string.Empty;

            if (sh1 == sh2)
                s += $"{sh1.Code} is equal to {sh2.Code}.{Environment.NewLine}";
            if (sh1 != sh2)
                s += $"{sh1.Code} is not equal to {sh2.Code}.{Environment.NewLine}";
            if (sh1 <= sh2)
                s += $"{sh1.Code} is equal to or less than {sh2.Code}.{Environment.NewLine}";
            if (sh1 >= sh2)
                s += $"{sh1.Code} is equal to or greater than {sh2.Code}.{Environment.NewLine}";
            if (sh1 < sh2)
                s += $"{sh1.Code} is less than {sh2.Code}.{Environment.NewLine}";
            if (sh1 > sh2)
                s += $"{sh1.Code} is greater than {sh2.Code}.{Environment.NewLine}";

            Console.WriteLine(s);
            Console.WriteLine($"{nameof(sh1)} + {nameof(sh2)} = {(sh1 + sh2).ToString()}");
            Console.WriteLine($"{nameof(sh1)} - {nameof(sh2)} = {(sh1 - sh2).ToString()}");

            #endregion

        }

        private void PrintSession()
        {
            while (true)
            {
                Console.WriteLine("TEST PARA IMPRIMIR POR CONSOLA UNA HORA ESPECÍFICA DE UNA SESIÓN.");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.Write("- Introduzca el código de la session( AM | EU | AE | AS | EL | RG | OVN): ");
                string session = Console.ReadLine().ToUpper().Trim();

                if (string.IsNullOrEmpty(session) || string.IsNullOrWhiteSpace(session))
                    return;

                if (session == "AM" || session == "AS")
                {
                    Console.Write("- Es una sesión residual (Y/TNinjaScript): ");
                    string isResidual = Console.ReadLine().ToUpper().Trim();

                    if (isResidual == "Y")
                    {
                        session += "-RS";

                        if (session == "AM-RS")
                        {
                            Console.Write("- Introduce el código de la sesión residual: ( EXT | EOD | NWD ): ");
                            string specificSession = Console.ReadLine().ToUpper().Trim();

                            if (specificSession == "EXT" || specificSession == "EOD" || specificSession == "NWD")
                                session += "-" + specificSession;
                        }
                    }
                }

                Console.Write("- Introduzca el momento temporal de la sesión: ( O | C ): ");
                string price = Console.ReadLine().ToUpper();

                if (price == "O" || price == "C")
                    session += "-" + price;
                else
                    return;

                Console.WriteLine();
                Console.WriteLine(String.Format("Código: {0} | {1} | {2}", session, session.ToTradingTime().ToSessionTime(InstrumentCode.MES).LocalTime.ToString(), session.ToTradingTime().ToDescription()));
                Console.ReadKey();
                Console.Clear();

            }


        }


        #endregion


    }
}

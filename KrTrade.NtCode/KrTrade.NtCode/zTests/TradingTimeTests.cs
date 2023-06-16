using KrTrade.NtCode.Tests;
using KrTrade.NtCode;
using KrTrade.NtCode.Data;

namespace KrTrade.NtCode
{
    public class TradingTimeTests : BaseConsoleTests
    {

        #region Private members

        #endregion

        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="TradingTimeTests"/> default instance.
        /// </summary>
        public TradingTimeTests()
        {
        }

        #endregion

        #region Run method

        public override void Run()
        {
            InstanceTests();
            Clear();
            ToStringTests(TradingTimeType.American_RS_EOD_Close);
            Clear();
            SessionTimeEnumTests();
            Clear();
            OperatorTests(TradingTimeType.Asian_Open, TradingTimeType.American_RS_EOD_Close);
            WaitAndClear();
        }

        #endregion

        #region Private methods

        private void InstanceTests()
        {
            //// Create a custom instance.
            //Title("Test of Instance methods.");

            //TradingTime st = TradingTime.CreateCustomSessionTime(9,15,0,TimeZoneInfo.Local);
            //Console.WriteLine($"Method ToString() => {st}");
            //Console.WriteLine($"Method ToShortString() => {st.ToShortString()}");
            //Console.WriteLine($"Method ToLongString() => {st.ToLongString()}");

            //st = TradingTime.CreateCustomSessionTime(new TimeSpan(9,0,0),TimeZoneInfo.Local);
            //NewLine();
            //Console.WriteLine($"Method ToString() => {st}");
            //Console.WriteLine($"Method ToShortString() => {st.ToShortString()}");
            //Console.WriteLine($"Method ToLongString() => {st.ToLongString()}");

            //// Create instance by type
            //st = TradingTime.CreateSessionTimeByType(TradingTimeType.European_Open);
            //NewLine();
            //Console.WriteLine($"Method ToString() => {st}");
            //Console.WriteLine($"Method ToShortString() => {st.ToShortString()}");
            //Console.WriteLine($"Method ToLongString() => {st.ToLongString()}");

        }

        private void ToStringTests(TradingTimeType type)
        {
            //// Create a session time by type.
            //TradingTime st = TradingTime.CreateSessionTimeByType(type);

            //Title("Test of To String methods.");
            //Console.WriteLine(st.ToString());
            //Console.WriteLine(st.ToShortString());
            //Console.WriteLine(st.ToLongString());
            //Console.WriteLine(st.ToString("LOCAL"));
            //Console.WriteLine(st.ToString("UTC"));
        }

        private void SessionTimeEnumTests()
        {
            //Title("Test of iteration and check methods.");
            //EnumHelpers.Writer<TradingTimeType>();

            //NewLine();
            //EnumHelpers.ForEach<TradingTimeType>((t) =>
            //{
            //    if (t != TradingTimeType.Custom)
            //        Console.WriteLine(t.ToSessionTime().ToString("Local"));
            //});

            //NewLine();
            //TradingTimeType tt = TradingTimeType.American_Open;
            //bool exist = TradingTime.Exist(tt);

            //string s = exist ? "exist" : "don't exist";
            //WriteLine(tt.ToString());
            //WriteLine($"{tt} {s} in {nameof(TradingTimeType)} enum.");

        }

        private void OperatorTests(TradingTimeType t1, TradingTimeType t2)
        {
            //TradingTime st1 = TradingTime.CreateSessionTimeByType(t1);
            //TradingTime st2 = TradingTime.CreateSessionTimeByType(t2);
            //int i;
            //bool b;
            //string s = string.Empty;
            //string method = string.Empty;
            
            //NewLine();
            //Console.WriteLine($"{nameof(st1)} = {st1.ToString("local")}");
            //Console.WriteLine($"{nameof(st2)} = {st2.ToString("local")}");

            //#region Compare tests
            
            //Title("Test of Compare methods.");
            //i = TradingTime.Compare(st1,st2);
            //method = "CompareTo(st1,st2) =>";

            //if (i < 0)
            //    s = $"{method} {st1.Code} is minor than {st2.Code}.";
            //if (i > 0)
            //    s = $"{method} {st1.Code} is major than {st2.Code}.";
            //if (i == 0)
            //    s = $"{method} {st1.Code} and {st2.Code} have the same time.";
            //WriteLine(s);

            //i = st1.CompareTo(st2);
            //s = string.Empty;
            //method = "CompareTo(st2) =>";

            //if (i < 0)
            //    s = $"{method} {st1.Code} is minor than {st2.Code}.";
            //if (i > 0)
            //    s = $"{method} {st1.Code} is major than {st2.Code}.";
            //if (i == 0)
            //    s = $"{method} {st1.Code} and {st2.Code} have the same time.";
            //WriteLine(s);

            //#endregion

            //#region Equal tests

            //Title("Test of Equal methods.");
            //b = TradingTime.Equals(st1,st2);
            //s = string.Empty;
            //method = "Equals(st1,st2) =>";

            //if (b)
            //    s = $"{method} {st1.Code} and {st2.Code} are equals.";
            //else
            //    s = $"{method} {st1.Code} and {st2.Code} are not equals.";
            //WriteLine(s);

            //b = st1.Equals(st2);
            //s = string.Empty;
            //method = "st1.Equals(st2) =>";

            //if (st1.Equals(st2))
            //    s = $"{method} {nameof(st1)} and {nameof(st2)} are equals.";
            //else
            //    s = $"{method} {nameof(st1)} and {nameof(st2)} are not equals.";

            //WriteLine(s);

            //#endregion

            //#region Method operators

            //Title("Test of Operator methods.");
            //s = string.Empty;

            //if (st1 == st2)
            //    s += $"{st1.Code} is equal to {st2.Code}.{Environment.NewLine}";
            //if (st1 != st2)
            //    s += $"{st1.Code} is not equal to {st2.Code}.{Environment.NewLine}";
            //if (st1 <= st2)
            //    s += $"{st1.Code} is equal to or less than {st2.Code}.{Environment.NewLine}";
            //if (st1 >= st2)
            //    s += $"{st1.Code} is equal to or greater than {st2.Code}.{Environment.NewLine}";
            //if (st1 < st2)
            //    s += $"{st1.Code} is less than {st2.Code}.{Environment.NewLine}";
            //if (st1 > st2)
            //    s += $"{st1.Code} is greater than {st2.Code}.{Environment.NewLine}";

            //Console.WriteLine(s);
            //Console.WriteLine($"{nameof(st1)} + {nameof(st2)} = {(st1+st2).ToString()}");
            //Console.WriteLine($"{nameof(st1)} - {nameof(st2)} = {(st1-st2).ToString()}");

            //#endregion

        }

        private void PrintSession()
        {
            //while (true)
            //{
            //    Console.WriteLine("TEST PARA IMPRIMIR POR CONSOLA UNA HORA ESPECÍFICA DE UNA SESIÓN.");
            //    Console.WriteLine("-----------------------------------------------------------------");

            //    Console.Write("- Introduzca el código de la session( AM | EU | AE | AS | EL | RG | OVN): ");
            //    string session = Console.ReadLine().ToUpper().Trim();

            //    if (string.IsNullOrEmpty(session) || string.IsNullOrWhiteSpace(session))
            //        return;

            //    if (session == "AM" || session == "AS")
            //    {
            //        Console.Write("- Es una sesión residual (Y/TNinjaScript): ");
            //        string isResidual = Console.ReadLine().ToUpper().Trim();

            //        if (isResidual == "Y")
            //        {
            //            session += "-RS";

            //            if (session == "AM-RS")
            //            {
            //                Console.Write("- Introduce el código de la sesión residual: ( EXT | EOD | NWD ): ");
            //                string specificSession = Console.ReadLine().ToUpper().Trim();

            //                if (specificSession == "EXT" || specificSession == "EOD" || specificSession == "NWD")
            //                    session += "-" + specificSession;
            //            }
            //        }
            //    }

            //    Console.Write("- Introduzca el momento temporal de la sesión: ( O | C ): ");
            //    string price = Console.ReadLine().ToUpper();

            //    if (price == "O" || price == "C")
            //        session += "-" + price;
            //    else
            //        return;

            //    Console.WriteLine();
            //    Console.WriteLine(String.Format("Código: {0} | {1} | {2}", session, session.ToTradingTime().ToSessionTime().LocalTime.ToString(), session.ToTradingTime().ToDescription()));
            //    Console.ReadKey();
            //    Console.Clear();

            //}


        }

        //private static void TimeSpanTester()
        //{
        //    TradingTimeInfo sessionTime1 = TradingTimeInfo.CreateCustomSessionTime(22,0,0, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));
        //    TradingTimeInfo sessionTime2 = TradingTimeInfo.CreateCustomSessionTime(2,0,0, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

        //    Console.WriteLine(String.Format("Utc Offset: {0}",sessionTime1.TimeZoneInfo.BaseUtcOffset.ToString()));
        //    Console.WriteLine(String.Format("Time 1: {0}",sessionTime1.ToUtcTime.ToString()));
        //    Console.WriteLine(String.Format("Time 2: {0}",sessionTime2.ToUtcTime.ToString()));
        //}

        #endregion

    }
}

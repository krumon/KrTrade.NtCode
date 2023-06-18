using System;

namespace KrTrade.NtCode.Helpers
{

    /// <summary>
    /// Helper methods of <see cref="Enum"/> objects.
    /// </summary>
    public static class EnumHelpers
    {

        /// <summary>
        /// Write on console an object enum.
        /// </summary>
        /// <typeparam name="T">Any object enum.</typeparam>
        public static void Writer<T>()
            where T : Enum
        {
            Array array = Enum.GetValues(typeof(T));
            foreach (T t in array)
                Console.WriteLine(t.ToString());
        }

        /// <summary>
        /// Execute a delegate method for each enum member.
        /// </summary>
        /// <typeparam name="T">Any object enum.</typeparam>
        /// <param name="action">Delegate method to execute for each enum member.</param>
        public static void ForEach<T>(Action<T> action)
            where T : Enum
        {
            Array array = Enum.GetValues(typeof(T));
            foreach (T t in array)
                action(t);
        }

    }
}

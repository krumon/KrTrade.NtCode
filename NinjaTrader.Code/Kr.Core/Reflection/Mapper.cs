using System.Reflection;
using System;

namespace Kr.Core.Reflection
{
    /// <summary>
    /// Represents the helper methods to mapper objects.
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// Execute auto mapper between two objects.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void Auto<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
        {
            PropertyInfo[] sourceProperties = Type.GetType(source.GetType().Name).GetProperties();
            PropertyInfo[] targetProperties = Type.GetType(target.GetType().Name).GetProperties();

            foreach (PropertyInfo sourceProperty in sourceProperties)
                foreach (PropertyInfo targetProperty in targetProperties)
                    if (sourceProperty.Name == targetProperty.Name)
                    {
                        targetProperty.SetValue(target, sourceProperty.GetValue(target));
                        break;
                    }
        }

        /// <summary>
        /// Execute reversal mapper between two objects.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void Reversal<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
        {
            PropertyInfo[] sourceProperties = Type.GetType(source.GetType().Name).GetProperties();
            PropertyInfo[] targetProperties = Type.GetType(target.GetType().Name).GetProperties();

            foreach (PropertyInfo sourceProperty in sourceProperties)
                foreach (PropertyInfo targetProperty in targetProperties)
                    if (sourceProperty.Name == targetProperty.Name)
                    {
                        sourceProperty.SetValue(source, targetProperty.GetValue(target));
                        break;
                    }
        }


    }
}

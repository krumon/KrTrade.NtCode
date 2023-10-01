using KrTrade.Nt.DI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace KrTrade.Nt.Console
{

    /// <summary>
    /// Base class for any ninjascripts manager.
    /// </summary>
    /// <typeparam name="TManagerScript">The ninjascripts manager.</typeparam>
    /// <typeparam name="TManagerConfiguration">The ninjascripts manager options.</typeparam>
    public abstract class BaseManager<TManagerScript, TManagerConfiguration,TManagerBuilder> : BaseNinjascript<TManagerScript, TManagerConfiguration,TManagerBuilder>, IManager
        where TManagerScript : BaseManager<TManagerScript, TManagerConfiguration, TManagerBuilder>, IManager
        where TManagerConfiguration : BaseManagerConfiguration<TManagerConfiguration>, IManagerConfiguration
        where TManagerBuilder : BaseManagerBuilder<TManagerScript,TManagerConfiguration,TManagerBuilder>, IManagerBuilder
    {

        #region Private members

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        private List<INinjascript> scripts = new List<INinjascript>();

        #endregion

        #region Public methods

        /// <summary>
        /// Add elements to the manager collection in the order selected.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="ordereventType">The event type reference for sorted the collection.</param>
        public void Add(INinjascript script, EventType fromEvent = EventType.Configure)
        {

            // Make sure the element doesn't exist.
            if (scripts == null || Contains(script))
                return;

            // If the collection is empty...added the first element.
            if (scripts.Count == 0)
            {
                scripts.Add(script);
                return;
            }

            // Make sure if the type exist and multi use isn't allowed.
            if (!script.AllowManagerMultiUse && Contains(script.GetType()))
                return;

            // Add the elements to the sorted collection.
            for (int i = 0; i < scripts.Count; i++)
            {
                if (script.GetOrder(fromEvent) >= scripts[i].GetOrder(fromEvent))
                {
                    if (i == scripts.Count - 1)
                    {
                        scripts.Add(script);
                        break;
                    }
                    continue;
                }
                
                scripts.Insert(i, script);
                break;
            }
        }

        /// <summary>
        /// Gets script from the manager collection.
        /// </summary>
        /// <param name="script">The script to extract.</param>
        /// <returns>If element exists, return it, otherwise return null.</returns>
        public INinjascript Get(INinjascript script)
        {
            if (scripts != null)
                if (scripts.Contains(script))
                    return scripts[scripts.IndexOf(script)];

            return null;
        }

        /// <summary>
        /// Gets script from the manager collection.
        /// </summary>
        /// <param name="type">The type of the script to extract.</param>
        /// <returns>If element exists, return it, otherwise return null.</returns>
        public INinjascript Get(Type type)
        {
            if (scripts != null)
                foreach (var script in scripts)
                    if (type == script.GetType())
                        return script;
            return null;
        }

        /// <summary>
        /// Gets script from the manager collection.
        /// </summary>
        /// <typeparam name="T">The type to extract.</typeparam>
        /// <returns>If element exists, return it, otherwise return null.</returns>
        public INinjascript Get<T>()
            where T : INinjascript
        {
            if (scripts != null)
                foreach (var script in scripts)
                    if (typeof(T) == script.GetType())
                        return script;
            return null;
        }

        /// <summary>
        /// Gets all <see cref="INinjascript"/> objects if have the same type.
        /// </summary>
        /// <param name="type">The type of objects to extract.</param>
        /// <returns>Collection of <see cref="T"/> objects or empty list if they don't exists.</returns>
        public List<INinjascript> GetAll(INinjascript script) => GetAll(script.GetType());

        /// <summary>
        /// Gets all <see cref="INinjascript"/> objects if have the same type.
        /// </summary>
        /// <param name="type">The type of objects to extract.</param>
        /// <returns>Collection of <see cref="T"/> objects or empty list if they don't exists.</returns>
        public List<INinjascript> GetAll(Type type) =>
            scripts.FindAll(x => x.GetType() == type);

        /// <summary>
        /// Gets all <see cref="INinjascript"/> objects if have the same type.
        /// </summary>
        /// <typeparam name="T">The type of the objects to get.</typeparam>
        /// <returns>Collection of <see cref="T"/> objects or empty list if they don't exists.</returns>
        public List<INinjascript> GetAll<T>()
            where T : INinjascript => 
            scripts.FindAll(x => x.GetType() == typeof(T));

        /// <summary>
        /// Gets the first element of the manager collection with the <see cref="T"/> type.
        /// </summary>
        /// <param name="script">The script to find.</param>
        /// <returns>The element finded or the element default value.</returns>
        public INinjascript FirstOrDefault(INinjascript script) =>
            scripts.Find(s => s == script);

        /// <summary>
        /// Gets the first element of the manager collection with the <see cref="T"/> type.
        /// </summary>
        /// <param name="type">The type of the element to find.</param>
        /// <returns>The element finded or the element default value.</returns>
        public INinjascript FirstOrDefault(Type type) =>
            scripts.Find(s => s.GetType() == type);

        /// <summary>
        /// Gets the first element of the manager collection with the <see cref="T"/> type.
        /// </summary>
        /// <typeparam name="T">The type of the element to find.</typeparam>
        /// <returns>The element finded or the default element value.</returns>
        public T FirstOrDefault<T>() where T : INinjascript =>
            (T)scripts.Find(s => s.GetType() == typeof(T));

        /// <summary>
        /// Indicates if one element exists in <see cref="scripts"/>.
        /// </summary>
        /// <typeparam name="T">The type of element to find.</typeparam>
        /// <returns>True, if the element exists, otherwise, false.</returns>
        public bool Contains(INinjascript script) =>
            scripts.Contains(script);

        /// <summary>
        /// Indicates if one element exists in <see cref="scripts"/>.
        /// </summary>
        /// <param name="type">The type of the element to find.</param>
        /// <returns>True, if the element exists, otherwise, false.</returns>
        public bool Contains(Type type) =>
            scripts.Exists(s => s.GetType() == type);

        /// <summary>
        /// Returns if the type of element exists in the manager collection.
        /// </summary>
        /// <typeparam name="T">The type to find in the collection.</typeparam>
        /// <returns>True, if the element exist, otherwise false.</returns>
        public bool Contains<T>() =>
            scripts.Exists(s => s.GetType() == typeof(T));

        /// <summary>
        /// Returns the <see cref="T"/> index in the manager collection.
        /// </summary>
        /// <typeparam name="T">The type of the element to find.</typeparam>
        /// <returns>The <see cref="T"/> index in the manager collection. If the element doesn't exist, returns -1.</returns>
        public int IndexOf(INinjascript script) => scripts.IndexOf(script);

        /// <summary>
        /// Returns the <see cref="T"/> index in the manager collection.
        /// </summary>
        /// <param name="type">The type of the element.</param>
        /// <returns>The object index in the manager collection. If the element doesn't exist, returns -1.</returns>
        public int IndexOf(Type type)
        {
            if(scripts != null && scripts.Count > 0)
                for (int i = 0; i<scripts.Count; i++)
                    if (type == scripts[i].GetType())
                        return i;
            return -1;
        }

        /// <summary>
        /// Returns the <see cref="T"/> index in the manager collection.
        /// </summary>
        /// <typeparam name="T">The type of the element to find.</typeparam>
        /// <returns>The <see cref="T"/> index in the manager collection. If the element doesn't exist, returns -1.</returns>
        public int IndexOf<T>() => IndexOf(typeof(T));

        /// <summary>
        /// Gets a sorted list with the necesary elements for specific event.
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public IEnumerable<INinjascript> GetSortedList(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.BarUpdate:
                default:
                    return from script in scripts
                           where script.GetOrder(eventType) < 99
                           orderby script.GetOrder(eventType) ascending
                           select script;
            }
        }

        /// <summary>
        /// Execute the ninjascripts handler methods.
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="e"></param>
        public override void ExecuteHandlerMethod(EventType eventType, SessionUpdateArgs e = null)
        {
            foreach (INinjascript script in GetSortedList(eventType))
                script.ExecuteHandlerMethod(eventType, e);
        }

        /// <summary>
        /// Sets the ninjascripts collection from <see cref="BaseManagerBuilder"/> class.
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="methodName"></param>
        public void SetScripts(List<INinjascript> scripts, [CallerMemberName] string methodName = null)
        {
            if (methodName == "BaseManagerBuilder" || methodName == "Initialize" || methodName == "Build" || methodName == "Add")
                this.scripts = scripts;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a new <see cref="IBuilder"/> instance.
        /// </summary>
        /// <returns>The constructor can not be invoke.</returns>
        protected IManagerBuilder CreateManagerBuilderInstance()
        {
            ConstructorInfo construct = typeof(TManagerBuilder).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(TManagerScript) }, null);
            if (construct != null)
                return (TManagerBuilder)construct.Invoke(new object[] { this });
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// Clear the <see cref="INinjascript"/> collection.
        /// </summary>
        public override void Dispose()
        {
            scripts.Clear();
        }

        #endregion

    }

}

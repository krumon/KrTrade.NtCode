using KrTrade.Nt.DI.Data;
using System;
using System.Collections;
using System.Collections.Generic;

namespace KrTrade.Nt.Console.Internal
{
    internal class TradingSessionCollection : ITradingSessionCollection
    {

        #region Private members

        private IList<ISessions> _sessions = new List<ISessions>();

        #endregion

        #region Implementation methods

        /// <inheritdoc/>
        public ISessions this[int index]
        {
            get => _sessions[index];
            set => _sessions[index] = value;
        }

        /// <inheritdoc/>
        public int Count => _sessions.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(ISessions session)
        {
            // Make sure item doesn't exist.
            if (_sessions.Contains(session))
                return;
            // If item is the first element...
            if (_sessions.Count == 0)
            {
                _sessions.Add(session);
                return;
            }
            // Otherwise...
            // Search children
            bool hasChildren = false;
            for (int i = 0; i < Count; i++)
            {
                SessionCompareResult result = session.CompareSessionTo(_sessions[i]);
                if (result == SessionCompareResult.Outer)
                {
                    // Store the child
                    ISessions tradingSession = _sessions[i];
                    // Clear the session item
                    _sessions[i] = null;
                    // Add the child to the item
                    session.Add(tradingSession);
                    // Sets the child flag
                    if (!hasChildren)
                        hasChildren = true;
                    break;
                }
            }

            // Clear the old children
            if (hasChildren)
            {
                for (int i = 0; i < Count; i++)
                    while (_sessions[i] == null)
                        RemoveAt(i);
            }

            for (int i = 0; i < Count; i++)
            {
                SessionCompareResult result = session.CompareSessionTo(_sessions[i]);
                switch (result)
                {
                    case SessionCompareResult.Before:
                    case SessionCompareResult.BeforeAndInner:
                        _sessions.Insert(i, session);
                        return;
                    case SessionCompareResult.Later:
                    case SessionCompareResult.InnerAndLater:
                        {
                            if (i == Count - 1)
                                _sessions.Add(session);
                            break;
                        }
                    case SessionCompareResult.Inner:
                        _sessions[i].Add(session);
                        break;
                    case SessionCompareResult.Equals:
                        return;
                    default:
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (_sessions != null && _sessions.Count > 0)
            {
                foreach (TradingSession item in _sessions)
                {
                    if (item.Sessions != null && item.Sessions.Count > 0)
                        item.Sessions.Clear();
                }
                _sessions.Clear();
            }
        }

        /// <inheritdoc/>
        public bool Contains(ISessions item)
        {
            if (_sessions != null && _sessions.Count > 0)
            {
                foreach (TradingSession ts in _sessions)
                {
                    if (ts.Sessions != null && ts.Sessions.Count > 0)
                        ts.Sessions.Contains(item);
                }
                if (_sessions.Contains(item))
                    return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public void CopyTo(ISessions[] array, int arrayIndex)
        {
            _sessions.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<ISessions> GetEnumerator()
        {
            return _sessions.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(ISessions item)
        {
            return _sessions.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, ISessions item)
        {
            _sessions.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(ISessions item)
        {
            return _sessions.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _sessions.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        //public ISessionBuilder CreateDefaultTradingSessionBuilder() => new SessionBuilder();

        ///// <summary>
        ///// Add a <see cref="TradingSession"/> element by the generic type.
        ///// </summary>
        ///// <param name="type">the type of the trading session.</param>
        //public void Add(SessionType type)
        //{
        //    TradingSession ts = TradingSession.CreateTradingSessionByType(type);
        //    Add(ts);
        //}

        ///// <summary>
        ///// Add a <see cref="Trading.SessionType"/> array.
        ///// </summary>
        ///// <param name="types">The <see cref="TradingSession"/> array to add.</param>
        //public void Add(params SessionType[] types)
        //{
        //    if (types == null)
        //        throw new ArgumentNullException(nameof(types));

        //    foreach (var type in types)
        //        Add(type);
        //}

        ///// <summary>
        ///// Add all <see cref="Trading.SessionType"/> enums.
        ///// </summary>
        ///// <typeparam name="T">The generic parameter <see cref="Trading.SessionType"/>.</typeparam>
        //public void Add<T>()
        //    where T : Enum
        //{
        //    if (typeof(T).Name == nameof(Trading.SessionType))
        //    {
        //        EnumHelpers.ForEach<SessionType>((type) =>
        //        {
        //            Add(type);
        //        });
        //    }
        //}

        #endregion
    }
}

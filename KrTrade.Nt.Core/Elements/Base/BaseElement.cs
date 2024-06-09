using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Text;

namespace KrTrade.Nt.Core.Elements
{
    public abstract class BaseElement : IElement
    {
        private readonly NinjaScriptBase _ninjascript;
        private readonly IInfo _info;

        public NinjaScriptBase Ninjascript => _ninjascript;
        public IInfo Info => _info;

        public ElementType Type { get => Info.Type; protected set => Info.Type = value; }
        public string Key => Info.Key;
        public string Name => string.IsNullOrEmpty(Info.Name) ? Key : Info.Name;
        public override bool Equals(object obj) => obj is IElement other && this == other;
        public bool Equals(IElement other) => other != null && this == other;
        public override int GetHashCode() => Info.Key.GetHashCode();

        public static bool operator ==(BaseElement element1, IElement element2) =>
            (element1 is null && element2 is null) ||
            (!(element1 is null) && !(element2 is null) && element1.Key == element2.Key);
        public static bool operator !=(BaseElement element1, IElement element2) => !(element1 == element2);

        public static bool operator ==(IElement element1, BaseElement element2) =>
            (element1 is null && element2 is null) ||
            (!(element1 is null) && !(element2 is null) && element1.Key == element2.Key);
        public static bool operator !=(IElement element1, BaseElement element2) => !(element1 == element2);

        public static bool operator ==(BaseElement element1, BaseElement element2) =>
            (element1 is null && element2 is null) ||
            (!(element1 is null) && !(element2 is null) && element1.Key == element2.Key);
        public static bool operator !=(BaseElement element1, BaseElement element2) => !(element1 == element2);

        protected BaseElement(NinjaScriptBase ninjascript, IInfo info)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException($"The {nameof(ninjascript)} argument cannot be null.");
            _info = info ?? throw new ArgumentNullException($"The {nameof(info)} argument cannot be null.");
        }


        // REPAIR

        public string ToString(int tabOrder, string title, string subtitle, string description, int index, bool isDescriptionBracketsVisible, bool isIndexVisible, string separator, string state)
        {
            // {tab}{header}({subheader})[{description}][{index}]{separator}{state}

            // Text to returns.
            //string text = string.Empty;
            StringBuilder sb = new StringBuilder();

            // TAB
            string tab = string.Empty;
            if (tabOrder > 0)
                for (int i = 0; i < tabOrder; i++)
                    tab += "\t";
            //text += tab;
            sb.Append(tab);

            // HEADER
            if (!string.IsNullOrEmpty(title))
                //text += header;
                sb.Append(title);

            // SUBHEADER
            if (!string.IsNullOrEmpty(subtitle))
            {
                if (string.IsNullOrEmpty(title))
                    //text += subheader;
                    sb.Append(subtitle);
                else
                    //text += "(" + subheader + ")";
                    sb.AppendFormat("({0})", subtitle);
            }

            // DESCRIPTION
            if (!string.IsNullOrEmpty(description))
            {
                if (!isDescriptionBracketsVisible)
                    //text += description;
                    sb.Append(description);
                else
                    //text += "(" + description + ")";
                    sb.AppendFormat("[{0}]", description);
            }

            // INDEX
            if (isIndexVisible)
                sb.AppendFormat("[{0}]", index);

            //SEPARATOR
            if (!string.IsNullOrEmpty(state) && string.IsNullOrWhiteSpace(state))
            {
                if (!string.IsNullOrEmpty(state))
                {
                    if (separator.EndsWith(" "))
                        sb.AppendFormat("{0}", separator);
                    else
                        sb.AppendFormat("{0} ", separator);
                }
                else
                    sb.Append(": ");
            }

            // STATE
            if (!string.IsNullOrEmpty(state))
                sb.Append(state);

            return sb.ToString();
        }
        public string ToString(string state) => ToString(
            tabOrder: 0,
            title: GetHeaderString(),
            subtitle: GetParentString(),
            description: GetDescriptionString(),
            index: 0,
            isDescriptionBracketsVisible: true,
            isIndexVisible: false,
            separator: ": ",
            state: state);
        public string ToString(int tabOrder, string state, int index = 0, string separator = ": ", bool isTitleVisible = true, bool isSubtitleVisible = false, bool isDescriptionVisible = true, bool isDescriptionBracketsVisible = true, bool isIndexVisible = false) => ToString(
            tabOrder: tabOrder,
            title: isTitleVisible ? GetHeaderString() : null,
            subtitle: isSubtitleVisible ? GetParentString() : null,
            description: isDescriptionVisible ? GetDescriptionString() : null,
            index: index,
            isDescriptionBracketsVisible: isDescriptionBracketsVisible,
            isIndexVisible: isIndexVisible,
            separator: separator,
            state: state);

        protected string GetLabelString(bool isLabelVisible, bool isHeaderVisible, bool isParentVisible, bool isDescriptionVisible, bool isDescriptionBracketsVisible, bool isIndexVisible, int index = 0)
        {
            if (!isLabelVisible)
                return string.Empty;

            if (!isHeaderVisible && !isParentVisible && !isDescriptionVisible && !isIndexVisible)
                return string.Empty;

            string header = GetHeaderString();
            string parent = GetParentString();
            string description = GetDescriptionString();

            // Text to returns.
            StringBuilder sb = new StringBuilder();

            // HEADER
            if (!string.IsNullOrEmpty(header))
                sb.Append(header);

            // SUBHEADER
            if (!string.IsNullOrEmpty(parent))
            {
                if (string.IsNullOrEmpty(header))
                    sb.Append(parent);
                else
                    sb.AppendFormat("({0})", parent);
            }

            // DESCRIPTION
            if (!string.IsNullOrEmpty(description))
            {
                if (!isDescriptionBracketsVisible)
                    sb.Append(description);
                else
                    sb.AppendFormat("({0})", description);
            }

            // INDEX
            if (isIndexVisible)
                sb.AppendFormat("[{0}]", index);

            return sb.ToString();

        }
        protected abstract string GetLogString(string state);
        protected abstract string GetHeaderString();
        protected abstract string GetParentString();
        protected abstract string GetDescriptionString();

        public string ToLogString(int tabOrder, string label, string state, string separator = ": ")
        {
            // Text to returns.
            StringBuilder sb = new StringBuilder();

            // TAB
            string tab = string.Empty;
            if (tabOrder > 0)
                for (int i = 0; i < tabOrder; i++)
                    tab += "\t";
            sb.Append(tab);

            // LABEL
            if (!string.IsNullOrEmpty(label))
                sb.Append(label);

            //SEPARATOR AND STATE
            if (!string.IsNullOrEmpty(label) && !string.IsNullOrEmpty(state) && !string.IsNullOrWhiteSpace(state))
            {
                if (!string.IsNullOrEmpty(state))
                {
                    if (separator.EndsWith(" "))
                        sb.AppendFormat("{0}", separator);
                    else
                        sb.AppendFormat("{0} ", separator);

                    sb.Append(state);
                }
            }

            sb.Append(state);

            return sb.ToString();

        }
        public string ToLogString(int tabOrder, string state, int index = 0, string separator = ": ", bool isLabelVisible = true, bool isTitleVisible = true, bool isDescriptionVisible = true, bool isDescriptionBracketsVisible = true, bool isIndexVisible = false)
            => ToLogString(
                tabOrder,
                ToLabelString(isLabelVisible, isTitleVisible, isDescriptionVisible, isDescriptionBracketsVisible, isIndexVisible, index),
                state,
                separator);
        protected string ToString(ElementType elementType) => elementType.ToString();
        protected string ToLabelString(bool isLabelVisible, bool isTitleVisible, bool isDesriptionVisible, bool isDescriptionBracketsVisible, bool isIndexVisible, int index = 0)
        {
            if (!isLabelVisible)
                return string.Empty;

            if (!isTitleVisible && !isDesriptionVisible && !isIndexVisible)
                return string.Empty;

            string title = ToString(Type);
            string description = ToString();

            // Text to returns.
            StringBuilder sb = new StringBuilder();

            // HEADER
            if (!string.IsNullOrEmpty(title))
                sb.Append(title);

            // SUBHEADER
            if (!string.IsNullOrEmpty(description))
            {
                if (string.IsNullOrEmpty(title) || !isDescriptionBracketsVisible)
                    sb.Append(description);
                else
                    sb.AppendFormat("({0})", description);
            }

            // INDEX
            if (isIndexVisible)
                sb.AppendFormat("[{0}]", index);

            return sb.ToString();
        }

        #region Helpers methods

        protected int GetBarIdx(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return -1;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return -1;

            return Ninjascript.CurrentBars[barsInProgress] - barsAgo;
        }
        protected double GetOpen(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return 0.0;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return 0.0;

            return Ninjascript.Opens[barsInProgress][barsAgo];
        }
        protected double GetHigh(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return double.MinValue;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return double.MinValue;

            return Ninjascript.Highs[barsInProgress][barsAgo];
        }
        protected double GetLow(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return double.MaxValue;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return double.MaxValue;

            return Ninjascript.Lows[barsInProgress][barsAgo];
        }
        protected double GetClose(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return 0.0;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return 0.0;

            return Ninjascript.Closes[barsInProgress][barsAgo];
        }
        protected double GetVolume(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return -1.0;
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return -1.0;

            return Ninjascript.Volumes[barsInProgress][barsAgo];
        }
        protected DateTime GetTime(int barsInProgress, int barsAgo)
        {
            if (barsInProgress < 0 || barsInProgress != Ninjascript.BarsInProgress)
                return new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            if (barsAgo < 0 || barsAgo >= Ninjascript.BarsArray[barsInProgress].Count)
                return new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

            return Ninjascript.Times[barsInProgress][barsAgo];
        }

        protected void Execute(List<Action> actions)
        {
            if (actions == null || actions.Count == 0)
                return;

            for (int i = 0; i < actions.Count; i++)
                actions[i]?.Invoke();
        }
        protected bool IsBarsInProgressOutOfRange(int barsInProgress)
        {
            if (barsInProgress < 0 || barsInProgress >= Ninjascript.BarsArray.Length)
                throw new ArgumentOutOfRangeException(nameof(barsInProgress));
            return false;
        }

        /// <summary>
        /// Indicates whether NinjaScript is in any of the configuration states.
        /// The configuaration states are 'Configure' and 'DataLoaded'.
        /// </summary>
        /// <returns></returns>
        protected bool IsInConfigurationStates()
        {
            if (Ninjascript.State == State.Configure || Ninjascript.State == State.DataLoaded)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is in running states.
        /// The running states are 'Historical' and 'Realtime'.
        /// </summary>
        /// <returns>True, when the NijaScript State is 'Historical' or 'Realtime'.</returns>
        protected bool IsInRunningStates()
        {
            if (Ninjascript.State == State.Historical || Ninjascript.State == State.Realtime)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the configuration states.
        /// The configuaration states are 'Configure' and 'DataLoaded'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Configure' and 'DataLoaded'.</returns>
        protected bool IsOutOfConfigurationStates()
        {
            if (Ninjascript.State != State.Configure && Ninjascript.State != State.DataLoaded)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the configure state.
        /// The configure state is 'Configure'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Configure'.</returns>
        protected bool IsOutOfConfigureState()
        {
            if (Ninjascript.State != State.Configure)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the data loaded state.
        /// The data loaded state is 'DataLoaded'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'DataLoaded'.</returns>
        protected bool IsOutOfDataLoadedState()
        {
            if (Ninjascript.State != State.DataLoaded)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript is out of the running states.
        /// The running states are 'Historical' and 'Realtime'.
        /// </summary>
        /// <returns>True, when the NijaScript State is NOT 'Historical' and 'Realtime'.</returns>
        protected bool IsOutOfRunningStates()
        {
            if (Ninjascript.State != State.Historical && Ninjascript.State != State.Realtime)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript indexes are available.
        /// The indexs are 'BarsInProgress' and 'CurrentBar'.
        /// </summary>
        /// <returns>True, when the NijaScript indexes are greater than -1.</returns>
        protected bool IsNinjaScriptIndexesAvailable()
        {
            if (IsNotAvilableBarsInProgressIdx() && IsNotAvailableFirstBarIdx())
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript data series is available to be updated.
        /// The index is 'CurrentBar'.
        /// </summary>
        /// <returns>True, when the NijaScript index are greater than -1.</returns>
        protected bool IsNotAvailableFirstBarIdx()
        {
            if (Ninjascript.CurrentBars[Ninjascript.BarsInProgress] < 0)
                return true;

            return false;
        }
        /// <summary>
        /// Indicates whether NinjaScript multi data series is available to be updated.
        /// The index is 'BarsInProgress'.
        /// </summary>
        /// <returns>True, when the NijaScript index are greater than -1.</returns>
        protected bool IsNotAvilableBarsInProgressIdx()
        {
            if (Ninjascript.BarsInProgress < 0)
                return true;

            return false;
        }

        #endregion

    }

    public abstract class BaseElement<TInfo> : BaseElement, IElement<TInfo>
        where TInfo : IInfo
    {
        protected BaseElement(NinjaScriptBase ninjascript, IInfo info) : base(ninjascript,info)
        {
        }

        new public TInfo Info => (TInfo)base.Info; 
    }

}

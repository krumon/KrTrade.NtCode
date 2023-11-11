namespace KrTrade.Nt.Core.Events
{
    public class TickEventArgs
    {
        public bool IsFirstTick { get;set; }

        public TickEventArgs(bool isFirstTick)
        {
            IsFirstTick = isFirstTick;
        }
    }
}

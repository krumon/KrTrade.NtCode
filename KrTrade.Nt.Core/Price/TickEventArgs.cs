namespace KrTrade.Nt.Core
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

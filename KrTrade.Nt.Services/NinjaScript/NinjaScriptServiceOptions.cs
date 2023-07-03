namespace KrTrade.Nt.Services
{
    public class NinjaScriptServiceOptions
    {
        private readonly int _minimumGapSize = 2;
        private readonly int _maximumGapSize = 20;

        public int MinGapSize { get; set; }
        public int MaxGapSize { get; set;}

        public void NormalizeOptions()
        {
            if(MinGapSize < _minimumGapSize || MinGapSize >= _maximumGapSize)
                MinGapSize = _minimumGapSize;
            if(MaxGapSize <= _minimumGapSize || MaxGapSize > _maximumGapSize)
                MaxGapSize = _maximumGapSize;
            if(MinGapSize >= MinGapSize)
            {
                MinGapSize = _minimumGapSize;
                MaxGapSize = _maximumGapSize;
            }
        }
    }
}

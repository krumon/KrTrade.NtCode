namespace KrTrade.NtCode
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseIndicatorsManagerConfiguration<TManagerConfiguration> : BaseManagerConfiguration<TManagerConfiguration>, IIndicatorsManagerConfiguration
        where TManagerConfiguration : BaseIndicatorsManagerConfiguration<TManagerConfiguration>, IIndicatorsManagerConfiguration
    {
    }
}

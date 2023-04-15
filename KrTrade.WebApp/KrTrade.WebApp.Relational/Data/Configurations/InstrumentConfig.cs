using KrTrade.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KrTrade.WebApp.Relational.Data.Configurations
{
    public class InstrumentConfig : IEntityTypeConfiguration<Instrument>
    {
        public void Configure(EntityTypeBuilder<Instrument> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(true);

            builder.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.ToTable("Instruments");

        }
    }
}

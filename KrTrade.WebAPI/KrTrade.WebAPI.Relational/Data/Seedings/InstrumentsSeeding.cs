using KrTrade.WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace KrTrade.WebApp.Relational.Data.Seedings
{
    public class InstrumentsSeeding
    {
        public static void Seed(ModelBuilder modelBuilder) 
        {
            var mes = new Instrument()
            {
                Id="MES",
                Name="Micro-S&P500",
                TickSize="0.25",
                Currency="Dollar USD",
                Description="Micro standar and poor 500 ............."            
            };
           
            var es = new Instrument()
            {
                Id="ES",
                Name="Mini-S&P500",
                TickSize="0.25",
                Currency="Dollar USD",
                Description="Mini standar and poor 500 ............."            
            };
           
            modelBuilder.Entity<Instrument>().HasData(mes);
        
        }
    }
}

using AutoMapper;
using KrTrade.WebApp.Core.DTOs;
using KrTrade.WebApp.Core.Entities;

namespace KrTrade.WebApp.Relational.Mappings
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Instrument, InstrumentDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using MonstersInc.Models;
using MonstersIncDomain;

namespace MonstersInc.MapperProfiles
{
    public class IntimidatorProfile : Profile
    {
        public IntimidatorProfile()
        {
            CreateMap<Intimidator, IntimidatorDto>().ReverseMap();
            CreateMap<Intimidator, IntimidatorCreationDto>().ReverseMap();
            CreateMap<Intimidator, CurrentEmployeeOfTheMonthDto>().ReverseMap();
        }
    }
}

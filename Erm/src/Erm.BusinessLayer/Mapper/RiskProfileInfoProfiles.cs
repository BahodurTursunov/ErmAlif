using AutoMapper;
using Erm.src.Erm.BusinessLayer;
using Erm.src.Erm.DataAccess;

namespace Erm.src.Erm.BusinessLayer.Mapper;

public sealed class RiskProfileInfoProfile : Profile // Profile содержит в себе кастомное описание того, как нужно преобразовать одну модель в другую
{
    public RiskProfileInfoProfile()
    {
        CreateMap<RiskProfileInfo, RiskProfile>()
            .ForMember(dest => dest.RiskName, opt => opt.MapFrom(src => src.Name)) // здесь в RiskName будет присваиваться значение из DTO RiskProfile
            .ForMember(dest => dest.BusinessProcess,
                opt => opt.MapFrom(src => new BusinessProcess()
                {
                    Name = src.BusinessProcess,
                    Domain = src.BusinessProcess
                }))
            .ReverseMap()
            .ForMember(dest => dest.BusinessProcess, opt => opt.MapFrom(src => src.BusinessProcess.Name))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RiskName));
    }
}
using AutoMapper;
using Erm.src.Erm.BusinessLayer.Mapper;

namespace Erm.src.Erm.BusinessLayer
{
    internal static class AutoMapperHelper
    {
        internal readonly static MapperConfiguration MapperConfiguration = new(opt => opt.AddProfile<RiskProfileInfoProfile>());
    }
}
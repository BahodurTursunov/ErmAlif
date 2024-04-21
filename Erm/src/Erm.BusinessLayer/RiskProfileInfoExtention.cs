using Erm.src.Erm.DataAccess;
namespace Erm.src.Erm.BusinessLayer;
internal static class RiskProfileInfoExtention
{
    internal static RiskProfile ToRiskProfile(this RiskProfileInfo profileInfo) => new()
    {
        RiskName = profileInfo.Name,
        Description = profileInfo.Description,
        BusinessProcess = new() { Name = profileInfo.BusinessProcess, Domain = profileInfo.BusinessProcess },
        PotentialBusinessImpact = profileInfo.PotentialBusinessImpact,
        OccurrenceProbability = profileInfo.OccurrenceProbability
    };
}
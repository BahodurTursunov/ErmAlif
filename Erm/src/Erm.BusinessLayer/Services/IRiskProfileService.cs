using Erm.src.Erm.BusinessLayer;

namespace Erm.src.Erm.BusinessLayer.Services;

public interface IRiskProfileService
{
    Task CreateAsync(RiskProfileInfo profileInfo, CancellationToken token = default);

    Task<IEnumerable<RiskProfileInfo>> QueryAsync(string query, CancellationToken token = default);
    Task<RiskProfileInfo> GetAsync(string name, CancellationToken token = default);
    Task UpdateAsync(string name, RiskProfileInfo riskProfileInfo, CancellationToken token = default);
    Task DeleteAsync(string riskProfileName, CancellationToken token = default);
    Task<double> CalculateRiskAsync(string riskProfileName);

}

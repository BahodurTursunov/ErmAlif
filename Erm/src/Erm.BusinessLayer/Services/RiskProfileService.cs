using AutoMapper;
using Erm.src.Erm.BusinessLayer.Validators;
using Erm.src.Erm.DataAccess;
using Erm.src.Erm.DataAccess.Repositories;
using FluentValidation;

namespace Erm.src.Erm.BusinessLayer.Services;

public sealed class RiskProfileService(
    IValidator<RiskProfileInfo> validator,
    RiskProfileRepositoryProxy profileRepositoryProxy,
    IMapper mapper
    ) : IRiskProfileService
{
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<RiskProfileInfo> _validator = validator;
    private readonly RiskProfileRepositoryProxy _repository = profileRepositoryProxy;

    public async Task<double> CalculateRiskAsync(string riskProfileName)
    {
        var riskProfile = await _repository.GetAsync(riskProfileName);

        double calculateRisk = riskProfile.OccurrenceProbability * riskProfile.PotentialBusinessImpact;

        return calculateRisk;
    }

    public async Task CreateAsync(RiskProfileInfo profileInfo, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(profileInfo, token);

        RiskProfile riskProfile = _mapper.Map<RiskProfile>(profileInfo);
        await _repository.CreateAsync(riskProfile, token);
    }

    public async Task DeleteAsync(string riskProfileName, CancellationToken token = default)
    {
        await _repository.DeleteAsync(riskProfileName, token);
    }

    public async Task<RiskProfileInfo> GetAsync(string name, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        return _mapper.Map<RiskProfileInfo>(await _repository.GetAsync(name, token));
    }

    public async Task<IEnumerable<RiskProfileInfo>> QueryAsync(string query, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(query);
        return _mapper.Map<IEnumerable<RiskProfileInfo>>(await _repository.QueryAsync(query, token));
    }

    public async Task UpdateAsync(string name, RiskProfileInfo profileInfo, CancellationToken token = default)
    {
        RiskProfileInfoValidator validatorRules = new();
        await validatorRules.ValidateAndThrowAsync(profileInfo, token);

        await _repository.UpdateAsync(name,
            new RiskProfile
            {
                RiskName = profileInfo.Name,
                Description = profileInfo.Description,
                BusinessProcess =
                    new BusinessProcess
                    {
                        Name = profileInfo.BusinessProcess,
                        Domain = profileInfo.BusinessProcess
                    },
                OccurrenceProbability = profileInfo.OccurrenceProbability,
                PotentialBusinessImpact = profileInfo.PotentialBusinessImpact
            }, token);
    }
}

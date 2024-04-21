namespace Erm.src.Erm.BusinessLayer;

public readonly record struct RiskProfileInfo(
    string Name,
    string Description,
    string BusinessProcess,
    int OccurrenceProbability,
    int PotentialBusinessImpact
);
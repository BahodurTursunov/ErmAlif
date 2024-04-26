using System.Text.Json;
using System.Text.Json.Serialization;
using Erm.src.Erm.DataAccess;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Erm.src.Erm.DataAccess.Repositories;

public sealed class RiskProfileRepositoryProxy(IDistributedCache distributedCache, RiskProfileRepository originalRepository) : IRiskProfileRepository
{
    private readonly RiskProfileRepository _originalRepository = originalRepository;
    private readonly IDistributedCache _db = distributedCache;

    public Task CreateAsync(RiskProfile entity, CancellationToken token = default) 
        => _originalRepository.CreateAsync(entity, token);

    public Task DeleteAsync(string name, CancellationToken token = default) 
        => _originalRepository.DeleteAsync(name, token);

    public Task<IEnumerable<RiskProfile>> GetAllAsync(string query, CancellationToken token = default) 
        => originalRepository.GetAllAsync(query, token);

    public async Task<RiskProfile> GetAsync(string name, CancellationToken token = default)
    {
        RedisValue redisValue = await _db.GetStringAsync(name, token);

        if (string.IsNullOrEmpty(redisValue))
        {
            RiskProfile profileFromDb = await _originalRepository.GetAsync(name, token);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            // Serialize or deserialize with the options
            var redisProfileJson = JsonSerializer.Serialize(profileFromDb, options);
            //string redisProfileJson = JsonSerializer.Serialize(profileFromDb);

            await _db.SetStringAsync(name, redisProfileJson, token);

            return profileFromDb;
        }

        string redisProfileJsonStr = redisValue.ToString();

        RiskProfile profile = JsonSerializer.Deserialize<RiskProfile>(redisProfileJsonStr) ?? throw new InvalidOperationException();

        return profile;
    }
    public Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default) 
        => _originalRepository.QueryAsync(query, token);

    public Task UpdateAsync(string name, RiskProfile riskProfile, CancellationToken token = default) => _originalRepository.UpdateAsync(name, riskProfile, token);
}
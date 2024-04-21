using Erm.src.Erm.BusinessLayer;
using Erm.src.Erm.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/riskprofiles")]
public sealed class RiskProfileController(IRiskProfileService riskProfileService) : ControllerBase
{
    private readonly IRiskProfileService _riskProfileService = riskProfileService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RiskProfileInfo riskProfileInfo)
    {
        await _riskProfileService.CreateAsync(riskProfileInfo);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Query([FromQuery] string? query, [FromQuery] string? name)
    {
        if (!string.IsNullOrEmpty(query))
            return Ok(await _riskProfileService.QueryAsync(query));

        if (!string.IsNullOrEmpty(name))
            return Ok(await _riskProfileService.GetAsync(name));

        return BadRequest();
    }

    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name) =>
        Ok(await _riskProfileService.GetAsync(name));

    [HttpPut]
    [Route("{name}")]
    public async Task<IActionResult> Update([FromRoute] string name, [FromBody] RiskProfileInfo riskProfileInfo)
    {
        await _riskProfileService.UpdateAsync(name, riskProfileInfo);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string name)
    {
        await _riskProfileService.DeleteAsync(name);
        return Ok();
    }

    [HttpGet("GetCalculateRisk")]
    public async Task<IActionResult> GetCalculateRisk([FromQuery] string name)
    {
        return Ok(await _riskProfileService.CalculateRiskAsync(name));
    }
}
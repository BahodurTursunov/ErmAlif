using Erm.src.Erm.BusinessLayer;
using Erm.src.Erm.BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Erm.PresentationLater.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class RiskProfileController : ControllerBase
    {
        private readonly IRiskProfileService _riskProfileService;

        public RiskProfileController(IRiskProfileService riskProfileService)
        {
            _riskProfileService = riskProfileService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RiskProfileInfo riskProfileInfo)
        {
            await _riskProfileService.CreateAsync(riskProfileInfo);
            return Ok("Data added in DataBase");
        }

        [HttpGet("getQuery")]
        public async Task<IActionResult> Query([FromQuery] string? query, [FromQuery] string? name)
        {
            if (!string.IsNullOrEmpty(query))
                return Ok(await _riskProfileService.QueryAsync(query));

            if (!string.IsNullOrEmpty(name))
                return Ok(await _riskProfileService.GetAsync(name));

            return BadRequest();
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName([FromRoute] string name) =>
            Ok(await _riskProfileService.GetAsync(name));

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromRoute] string nameForUpdate, [FromBody] RiskProfileInfo riskProfileInfo)
        {
            await _riskProfileService.UpdateAsync(nameForUpdate, riskProfileInfo);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] string name)
        {
            await _riskProfileService.DeleteAsync(name);
            return Ok($"Deleted <{name}> from Database");
        }
    }
}

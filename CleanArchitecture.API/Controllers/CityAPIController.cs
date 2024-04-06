using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityAPIController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityAPIController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet("GetAllCity")]
        public async Task<ActionResult<ResponseDTO>> GetAllCity()
        {
            return Ok(await _cityService.GetAllCity());
        }

        [HttpGet("GetCity/{cityId:int}")]
        public async Task<ActionResult<ResponseDTO>> GetCity(int cityId)
        {
            return Ok(await _cityService.GetCity(cityId));
        }

        [HttpPost("CreateCity")]
        public async Task<ActionResult<ResponseDTO>> CreateCity([FromBody] City city)
        {
            return Ok(await _cityService.CreateCity(city));
        }

        [HttpPut("UpdateCity")]
        public async Task<ActionResult<ResponseDTO>> UpdateCity([FromBody] City city)
        {
            return Ok(await _cityService.UpdateCity(city));
        }

        [HttpDelete("DeleteCity/{cityId:int}")]
        public async Task<ActionResult<ResponseDTO>> DeleteCity([FromRoute] int cityId)
        {
            return Ok(await _cityService.DeleteCity(cityId));
        }
    }
}

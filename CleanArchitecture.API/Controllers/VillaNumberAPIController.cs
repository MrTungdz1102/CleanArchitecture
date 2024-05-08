using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly IVillaNumberService _service;
        public VillaNumberAPIController(IVillaNumberService service)
        {
            _service = service;
        }

        [HttpGet("GetAllVillaNumber")]
        public async Task<ActionResult<ResponseDTO>> GetAllVillaNumber([FromQuery] QueryParameter queryParameter, string? userId)
        {
            return Ok(await _service.GetAllVillaNumber(queryParameter, userId));
        }

        [HttpGet("GetVillaNumber/{villaNumberId:int}")]
         public async Task<ActionResult<ResponseDTO>> GetVillaNumber([FromRoute] int villaNumberId)
        {
            return Ok(await _service.GetVillaNumber(villaNumberId));
        }

        [HttpPost("CreateVillaNumber")]
        public async Task<ActionResult<ResponseDTO>> CreateVillaNumber([FromBody] VillaNumber villaNumber)
        {
            return Ok(await _service.CreateVillaNumber(villaNumber));
        }

        [HttpPut("UpdateVillaNumber")]
        public async Task<ActionResult<ResponseDTO>> UpdateVillaNumber(VillaNumber villaNumber)
        {
            return Ok(await _service.UpdateVillaNumber(villaNumber));
        }

        [HttpDelete("DeleteVillaNumber/{villaNumberId:int}")]
        public async Task<ActionResult<ResponseDTO>> DeleteVillaNumber([FromRoute] int villaNumberId)
        {
            return Ok(await _service.DeleteVillaNumber(villaNumberId));
        }
    }
}

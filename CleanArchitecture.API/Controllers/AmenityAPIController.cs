using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityAPIController : ControllerBase
    {
        private readonly IAmenityService _amenityService;
        public AmenityAPIController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpGet("GetAllAmenity")]
        public async Task<ActionResult<ResponseDTO>> GetAllAmenity([FromQuery] QueryParameter query, string? userId)
        {
            return Ok(await _amenityService.GetAllAmenity(query, userId));
        }

        [HttpGet("GetAmenity/{amenityId:int}")]
        public async Task<ActionResult<ResponseDTO>> GetAmenity([FromRoute] int amenityId)
        {
            return Ok(await _amenityService.GetAmenity(amenityId));
        }

        [HttpPost("CreateAmenity")]
        public async Task<ActionResult<ResponseDTO>> CreateAmenity([FromBody] Amenity amenity)
        {
            return Ok(await _amenityService.CreateAmenity(amenity));
        }

        [HttpPut("UpdateAmenity")]
        public async Task<ActionResult<ResponseDTO>> UpdateAmenity([FromBody] Amenity amenity)
        {
            return Ok(await _amenityService.UpdateAmenity(amenity));
        }

        [HttpDelete("DeleteAmenity/{amenityId:int}")]
        public async Task<ActionResult<ResponseDTO>> DeleteAmenity([FromRoute] int amenityId)
        {
            return Ok(await _amenityService.DeleteAmenity(amenityId));
        }
    }
}

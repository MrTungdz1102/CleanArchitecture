using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAPIController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleAPIController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("GetAllRole")]
        public ActionResult<ResponseDTO> GetAllRole()
        {
            return Ok(_roleService.GetAllRole());
        }
    }
}

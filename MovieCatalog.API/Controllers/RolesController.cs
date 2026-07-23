using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MovieCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("GetAllRoles")]

        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            
            if(roles.Count==0)
            {
                return NotFound("No Roles Found");
            }
            return Ok(roles);
        }

        [HttpPost("CreatRole")]

        public async Task<IActionResult> CreateRoles(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return BadRequest($"Role '{roleName}' already exists.");
            }
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok($"Role '{roleName}' created successfully.");
        }
    
    }
}

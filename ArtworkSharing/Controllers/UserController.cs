using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Core.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return NotFound(new { Message = "Not found User" });

            var user = await _userService.GetUser(id);

            return user != null ? Ok(user) : NotFound(new { Message = "Not found User" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserModel um)
        {
            var id = HttpContext.Items["UserId"];
            if (id == null) return Unauthorized();

            var user = await _userService.GetOne(Guid.Parse(id + ""));

            if (user != null)
            {
                if (user.Role.Name != "Admin" && um.RoleId != Guid.Empty) return Unauthorized();

                if (um.RoleId != Guid.Empty && (await _roleService.GetRole("Admin") == null)) return BadRequest(new { Message = "Not found role" });

                if (um.RoleId == Guid.Empty)
                {
                    um.RoleId = (await _roleService.GetRole("User")).Id;
                }

                var rs = await _userService.CreateNewUser(um);

                return rs != null ? StatusCode(StatusCodes.Status201Created, rs) : StatusCode(StatusCodes.Status400BadRequest, new { Message = "Create fail" });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserModel upm)
        {
            if (id == Guid.Empty || upm == null) return BadRequest(new { Message = "Not found user" });
            return Ok(await _userService.UpdateUser(id, upm));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest(new { Message = "Not found user" });

            await _userService.DeleteUser(id);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}

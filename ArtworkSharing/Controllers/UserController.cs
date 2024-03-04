using ArtworkSharing.Core.Domain.Entities;
using ArtworkSharing.Core.Interfaces.Services;
using ArtworkSharing.Core.ViewModels.User;
using ArtworkSharing.Service.AutoMappings;
using Microsoft.AspNetCore.Mvc;

namespace ArtworkSharing.Controllers;
[ApiController]
[Route("api/usercontroller")]
public class UserController : Controller
{
     private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _roleService = roleService;
            _userService = userService;
        }


    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        try
        {
            var userList = await _userService.GetUsers();
            return Ok(userList);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    [HttpGet("getuser")]
    public async Task<ActionResult> getUser(Guid userId)
    {
        try
        {
            var user = await _userService.GetUser(userId);
            return Ok(user);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost("createuser")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserViewModel cum)
    {
        try
        {
            var user = AutoMapperConfiguration.Mapper.Map<User>(cum);
             _userService.CreateNewUser(user);
            return Ok(user);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete("deleteuser")]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        try
        {
            var user = _userService.GetUser(userId);
            if (user == null)
                return NotFound("User not found");
            else
            {
                await _userService.DeleteUser(userId);
            }

            return Ok("Deleted!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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

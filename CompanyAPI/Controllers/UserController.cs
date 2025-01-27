using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var userDto = await _userService.GetUserByIdAsync(id);
        if (userDto == null)
            return NotFound($"User with ID {id} not found.");
        return Ok(userDto);
    }

    [HttpPost]
    [AllowAnonymous] 
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password) || string.IsNullOrWhiteSpace(userDto.Email))
        {
            return BadRequest("Username, password, and email are required.");
        }

        if (await _userService.UserExists(userDto.Username))
        {
            return BadRequest("Username already exists.");
        }

        var createdUser = await _userService.CreateUser(userDto);
        if (createdUser == null)
        {
            return BadRequest("User could not be created.");
        }

        return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, new UserDto(createdUser));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
    {
        if (id != userDto.Id)
            return BadRequest("ID mismatch in the URL and body.");

        var success = await _userService.UpdateUserAsync(id, userDto);
        if (!success)
            return NotFound($"User with ID {id} not found.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if (!success)
            return NotFound($"User with ID {id} not found.");

        return NoContent();
    }
}

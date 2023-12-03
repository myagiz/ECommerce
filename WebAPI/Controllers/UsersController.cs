using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUserAsync")]
        public async Task<IActionResult> GetAllUserAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetUserByIdAsync/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("CreateUserAsync")]
        public async Task<IActionResult> CreateUserAsync(CreateUserDto model)
        {
            var result = await _userService.CreateUserAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateUserAsync")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDto model)
        {
            var result = await _userService.UpdateUserAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteUserAsync/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

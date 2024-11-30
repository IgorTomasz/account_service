using account_service.context;
using account_service.models;
using account_service.models.DTOs;
using account_service.services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace account_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("/auth/login")]
        public async Task<IActionResult> login(loginDTO loginDTO)
        {
            Guid userId = await _userService.CheckIfUserExists(loginDTO.username);
            if (userId == Guid.Empty) {
                return BadRequest("This user doesn't exists");
            }

            return Ok("Correct user");
        }

        [HttpGet("/adm/users")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.getAllUsers());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
			await _userService.createUser(userDTO);

            return Created();
        }
    }
}

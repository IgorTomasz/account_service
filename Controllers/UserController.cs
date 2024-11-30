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
    [Route("account/[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("auth/login")]
        public async Task<IActionResult> Login(LoginRequest loginDTO)
        {
            Guid userId = await _userService.CheckIfUserExists(loginDTO.Username);

            if (userId == Guid.Empty) {
                return BadRequest("This user doesn't exists");
            }

            bool isMatching = await _userService.CheckPasswordMatchUser(userId, loginDTO.Password);

            if (isMatching)
            {
				return Ok("Correct credentials");
			}

            return Conflict();
        }

        [HttpGet("adm/users")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> Create(RegisterRequest userDTO)
        {
            bool isExistingAlready = await _userService.CheckIfAlreadyExistsByUsernameAndEmail(userDTO.Username, userDTO.Email);

            if (isExistingAlready)
            {
				return Conflict("User with that username/email already exists.");
			}

            if (DateOnly.FromDateTime(DateTime.Now).AddYears(-18) < userDTO.DateOfBirth)
            {
                return Conflict("User to young.");
            }

			await _userService.CreateUser(userDTO);

            return Created();
        }

        [HttpGet("profile/{userIdString}")]
        public async Task<IActionResult> GetUserProfile(string userIdString)
        {
			Guid userId = ParseGuid(userIdString);

            if (userId == Guid.Empty)
            {
                return BadRequest("Wrong user guid");
            }
            
            User user = await _userService.GetUserProfile(userId);
            if (user.Username == string.Empty)
            {
                return Conflict("Something went wrong retreving user profile from userId");
            }

            return Ok(new
            {
                UserId = user.UserId,
                Username = user.Username,
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin
            });
        }

		public Guid ParseGuid(string guidString)
		{
			Guid userId = Guid.Empty;

			try
			{
				userId = Guid.Parse(guidString);
			}
			catch (FormatException e)
			{

			}

			return userId;
		}
	}
}

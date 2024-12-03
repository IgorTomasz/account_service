using account_service.context;
using account_service.models;
using account_service.models.DTOs;
using account_service.models.UserDTOs;
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

		[HttpGet("adm/users")]
		public async Task<IActionResult> Get()
		{
			return Ok(await _userService.GetAllUsers());
		}

		[HttpGet("profile/{userId}")]
		public async Task<IActionResult> GetUserProfile(Guid userId)
		{
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

		[HttpPost("auth/login")]
        public async Task<IActionResult> Login(LoginRequest loginDTO)
        {
            Guid userId = await _userService.CheckIfUserExists(loginDTO.Username);

            if (userId == Guid.Empty) {
                return BadRequest(new LoginResponse
				{
					Success = false,
					Error = "This user doesn't exists",
					UserId = Guid.Empty
				});
            }

            bool isMatching = await _userService.CheckPasswordMatchUser(userId, loginDTO.Password);

            if (isMatching)
            {
				return Ok(new LoginResponse
				{
					Success = true,
					UserId = userId,
				});
			}

            return Conflict(new LoginResponse
			{
				Success = false,
				Error = "Wrong credentials",
				UserId= Guid.Empty
			});
        }

		[HttpPatch("profile/update-password")]
		public async Task<IActionResult> UpdatePassword(ChangePasswordRequest passwordRequest)
		{
			bool isExisting = await _userService.CheckIfUserExists(passwordRequest.userId);

			if (!isExisting)
			{
				return NotFound("User does not exist");
			}

			bool passwordUpdated = await _userService.ChangePassword(passwordRequest);

			if (passwordUpdated)
			{
				return Ok("Password changed");
			}

			return Conflict("Something went wrong while saving the new password");
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

			User user = await _userService.CreateUser(userDTO);

            return Created("",user);
        }


		[HttpDelete("adm/delete")]
		public async Task<IActionResult> DeleteUser(Guid userId)
		{
			await _userService.DeleteUser(userId);
			return Ok($"User {userId} was deleted");
		}

		
	}
}

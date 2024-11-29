using account_service.context;
using account_service.models;
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
        private readonly UserDatabaseContext _context;

        public UserController(ILogger<UserController> logger, UserDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Users.Select(e => new User
            {
                userId = e.userId,
                name = e.name,
                lastname = e.lastname,
                email = e.email,
                username = e.username,
                dateOfBirth = e.dateOfBirth,
                isActive = e.isActive,
                isVerified = e.isVerified,
                createdAt = e.createdAt,
                lastLogin = e.lastLogin,
                passwordHash = e.passwordHash,
                sessions = e.sessions

            }).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
			byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: userDTO.passwordHash, salt: salt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 1000, numBytesRequested: 256 / 8));
            var g = Guid.NewGuid();

			var id = await _context.Users.AddAsync(new User
            {
                userId = g,
                name = userDTO.name,
                lastname = userDTO.lastname,
                email = userDTO.email,
                username = userDTO.username,
                isActive = userDTO.isActive,
                isVerified = userDTO.isVerified,
                createdAt = DateTime.Now,
                passwordHash = hashed,
                dateOfBirth = userDTO.dateOfBirth,
                lastLogin=null
            });

            await _context.SaveChangesAsync();

            return Created();
        }
    }
}

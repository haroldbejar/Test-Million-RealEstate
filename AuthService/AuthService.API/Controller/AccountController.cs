using System.Security.Cryptography;
using System.Text;
using AuthService.Application.Dtos;
using AuthService.Application.Interfaces;
using AuthService.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly ITokenService _tokenService;

        public AccountController(IAppUserService appUserService, ITokenService tokenService)
        {
            _appUserService = appUserService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerUser)
        {
            var existingUser = await _appUserService.ValidateAppUserExist(registerUser.UserName);
            if (existingUser) return BadRequest("El usuario ya existe!");

            await _appUserService.Register(registerUser);

            var user = new LogedUserDto
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                Token = _tokenService.CreateToken(registerUser)
            };

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(RegisterDto registerUser)
        {
            var existingUser = await _appUserService.GetUserByUserName(registerUser.UserName);
            if (existingUser == null)
                throw new UserNotFoundException($"Usuario {registerUser.UserName} no encontrado");

            using var hmac = new HMACSHA256(existingUser.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(registerUser.PassWord));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != existingUser.PasswordHash[i])
                    throw new InvalidCredentialsException("Credenciales inválidas");
            }

            var user = new LogedUserDto
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                Token = _tokenService.CreateToken(registerUser)
            };

            return Ok(user);
        }


    }
}
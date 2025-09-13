using System.Text;
using AuthService.Application.Dtos;
using AuthService.Application.Interfaces;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Models;
using AuthService.Domain.Exceptions;


namespace AuthService.Application.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _userRepository;

        public AppUserService(IAppUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<AppUserDto> GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("El nombre de usuario no puede estar vacío");

            var user = await _userRepository.GetUserByUserName(userName) ?? throw new UserNotFoundException($"Usuario {userName} no encontrado");
            return new AppUserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            };
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="resgisterDto"></param>
        /// <returns></returns>
        public async Task<AppUserDto> Register(RegisterDto resgisterDto)
        {
            if (resgisterDto == null)
                throw new ArgumentNullException(nameof(resgisterDto), "Los datos de registro no pueden ser nulos");

            if (string.IsNullOrEmpty(resgisterDto.UserName))
                throw new ArgumentException("El nombre de usuario es requerido");

            if (string.IsNullOrEmpty(resgisterDto.PassWord))
                throw new ArgumentException("La contraseña es requerida");

            bool userExists = await ValidateAppUserExist(resgisterDto.UserName);
            if (userExists)
                throw new DuplicateUserException($"El usuario {resgisterDto.UserName} ya existe");

            try
            {
                using var hmac = new System.Security.Cryptography.HMACSHA256();
                var user = new AppUser
                {
                    UserName = resgisterDto.UserName,
                    Email = resgisterDto.Email,
                    PasswordHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(resgisterDto.PassWord)),
                    PasswordSalt = hmac.Key
                };

                await _userRepository.InsertAsync(user);

                return new AppUserDto
                {
                    UserName = resgisterDto.UserName
                };
            }
            catch (Exception ex) when (!(ex is DuplicateUserException || ex is ArgumentException))
            {
                throw new InvalidOperationException("Error al registrar el usuario. Intente nuevamente.", ex);
            }
        }

        /// <summary>
        /// Validate if user exist
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> ValidateAppUserExist(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("El nombre de usuario no puede estar vacío");

            try
            {
                var existingUser = await _userRepository.GetUserByUserName(userName);
                return existingUser != null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al validar la existencia del usuario", ex);
            }
        }
    }
}
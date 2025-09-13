using Moq;
using AuthService.Application.Dtos;
using AuthService.Application.Interfaces;
using AuthService.Domain.Exceptions;
using System.Text;
using System.Security.Cryptography;
using AuthService.API.Controller;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Tests.UseCases
{
    public class LoginUserTest
    {
        private readonly Mock<IAppUserService> _mockAppUserService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly AccountController _accountController;

        public LoginUserTest()
        {
            _mockAppUserService = new Mock<IAppUserService>();
            _mockTokenService = new Mock<ITokenService>();
            _accountController = new AccountController(_mockAppUserService.Object, _mockTokenService.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnOkAndToken_WhenCredentialsAreValid()
        {
            // Arrange
            var password = "password";
            using var hmac = new HMACSHA256();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var passwordSalt = hmac.Key;

            var loginDto = new RegisterDto { UserName = "testuser", PassWord = password };
            var appUser = new AppUserDto { Id = "1", UserName = "testuser", PasswordHash = passwordHash, PasswordSalt = passwordSalt };
            var token = "dummy_token";

            _mockAppUserService.Setup(service => service.GetUserByUserName(loginDto.UserName)).ReturnsAsync(appUser);
            _mockTokenService.Setup(service => service.CreateToken(loginDto)).Returns(token);

            // Act
            var result = await _accountController.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var logedUser = Assert.IsType<LogedUserDto>(okResult.Value);
            Assert.Equal(token, logedUser.Token);
            Assert.Equal(appUser.UserName, logedUser.UserName);
        }

        [Fact]
        public async Task Login_ShouldThrowInvalidCredentialsException_WhenPasswordIsIncorrect()
        {
            // Arrange
            var password = "password";
            var wrongPassword = "wrong_password";
            using var hmac = new HMACSHA256();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var passwordSalt = hmac.Key;

            var loginDto = new RegisterDto { UserName = "testuser", PassWord = wrongPassword };
            var appUser = new AppUserDto { Id = "1", UserName = "testuser", PasswordHash = passwordHash, PasswordSalt = passwordSalt };

            _mockAppUserService.Setup(service => service.GetUserByUserName(loginDto.UserName)).ReturnsAsync(appUser);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidCredentialsException>(() => _accountController.Login(loginDto));
        }

        [Fact]
        public async Task Login_ShouldThrowUserNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var loginDto = new RegisterDto { UserName = "nonexistinguser", PassWord = "password" };

            _mockAppUserService.Setup(service => service.GetUserByUserName(loginDto.UserName)).ReturnsAsync((AppUserDto?)null);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _accountController.Login(loginDto));
        }
    }
}

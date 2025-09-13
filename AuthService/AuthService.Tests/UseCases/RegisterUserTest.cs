using Moq;
using Xunit;
using System.Threading.Tasks;
using AuthService.Application.Services;
using AuthService.Application.Dtos;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Models;
using AuthService.Domain.Exceptions;
using System.Text;

namespace AuthService.Tests.UseCases
{
    public class RegisterUserTest
    {
        private readonly Mock<IAppUserRepository> _mockUserRepository;
        private readonly AppUserService _appUserService;

        public RegisterUserTest()
        {
            _mockUserRepository = new Mock<IAppUserRepository>();
            _appUserService = new AppUserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task Register_ShouldCreateUser_WhenUserDoesNotExist()
        {
            // Arrange
            var registerDto = new RegisterDto { UserName = "newUser", PassWord = "password" };
            _mockUserRepository.Setup(repo => repo.GetUserByUserName(registerDto.UserName))
                               .ReturnsAsync((AppUser?)null);
            _mockUserRepository.Setup(repo => repo.InsertAsync(It.IsAny<AppUser>()))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _appUserService.Register(registerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(registerDto.UserName, result.UserName);
            _mockUserRepository.Verify(repo => repo.InsertAsync(It.IsAny<AppUser>()), Times.Once);
        }

        [Fact]
        public async Task Register_ShouldThrowDuplicateUserException_WhenUserAlreadyExists()
        {
            // Arrange
            var registerDto = new RegisterDto { UserName = "existingUser", PassWord = "password" };
            var existingUser = new AppUser { UserName = "existingUser" };
            _mockUserRepository.Setup(repo => repo.GetUserByUserName(registerDto.UserName))
                               .ReturnsAsync(existingUser);

            // Act & Assert
            await Assert.ThrowsAsync<DuplicateUserException>(() => _appUserService.Register(registerDto));
            _mockUserRepository.Verify(repo => repo.InsertAsync(It.IsAny<AppUser>()), Times.Never);
        }
    }
}

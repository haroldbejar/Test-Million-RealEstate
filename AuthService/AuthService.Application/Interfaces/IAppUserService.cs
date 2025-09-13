using AuthService.Application.Dtos;

namespace AuthService.Application.Interfaces
{
    public interface IAppUserService
    {
        Task<AppUserDto> GetUserByUserName(string userName);
        Task<AppUserDto> Register(RegisterDto resgisterDto);
        Task<bool> ValidateAppUserExist(string userName);
    }
}
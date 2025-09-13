using AuthService.Application.Dtos;

namespace AuthService.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(RegisterDto user);
    }
}
namespace AuthService.Application.Dtos
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
    }
}
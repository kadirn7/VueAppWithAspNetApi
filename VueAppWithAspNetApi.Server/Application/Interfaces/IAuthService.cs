using VueAppWithAspNetApi.Server.Application.DTOs;

namespace VueAppWithAspNetApi.Server.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseDto> Login(UserLoginDto loginDto);
        Task<UserResponseDto> Register(UserRegisterDto registerDto);
    }
} 
using VueAppWithAspNetApi.Server.Application.DTOs;
using VueAppWithAspNetApi.Server.Application.Interfaces;
using VueAppWithAspNetApi.Server.Infrastructure.Authentication;

namespace VueAppWithAspNetApi.Server.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public Task<UserResponseDto> Login(UserLoginDto loginDto)
        {
            if (!_authenticationService.ValidateCredentials(loginDto.Email, loginDto.Password))
            {
                throw new Exception("Invalid credentials");
            }

            var token = _authenticationService.GenerateBasicToken(loginDto.Email);
            
            var response = new UserResponseDto
            {
                Id = Guid.NewGuid(),
                Email = loginDto.Email,
                Username = "Test User",
                Token = token
            };

            return Task.FromResult(response);
        }

        public Task<UserResponseDto> Register(UserRegisterDto registerDto)
        {
            var token = _authenticationService.GenerateBasicToken(registerDto.Email);
            
            var response = new UserResponseDto
            {
                Id = Guid.NewGuid(),
                Email = registerDto.Email,
                Username = registerDto.Username,
                Token = token
            };

            return Task.FromResult(response);
        }
    }
} 
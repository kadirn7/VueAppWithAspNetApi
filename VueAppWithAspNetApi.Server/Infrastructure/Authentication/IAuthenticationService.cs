namespace VueAppWithAspNetApi.Server.Infrastructure.Authentication
{
    public interface IAuthenticationService
    {
        bool ValidateCredentials(string email, string password);
        string GenerateBasicToken(string email);
    }
} 
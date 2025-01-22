using System.Text;

namespace VueAppWithAspNetApi.Server.Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        // Şimdilik basit bir doğrulama - gerçek uygulamada bu veriler DB'den gelecek
        private readonly Dictionary<string, string> _users = new()
        {
            { "test@test.com", "test123" }
        };

        public bool ValidateCredentials(string email, string password)
        {
            return _users.TryGetValue(email, out var storedPassword) && storedPassword == password;
        }

        public string GenerateBasicToken(string email)
        {
            // Şimdilik basit bir token oluşturma
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{email}:{DateTime.UtcNow.Ticks}"));
        }
    }
} 
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoListApiSqlite.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace TodoListApiSqlite.Services
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string tokenKey;
        private readonly IUserRepository _userRepository;

        public JWTAuthenticationManager(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            tokenKey = configuration["TokenSecret"];
        }

        public string? Authenticate(string email, string password)
        {
            var user = _userRepository.GetUser(email);
            if (user == null || !BCryptNet.Verify(password, user.Password))
            {
                return null;
            }

            return GenerateToken(email);
        }

        public string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

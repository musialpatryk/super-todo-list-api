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

        IDictionary<string, string> users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" }
        };
        // TODO: chuj dupa zrobic tak zeby to bylo zaciagance nie 
        private readonly string tokenKey;

        private readonly IUserRepository _userRepository;

        // private readonly IConfiguration Configuration;

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

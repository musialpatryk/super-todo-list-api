using Microsoft.AspNetCore.Mvc;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;
using BCryptNet = BCrypt.Net.BCrypt;


namespace TodoListApiSqlite.Controllers
{
    [ApiController]
    [Route("/authenticate")]
    public class AuthenticationController : ControllerBase
    {
        
        private IJWTAuthenticationManager _authenticationManager;

        public AuthenticationController(IJWTAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCredentials credentials)
        {
            if (credentials.Email == null || credentials.Password == null)
            {
                return BadRequest();
            }
            var token = _authenticationManager.Authenticate(credentials.Email, credentials.Password);
            var hash1 = BCryptNet.HashPassword("password1");
            var hash2 = BCryptNet.HashPassword("password1");
            var hash3 = BCryptNet.HashPassword("password1");
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;

namespace TodoListApiSqlite.Controllers
{
    [ApiController]
    [Route("/api/authenticate")]
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
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}

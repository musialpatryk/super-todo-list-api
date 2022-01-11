using Microsoft.AspNetCore.Mvc;
using TodoListApiSqlite.Data;
using TodoListApiSqlite.Dtos;
using TodoListApiSqlite.RequestModel;
using TodoListApiSqlite.Services;
using BCryptNet = BCrypt.Net.BCrypt;


namespace TodoListApiSqlite.Controllers
{
    [ApiController]
    [Route("/authenticate")]
    public class AuthenticationController : ApiController
    {
        
        private IJWTAuthenticationManager _authenticationManager;

        public AuthenticationController(TodoListApiContext context, IJWTAuthenticationManager authenticationManager): base(context)
        {
            _authenticationManager = authenticationManager;
        }
        
        [HttpPost]
        public async Task<ActionResult<UserDto>> Authenticate([FromBody] UserCredentials credentials)
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
            var user = _context.Users.Where(u => u.Email == credentials.Email).SingleOrDefault();
            var userDto = UserDto.Create(user);
            userDto.Token = token;
            return Ok(userDto);
        }
    }
}

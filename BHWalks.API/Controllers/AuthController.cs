using BHWalks.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BHWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserrepository _userrepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserrepository userrepository,
            ITokenHandler tokenHandler)
        {
            _userrepository = userrepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Models.DTO.LoginRequest loginRequest)
        {
            if(ModelState.IsValid)
            {
                var user = await _userrepository.AuthenticateUser(
                    loginRequest.Username!, loginRequest.Password!);

                if (user != null)
                {
                    var token = await _tokenHandler.CreateToken(user);
                    return Ok(token);
                }                
            }
            return BadRequest("Username or Password is incorrect");
        }
    }
}

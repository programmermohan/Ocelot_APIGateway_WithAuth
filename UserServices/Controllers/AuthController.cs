using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserServices.Services;

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly User _user;
        private readonly ITokenManager _tokenManager;

        public AuthController(User user, ITokenManager tokenManager)
        {
            this._user = user;
            this._tokenManager = tokenManager;
        }

        [HttpPost ("Login")]
        public async Task<IActionResult> Login([FromBody] User UserModel)
        {

            if (UserModel == null)
            {
                return BadRequest("Invalid client request");
            }

            string IP = HttpContext.Request.Host.Value;
            var user = _user.GetUser(UserModel);

            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.Name, UserModel.Name),
              new Claim(ClaimTypes.Role, "Anonymous User")
            };            

            var accessToken = _tokenManager.GenerateAccessToken(claims);
            var refreshToken = _tokenManager.GenerateRefreshToken();

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------//

    }
}

using Core.Dtos.Public;
using Core.Interfaces.Public;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Servicios_Zeus.Controllers.Public
{
    [EnableCors("CorsPolicy")]
    [ApiVersion("1.0")]
    [Route("api/login")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class LoginController : ControllerBase
    {
        IUserLoginRepository _userLoginRepository;
        public LoginController(IUserLoginRepository userService)
        {
            _userLoginRepository = userService;
        }
        [Route("GetAccessUser")]
        [HttpPost]
        public async Task<ActionResult<List<UserLoginDto>>> GetAccessUser(UserNameDto userNameDto)
        {
            var result = await _userLoginRepository.GetAccessUser(userNameDto);

            if (result == null)
            {
                return NoContent();
            }
            else if(result.EstaAutenticado==true)
            {
                SetRefreshTokenInCookie(result.RefreshToken);
            }
            
            return Ok(result);
        }

        [HttpPost("GetRefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _userLoginRepository.GetRefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        private void SetRefreshTokenInCookie(string refreshtoken)
        {
            var cookieOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(1)
            };
            Response.Cookies.Append("refreshtoken", refreshtoken, cookieOption);


        }


    }
}

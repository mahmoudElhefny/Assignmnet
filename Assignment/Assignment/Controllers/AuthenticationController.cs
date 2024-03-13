//using Assignment.Service.Contracts.IServices;
//using Assignment.Service.Services;
//using Assignment.Service.ViewModels;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;


//namespace Assignment.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthenticationController : ControllerBase
//    {
//        private readonly AuthService _authService;
//        public AuthenticationController(IAuthService authService)
//        {
//            _authService = authService;
//        }
//        [HttpPost("register")]
//        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
//        {
           
//            var result = await _authService.RegisterAsync(model);
//            if (result.IsSucceded)
//            {
//                SetRefreshTokenInCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);
//                return Ok(result);
//            }
//            return BadRequest(result);
//        }

//        [HttpPost("Login")]
//        public async Task<IActionResult> LoginAsync( LoginViewModel model)
//        {
           
//            var result = await _authService.LoginAsync(model);

//            if (!result.IsSucceded)
//                return BadRequest(result);
//            if (!string.IsNullOrEmpty(result.Data.RefreshToken))
//                SetRefreshTokenInCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);
//            return Ok(result);
//        }

//        [HttpGet("refreshToken")]
//        public async Task<IActionResult> RefreshToken()
//        {
//            var refreshToken = Request.Cookies["refreshToken"];

//            var result = await _authService.RefreshTokenAsync(refreshToken);

//            if (!result.IsSucceded)
//                return BadRequest(result);

//            SetRefreshTokenInCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);

//            return Ok(result);
//        }

//        [HttpPost("revokeToken")]
//        public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
//        {
//            var token = model.Token ?? Request.Cookies["refreshToken"];

//            if (string.IsNullOrEmpty(token))
//                return BadRequest("Token is required!");

//            var result = await _authService.RevokeTokenAsync(token);

//            if (!result.IsSucceded)
//                return BadRequest("Token is invalid!");

//            return Ok();
//        }

//        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
//        {
            
//            var cookieOptions = new CookieOptions
//            {
//                HttpOnly = true,
//                Expires = expires.ToLocalTime(),
//                Secure = true, 
//                IsEssential = true,
//                SameSite = SameSiteMode.None
//            };
//            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
//        }
//    }
//}


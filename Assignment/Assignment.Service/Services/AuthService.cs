using Assignment.Domain.ViewModels;
using Assignment.Infrastructure.Entities;
using Assignment.Infrastructure.Models.Entities;
using Assignment.Service.Helpers;
using Assignment.Service.Validators;
using Assignment.Service.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Assignment.Service.Services
{
    public class AuthService //: IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> userManger, IMapper mapper,
            IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManger = userManger;
            _mapper = mapper;
            _jwt = jwt.Value;
            _roleManager = roleManager;
        }

        public async Task<Response<AuthenticationViewModel>> LoginAsync(LoginViewModel model)
        {
            Response<AuthenticationViewModel> response = new();
            response.Data = new AuthenticationViewModel();
            response.Errors = ValidatorHandler.Validate(model, (LoginVmValidator)Activator.CreateInstance(typeof(LoginVmValidator)));
            if (response.Errors != null)
                throw new InvalidViewModelException(response.Errors);
            var user = await _userManger.FindByNameAsync(model.UserName);

            if (user is null || !await _userManger.CheckPasswordAsync(user, model.Password))
            {
                response.IsSucceded = false;
                response.Data = null;
                response.Errors = new() { new Error() { ErrorMessage = "Email or Password is incorrect!" } };
                return response;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManger.GetRolesAsync(user);
            response.IsSucceded = true;
            response.Errors = null;
            response.Data.IsAuthenticated = true;
            response.Data.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Data.Email = user.Email;
            response.Data.Username = user.UserName;
            response.Data.ExpiresOn = jwtSecurityToken.ValidTo;
            response.Data.Roles = rolesList.ToList();
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                response.Data.RefreshToken = activeRefreshToken.Token;
                response.Data.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                response.Data.RefreshToken = refreshToken.Token;
                response.Data.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await _userManger.UpdateAsync(user);
            }
            return response;
        }

        private Infrastructure.Entities.RefreshToken GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<AuthenticationViewModel>> RegisterAsync(RegisterViewModel userRegisterVM)
        {
            Response<AuthenticationViewModel> response = new();
            //response.Data = new AuthenticationViewModel();
            response.Errors = ValidatorHandler.Validate(userRegisterVM, (RegisterViewModelValidator)Activator.CreateInstance(typeof(RegisterViewModelValidator)));
            if (response.Errors != null)
            {
                response.Data = null;
                response.IsSucceded = false;
                //response.Errors=new List<Error>()
                // throw new InvalidViewModelException(response.Errors);
                return response;
            }

            //check if user has email registered before
            if (await _userManger.FindByEmailAsync(userRegisterVM.EmailAddress) is not null)
            {
                response.IsSucceded = false;
                response.Data = null;
                response.Errors = new List<Error>()
                {
                    new Error { ErrorMessage = "this Email already exit" }
                };
                return response;
            }

            //check if any user uses the same UserName
            else if (await _userManger.FindByNameAsync(userRegisterVM.UserName) is not null)
            {
                response.IsSucceded = false;
                response.Data = null;
                response.Errors = new List<Error>()
                {
                    new Error { ErrorMessage = "this user name already exits" }
                };
                return response;
            }
            else
            {

                ApplicationUser user = _mapper.Map<ApplicationUser>(userRegisterVM);
                //create user in database
                IdentityResult result = await _userManger.CreateAsync(user, userRegisterVM.Password);
                if (!result.Succeeded)
                {
                    response.IsSucceded = false;
                    response.Data = null;
                    response.Errors = new();
                    //response.Errors.AddRange(result.Errors.Select(error => new Error { ErrorMessage = error.Description }));
                    foreach (var error in result.Errors)
                    {
                        if (!string.IsNullOrEmpty(error.Description))
                        {
                            response.Errors.Add(new Error { ErrorMessage = error.Description });
                        }
                        else
                        {
                            response.Errors.Add(new Error { ErrorMessage = "An unknown error occurred." });
                        }
                    }
                    return response;
                }

                await _userManger.AddToRoleAsync(user, Roles.USER_ROLE);

                var jwtSecurityToken = await CreateJwtToken(user);

                var refreshToken = GenerateRefreshToken();
                user.RefreshTokens?.Add(refreshToken);
                await _userManger.UpdateAsync(user);
                response.IsSucceded = true;
                response.Errors = null;
                response.Data =
                 new AuthenticationViewModel
                 {
                     Email = user.Email,
                     ExpiresOn = jwtSecurityToken.ValidTo,
                     IsAuthenticated = true,
                     Roles = new List<string> { "User" },
                     Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                     Username = user.UserName,
                     RefreshToken = refreshToken.Token,
                     RefreshTokenExpiration = refreshToken.ExpiresOn
                 };
                return response;
            }
        }
        public async Task<Response<AuthenticationViewModel>> RefreshTokenAsync(string token)
        {

            Response<AuthenticationViewModel> response = new();

            var user = await _userManger.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                response.IsSucceded = false;
                response.Data.Message = null;
                response.Errors = new() { new Error() { ErrorMessage = "Invalid token" } };
                return response;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                response.IsSucceded = false;
                response.Data.Message = null;
                response.Errors = new() { new Error() { ErrorMessage = "Inactive token" } };
                return response;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManger.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);
            response.IsSucceded = true;
            response.Data.IsAuthenticated = true;
            response.Data.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            response.Data.Email = user.Email;
            response.Data.Username = user.UserName;
            var roles = await _userManger.GetRolesAsync(user);
            response.Data.Roles = roles.ToList();
            response.Data.RefreshToken = newRefreshToken.Token;
            response.Data.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return response;
        }

        public async Task<Response<bool>> RevokeTokenAsync(string token)
        {
            Response<bool> response = new();
            var user = await _userManger.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                response.IsSucceded = false;
                response.Data = false;
                return response;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                response.IsSucceded = false;
                response.Data = false;
                return response;
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManger.UpdateAsync(user);
            response.Data = true;
            response.IsSucceded = true;
            return response;
        }



        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManger.GetClaimsAsync(user);
            var roles = await _userManger.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.secret));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.ValidIssuer,
                audience: _jwt.ValidAudiance,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        //private RefreshToken GenerateRefreshToken()
        //{
        //    var randomNumber = new byte[32];

        //    using var generator = new RNGCryptoServiceProvider();

        //    generator.GetBytes(randomNumber);

        //    return new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(randomNumber),
        //        ExpiresOn = DateTime.UtcNow.AddMinutes(1),
        //        CreatedOn = DateTime.UtcNow
        //    };
        //}
    }
}


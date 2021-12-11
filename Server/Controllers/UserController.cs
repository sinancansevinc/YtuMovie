using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> logger;

        // api/user

        public UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration,ILogger<UserController> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.logger = logger;
        }

        // api/user/register
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            logger.LogInformation("Register method is called");
           
            string userName = user.Email;
            string password = user.Password;

            IdentityUser identityUser = new IdentityUser
            {
                Email = userName,
                UserName = userName
            };

            IdentityResult identityresult = await userManager.CreateAsync(identityUser, password);

            if (identityresult.Succeeded)
            {
                return Ok(new { identityresult.Succeeded });
            }
            else
            {
                string returnError = "Register failed with the following errors";

                foreach (var error in identityresult.Errors)
                {
                    returnError += Environment.NewLine;
                    returnError += $"Error Code: {error.Code} - {error.Description}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, returnError);

            }

        }

        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] User user)
        {
            logger.LogError("User login process started");
            string userName = user.Email;
            string password = user.Password;

            SignInResult signInResult = await signInManager.PasswordSignInAsync(userName, password, false, false);

            if (signInResult.Succeeded)
            {
                IdentityUser identityUser = await userManager.FindByNameAsync(userName);
                string jwtString = await GenerateJsonWebToken(identityUser);

                return Ok(jwtString);
            }
            else
            {
                return Unauthorized(user);
            }

        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<string> GenerateJsonWebToken(IdentityUser identityUser)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //Claims

            List<Claim> claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub,identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,identityUser.Id)
            };

            IList<string> roleNames = await userManager.GetRolesAsync(identityUser);

            claims.AddRange(roleNames.Select(roleName => new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
                configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.User;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/ApplicationUser")]
    public class AuthenticationController : ControllerBase
    {

        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationSettings appSettings;
        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
        }
        [HttpPost]
        [Route("login")]

        //POST : /api/ApplicationUser/login
        public async Task<IActionResult> Login(LoginDTO user)
        {

            var users = await userManager.FindByNameAsync(user.UserName);
            if (users != null && await userManager.CheckPasswordAsync(users, user.Password))
            {
                //get role assign to user
                var role = await userManager.GetRolesAsync(users);
                IdentityOptions options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",users.Id.ToString()),
                        new Claim(options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                // Console.Write("hello wordl");
                return Ok(new { token ,role});
                //return Ok(securityToken);

            }
            else
            {
                return BadRequest(new { message = "UserName or password is incorrect" });
            }

        }

        
        }
    }

   
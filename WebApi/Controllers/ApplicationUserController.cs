using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.User;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/Application/")]

    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly DataContext dataContext;
        public ApplicationUserController(UserManager<ApplicationUser> userManager, DataContext dataContext, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dataContext = dataContext;
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<Object>CreateUser(ApplicationUserDTO user)
        {
            user.Role = "Member";
            var applicationUser = new ApplicationUser()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
            try
            {
                var result = await userManager.CreateAsync(applicationUser, user.Password);
                await userManager.AddToRoleAsync(applicationUser, user.Role);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
       //To Do
       // create Httpget and HttpDelete
    }
}
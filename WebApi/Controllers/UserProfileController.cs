using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.User;

namespace WebApi.Controllers
{
    [Route("api/controllers")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

       [HttpGet]
      //[Authorize("member")]
        //get :/api/UserProfile
        public async Task<object> GetUserProfile()
        {
            string userId = User.Claims.First(c=>c.Type == "UserID").Value;
            var user =await userManager.FindByIdAsync(userId);
            return new
            {
                user.Id,
                user.UserName,
                user.FullName,
                user.PhoneNumber,
                user.Email,
               
                
            };
        }
        // test code for role authorization in frontend
        [HttpGet]
        [Authorize(Roles ="Admin")]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "you are admin,so you are able to modify the data from this page";

        }
        [HttpGet]
        [Authorize(Roles = "Member")]
        [Route("ForMember")]
        public string GetFormember()
        {
            return "when member login they need to complete form to start business";
        }
    }
}
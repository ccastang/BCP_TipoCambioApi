using BCP_TipoCambio.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }


        [HttpGet]
        public async Task<ActionResult> Register()
        {
            return NotFound();
        }

        //[HttpPost, ActionName("Login")]
        //public async Task<ActionResult> Login([FromBody] ApplicationUser entity)
        //{
        //    var user = await _userManager.FindByNameAsync(entity.UserNam);
        //    if (user != null)
        //    {
        //        var Signin = await _signInManager.PasswordSignInAsync(user, entity.Password, false, false);
        //        if (Signin.Succeeded)
        //        {
        //            return Ok();

        //        }
        //    }
        //    return Ok();
        //}


        [HttpPost, ActionName("Register")]
        public async Task<ActionResult> Register([FromBody] ApplicationUser entity)
        {
            var user = new ApplicationUser
            {
                Id = "1001",
                UserName = entity.UserNam,
                Password = entity.Password
            };

            var result = await _userManager.CreateAsync(user, user.Password);
            if (result.Succeeded)
            {
                var SignIn = await _signInManager.PasswordSignInAsync(user, user.Password, false, false);
                if (SignIn.Succeeded)
                {
                    //return RedirectToAction("Index");
                    return Ok();
                }
            }
            return Ok();
        }
    }
}

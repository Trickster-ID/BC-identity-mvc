using IdentityMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Microsoft.Owin.Security;


namespace IdentityMVC.Controllers
{
    public class LRController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: LR
        newConn conn2 = new newConn();
        public LRController()
        {

        }
        public LRController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Login(string returnUrl)
        {
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Home", new { username = Session["username"].ToString() });
            }
            else
            {

            }
            if (Session["Id"] != null)
            {
                return Redirect("/Home/Index#cuk");
                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
                //return RedirectToAction("Login", "Account");
            }
            //return View();
            //return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginUserVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                switch (result)
                {
                    case SignInStatus.Success:
                        
                        Session["Id"] = model.Email;
                        return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login2", "Account");
        }
        //[AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterUserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //result = UserManager.AddToRole(user.Id, "Admin");
                    //Roles.AddUserToRole(user.UserName, "Admin");
                    UserManager.AddToRole(user.Id, "Admin");

                    Session["Id"] = model.Email;
                    var supp = new Supplier { Email = model.Email, Name = model.Name, Address = model.Address, City = model.City, State = model.State, Country = model.Country, Zipcode = model.Zipcode };
                    conn2.Suppliers.Add(supp);
                    conn2.SaveChanges();
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    return RedirectToAction("Index", "Supplier");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session["Id"] = null;
            return RedirectToAction("Login", "LR");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}
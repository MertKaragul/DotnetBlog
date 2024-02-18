using Microsoft.AspNetCore.Mvc;
using NLayerBlog.Models.UserViewModel;

namespace NLayerBlog.Controllers.User {
    public class UserController : Controller {


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                return Json("User successfully sign in");
            }
            else
            {
                return Json("Check your form");
            }
        }
    }
}

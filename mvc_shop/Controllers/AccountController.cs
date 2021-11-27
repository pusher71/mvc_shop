using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using mvc_shop.Models.ViewModels;

namespace mvc_shop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signinManager;
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr)
        {
            userManager = userMgr;
            signinManager = signinMgr;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user =
                await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signinManager.SignOutAsync();
                    var sr = await signinManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if ((await signinManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {

                        // здесь проверка на костыльную или более-менее нормальную, но тоже не очень, покупку
                        if (TempData.ContainsKey("CategoryId") && TempData.ContainsKey("ProductId") && TempData.ContainsKey("Quantity"))
                            return RedirectToAction("BuyProductGet", "Categories");

                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
            }
            ModelState.AddModelError("", "Неправильное имя или пароль");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signinManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}

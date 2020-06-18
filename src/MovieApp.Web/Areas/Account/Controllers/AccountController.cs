using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Web.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieApp.Web.Areas.Account.Models;

namespace MovieApp.Web.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly ApplicationRegisterModel _auc;
        

        public AccountController(ApplicationRegisterModel auc)
        {
            _auc = auc;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult Register(RegisterModel model)
        {
            _auc.Add(model);
            _auc.SaveChanges();
            ViewBag.message = "The user " + model.Username + " is saved succesfully";
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
       [HttpPost]
       [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            var account = _auc.Users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
            if (account != null)
            {
                
                HttpContext.Session.SetString("Username", account.Username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("","Invalid username or password");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}


using AdmissionRegistrationSystem.Data;
using AdmissionRegistrationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionRegistrationSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ARSDBContext _context;

        public LoginController(ARSDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(LoginModel login) {

            var authenticate = await _context.Logins.FirstOrDefaultAsync(
                m => m.UserName == login.UserName && m.Password == login.Password);
            if (authenticate == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

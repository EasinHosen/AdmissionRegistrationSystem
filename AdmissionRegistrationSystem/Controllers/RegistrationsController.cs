using Microsoft.AspNetCore.Mvc;

namespace AdmissionRegistrationSystem.Controllers
{
    public class RegistrationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

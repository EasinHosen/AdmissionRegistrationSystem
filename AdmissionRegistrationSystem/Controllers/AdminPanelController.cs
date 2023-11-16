using Microsoft.AspNetCore.Mvc;

namespace AdmissionRegistrationSystem.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

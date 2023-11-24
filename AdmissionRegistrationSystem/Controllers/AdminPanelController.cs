using AdmissionRegistrationSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdmissionRegistrationSystem.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ARSDBContext _context;

        public AdminPanelController(ARSDBContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Registrations != null ? View(await _context.Registrations.ToListAsync()) : Problem("No data found!!");


        }
    }
}

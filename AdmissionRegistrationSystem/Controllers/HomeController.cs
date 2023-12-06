using AdmissionRegistrationSystem.Data;
using AdmissionRegistrationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdmissionRegistrationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ARSDBContext _context;


        public HomeController(ARSDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewApplication()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string Svalue) {
            string type = DetermineInputType(Svalue);
            //Debug.Print(type);
            RegistrationModel reg = new RegistrationModel();
            if (type == "Guid")
            {
                reg = await _context.Registrations.FirstOrDefaultAsync(
                    e => e.regId == Guid.Parse(Svalue)) ?? new RegistrationModel();
            }
            if (type == "Phone")
            {
                reg = await _context.Registrations.FirstOrDefaultAsync(
                    e => e.Phone == Svalue) ?? new RegistrationModel();
            }
            if (type == "Email") {
                reg = await _context.Registrations.FirstOrDefaultAsync(
                    e => e.Email == Svalue) ?? new RegistrationModel();
            }
            if (type == "Unknown")
            {
                Debug.Print("Reg not found");
            }
            else { 
                Debug.Print("Reg found: " + reg.Name);
            }

            return View("ViewApplication");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string DetermineInputType(string input)
        {
            if (Guid.TryParse(input, out _))
            {
                return "Guid";
            }

            if (IsPhoneNumber(input))
            {
                return "Phone";
            }

            if (IsEmailAddress(input))
            {
                return "Email";
            }

            return "Unknown";
        }

        private bool IsPhoneNumber(string input)
        {
            var phoneRegex = new Regex(@"^\d{11}$");
            return phoneRegex.IsMatch(input);
        }

        private bool IsEmailAddress(string input)
        {
            var emailRegex = new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$");
            return emailRegex.IsMatch(input);
        }
    }
}
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
        public async Task<IActionResult> Search(string Svalue)
        {
            string type = DetermineInputType(Svalue);
            Debug.Print(type);

            RegistrationModel reg;
            AdminViewDataModel viewDataModel = new AdminViewDataModel();

            if (type == "Guid")
            {
                reg = await _context.Registrations.Include(r => r.PermAddress).Include(r => r.PresAddress).Include(s=> s.SSC).Include(h => h.HSC)
                    .FirstOrDefaultAsync(e => e.regId == Guid.Parse(Svalue)) ?? new RegistrationModel();
                var paymentInfo = await _context.PaymentInfos.FirstOrDefaultAsync(p => p.RegistrationId == reg.Id);
                if (paymentInfo != null)
                {
                    viewDataModel.paymentStatus = paymentInfo.paymentStatus;
                    viewDataModel.registrations = reg;
                }
                else {
                    viewDataModel.paymentStatus = "Pending";
                    viewDataModel.registrations = reg;
                }

            }
            else if (type == "Phone")
            {
                reg = await _context.Registrations.Include(r => r.PermAddress).Include(r => r.PresAddress).Include(s => s.SSC).Include(h => h.HSC)
                    .FirstOrDefaultAsync(e => e.Phone == Svalue) ?? new RegistrationModel();
                var paymentInfo = await _context.PaymentInfos.FirstOrDefaultAsync(p => p.RegistrationId == reg.Id);
                if (paymentInfo != null)
                {
                    viewDataModel.paymentStatus = paymentInfo.paymentStatus;
                    viewDataModel.registrations = reg;
                }
                else
                {
                    viewDataModel.paymentStatus = "Pending";
                    viewDataModel.registrations = reg;
                }
            }
            else if (type == "Email")
            {
                reg = await _context.Registrations.Include(r => r.PermAddress).Include(r => r.PresAddress).Include(s => s.SSC).Include(h => h.HSC)
                    .FirstOrDefaultAsync(e => e.Email == Svalue) ?? new RegistrationModel();
                var paymentInfo = await _context.PaymentInfos.FirstOrDefaultAsync(p => p.RegistrationId == reg.Id);
                if (paymentInfo != null)
                {
                    viewDataModel.paymentStatus = paymentInfo.paymentStatus;
                    viewDataModel.registrations = reg;
                }
                else
                {
                    viewDataModel.paymentStatus = "Pending";
                    viewDataModel.registrations = reg;
                }
            }
            else
            {
                // Invalid input type
                return BadRequest("Invalid input. Please enter a valid Registration ID, Phone number, or Email.");
            }

            // Check if RegistrationModel is not found
            if (reg == null || reg.Id == 0)
            {
                // Data not found
                return NotFound("Data not found for the given input.");
            }

            return View("ApplicationDetails", viewDataModel);
        }


        /*public async Task<IActionResult> ApplicationDetails(int id)
        {
            Debug.Print("appdet");
            Debug.Print(id.ToString());
            var reg = await _context.Registrations.Include(r => r.PermAddress).Include(r => r.PermAddress)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reg != null)
            {
                Debug.Print(reg.Name);
                return View(reg);
            }
            
            return NotFound();
        }*/

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

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
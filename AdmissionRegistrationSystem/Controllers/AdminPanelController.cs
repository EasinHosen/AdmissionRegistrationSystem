using AdmissionRegistrationSystem.Data;
using AdmissionRegistrationSystem.Migrations;
using AdmissionRegistrationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Diagnostics;

namespace AdmissionRegistrationSystem.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ARSDBContext _context;

        public AdminPanelController(ARSDBContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index(String userType)
        {
            if (userType == null)
            {
                return Unauthorized();
            }
            var rList = await _context.Registrations.ToListAsync();

            List<AdminViewDataModel> dataList = new List<AdminViewDataModel>();
            for (int i = 0; i < rList.Count; i++)
            {
                var dataM = new AdminViewDataModel();
                dataM.registrations = rList[i];

                var hasPayment = _context.PaymentInfos.Any(p => p.RegistrationId == rList[i].Id);
                if (hasPayment)
                {
                    dataM.paymentStatus = "Completed";
                    var item = await _context.PaymentInfos.FirstOrDefaultAsync(e=>e.RegistrationId == rList[i].Id);
                    dataM.transaactionId = item.transactionId;
                }
                else
                {
                    dataM.paymentStatus = "Pending";
                    dataM.transaactionId = null;
                }
                dataList.Add(dataM);
            }

            return _context.Registrations != null ? View(dataList) : Problem("No data found!!");

        }

        public async Task<IActionResult> Details(int? id) {
            RegistrationModel reg;
            AdminViewDataModel viewDataModel = new AdminViewDataModel();

                reg = await _context.Registrations.Include(r => r.PermAddress).Include(r => r.PresAddress).Include(s => s.SSC).Include(h => h.HSC).FirstOrDefaultAsync(e => e.Id == id) ?? new RegistrationModel();
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

                if (reg == null || viewDataModel == null) {
                return NotFound();
            }
            return View("DetailsView", viewDataModel);
        }
    }
}

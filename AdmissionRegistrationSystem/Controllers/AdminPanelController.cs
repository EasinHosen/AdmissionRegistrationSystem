using AdmissionRegistrationSystem.Data;
using AdmissionRegistrationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}

using AdmissionRegistrationSystem.Data;
using AdmissionRegistrationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;

namespace AdmissionRegistrationSystem.Controllers
{
    public class RegistrationsController : Controller
    {

        private readonly ARSDBContext _context;

        public RegistrationsController(ARSDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(RegistrationModel registrationModel) {

            registrationModel.regId = Guid.NewGuid();
            registrationModel.PresAddress = registrationModel.PermAddress;
            registrationModel.PresAddress.AddressType = "Present";
            registrationModel.PermAddress.AddressType = "Permanent";
            registrationModel.SSC.ExamType = "SSC";
            registrationModel.HSC.ExamType = "HSC";
            registrationModel.PhotoUrl = "https://firebasestorage.googleapis.com/v0/b/let-s-chat-16cb3.appspot.com/o/ars%2Fplaceholder.png?alt=media&token=58df47c8-2773-4f21-8f60-de7b029e1280";

            Debug.Print(registrationModel.ToJson());
            /*Debug.Print("start");
            Debug.Print(
               registrationModel.ToJson());
            if (ModelState.IsValid)
            {
                if (registrationModel == null)
                {
                    Debug.Print("null");
                }
                Debug.Print("valid");

                Debug.Print(registrationModel.ToJson());
                Debug.Print(registrationModel.Name);

            }*/
            return View();
        }
    }
}

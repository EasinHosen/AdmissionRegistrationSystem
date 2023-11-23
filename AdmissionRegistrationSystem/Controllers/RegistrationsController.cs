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

        /*[HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                // Process the uploaded image
                var fileName = Path.GetFileName(imageFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                imageFile.SaveAs(path);

                // Save the image file information to the database or perform any necessary processing
                // ...

                return RedirectToAction("Index"); // Redirect to the index action after successful upload
            }
            else
            {
                // Handle any errors or invalid file uploads
                return View("Error"); // Redirect to an error page if no file was uploaded
            }
        }*/

        [HttpPost]
        public ActionResult ProcessImage()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];

                // Process the file as needed
                // For example, save the file or perform other operations
                Debug.Print("got the file!");
                
                return Json(new { success = true, message = "Image processed successfully", url= "https://firebasestorage.googleapis.com/v0/b/let-s-chat-16cb3.appspot.com/o/ars%2Fplaceholder.png?alt=media&token=58df47c8-2773-4f21-8f60-de7b029e1280" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error processing image: {ex.Message}" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> IndexAsync(RegistrationModel registrationModel) {

            Debug.Print("start");

            if (ModelState.IsValid)
            {
                if (registrationModel == null)
                {
                    Debug.Print("null");
                }
                Debug.Print("valid");

               Debug.Print(registrationModel.ToJson());

            }
            else {
                Debug.Print("Invalid");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Debug.Print($"Error: {error.ErrorMessage}");
                    }
                }

            }
            return View();
        }
    }
}

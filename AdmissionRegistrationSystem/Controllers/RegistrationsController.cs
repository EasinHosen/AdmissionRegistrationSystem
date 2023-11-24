using AdmissionRegistrationSystem.Data;
using AdmissionRegistrationSystem.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;

namespace AdmissionRegistrationSystem.Controllers
{
    public class RegistrationsController : Controller
    {

        private readonly ARSDBContext _context;

        private static string ApiKey = "AIzaSyCP29mIrHZQLZqHUcyaRIAhO3r7hb-EmH8";
        private static string Bucket = "let-s-chat-16cb3.appspot.com";
        private static string AuthEmail = "ars@test.com";
        private static string AuthPassword = "1212345";

        private string ImageUrl = "";
        private Guid? rId;

        public RegistrationsController(ARSDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success(Guid? rId) {
            ViewData["RegId"] = rId;

            if (rId == null)
            {
                return RedirectToAction("Error");
            }
            else { 
                return View();       
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        ///takes the image from ui to process
        [HttpPost]
        public async Task<ActionResult> ProcessImage()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];

                Debug.Print("got the file!");

                FileStream stream = await ConvertIFormFileToFileStream(file);
                Debug.Print("converted");

                ImageUrl = await UploadFile(stream, file.FileName);
                Debug.Print("uploaded: "+ImageUrl);

                return Json(new { success = true, message = "Image processed successfully", url= ImageUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error processing image: {ex.Message}" });
            }
        }

        ///converts the image to file stream
        public async Task<FileStream> ConvertIFormFileToFileStream(IFormFile file)
        {
            // Create a temporary file.
            string tempFilePath = Path.GetTempFileName();

            // Save the uploaded file to the temporary file.
            using (FileStream tempFileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(tempFileStream);
            }

            // Open the temporary file for reading.
            FileStream fileStream = new FileStream(tempFilePath, FileMode.Open);

            // Return the file stream.
            return fileStream;
        }
         
        ///uploads the file to the firebase storage
        public async Task<string> UploadFile(FileStream stream, string fileName)
        {
            string link = "";
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(Bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            }).Child("ars").Child(fileName).PutAsync(stream, cancellation.Token);
            try
            {
                link = await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return link;
        }


        ///submit button action
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
                _context.Add(registrationModel);
                await _context.SaveChangesAsync();
                /*return RedirectToAction("Home","Index");*/
                return RedirectToAction("Success", new { rId = registrationModel.regId});


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
            return RedirectToAction("Error");
        }
    }
}

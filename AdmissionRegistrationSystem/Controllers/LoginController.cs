using AdmissionRegistrationSystem.Data;
using AdmissionRegistrationSystem.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AdmissionRegistrationSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ARSDBContext _context;

        /*private static string ApiKey = "AIzaSyCP29mIrHZQLZqHUcyaRIAhO3r7hb-EmH8";
        private static string Bucket = "let-s-chat-16cb3.appspot.com";
        private static string AuthEmail = "ars@test.com";
        private static string AuthPassword = "1212345";*/

        public LoginController(ARSDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(LoginModel login/*, IFormFile file*/) {

            /*FileStream stream;
            if(file.Length > 0)
            {
               //tring path = Path
            }*/

            var authenticate = await _context.Logins.FirstOrDefaultAsync(
                m => m.UserName == login.UserName && m.Password == login.Password);
            if (authenticate == null)
            {
                return View();
            }
            //Debug.Print(authenticate.ToString());
            return RedirectToAction("Index", "Home");
        }

        /*public async void UploadFile(FileStream stream, string fileName) {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(Bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            }).Child("ars").Child(fileName).PutAsync(stream, cancellation.Token);
            try {
                string link = await task;
            }catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
        }*/
    }
}

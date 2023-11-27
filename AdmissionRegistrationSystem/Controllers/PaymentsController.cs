using AdmissionRegistrationSystem.Data;
using AdmissionRegistrationSystem.PaymentGateway;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nancy.Bootstrapper;
using NuGet.Protocol;
using System.Collections.Specialized;
using System.Diagnostics;

namespace AdmissionRegistrationSystem.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ARSDBContext _context;
        protected string registrationID;

        public PaymentsController(ARSDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaymentGateway(string rId)
        {
            var productName = "Admission Registration Fee";
            var price = 650;

            var baseUrl = Request.Scheme + "://" + Request.Host;

            NameValueCollection PostData = new NameValueCollection();

            Guid tId = Guid.NewGuid();

            string tran_id = tId.ToString();

            PostData.Add("total_amount", $"{price}");
            PostData.Add("tran_id", tran_id);
            PostData.Add("success_url", baseUrl + "/Payments/PaymentConfirmation?rId="+rId);
            PostData.Add("fail_url", baseUrl + "/Payments/PaymentFail");
            PostData.Add("cancel_url", baseUrl + "/Payments/PaymentCancel");

            PostData.Add("version", "3.00");
            PostData.Add("cus_name", rId);
            PostData.Add("cus_email", "abc.xyz@mail.co");
            PostData.Add("cus_add1", "Address Line On");
            PostData.Add("cus_add2", "Address Line Tw");
            PostData.Add("cus_city", "City Nam");
            PostData.Add("cus_state", "State Nam");
            PostData.Add("cus_postcode", "Post Cod");
            PostData.Add("cus_country", "Countr");
            PostData.Add("cus_phone", "0111111111");
            PostData.Add("cus_fax", "0171111111");
            PostData.Add("ship_name", "ABC XY");
            PostData.Add("ship_add1", "Address Line On");
            PostData.Add("ship_add2", "Address Line Tw");
            PostData.Add("ship_city", "City Nam");
            PostData.Add("ship_state", "State Nam");
            PostData.Add("ship_postcode", "Post Cod");
            PostData.Add("ship_country", "Countr");
            PostData.Add("value_a", "ref00");
            PostData.Add("value_b", "ref00");
            PostData.Add("value_c", "ref00");
            PostData.Add("value_d", "ref00");
            PostData.Add("shipping_method", "NO");
            PostData.Add("num_of_item", "1");
            PostData.Add("product_name", $"{productName}");
            PostData.Add("product_profile", "general");
            PostData.Add("product_category", "Demo");

            var storeId = "wizar64b2b3a882baf";
            var storePassword = "wizar64b2b3a882baf@ssl";
            var isSandboxMood = true;

            SSLCommerzGatewayProcessor sslcz = new SSLCommerzGatewayProcessor(storeId, storePassword, isSandboxMood);
            string response = sslcz.InitiateTransaction(PostData);
            return Redirect(response);
        }

        public IActionResult PaymentConfirmation(string rId)
        {
            Debug.Print("Rid in confirmation: "+rId);
            if (!(!String.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID"))
            {
                ViewBag.SuccessInfo = "There some error while processing your payment. Please try again.";
                return View();
            }

            string TrxID = Request.Form["tran_id"];
            //string registrationId = regId;

            string amount = "650";
            string currency = "BDT";

//            Debug.Print("Request: " + _rId);

            var storeId = "wizar64b2b3a882baf";
            var storePassword = "wizar64b2b3a882baf@ssl";

            SSLCommerzGatewayProcessor sslcz = new SSLCommerzGatewayProcessor(storeId, storePassword, true);
            var resonse = sslcz.OrderValidate(TrxID, amount, currency, Request);
            var successInfo = $"Validation Response: {resonse}";
            ViewBag.SuccessInfo = successInfo;

            return View();
        }
        public IActionResult PaymentFail()
        {
            ViewBag.FailInfo = "There some error while processing your payment. Please try again.";
            return View();
        }
        public IActionResult PaymentCancel()
        {
            ViewBag.CancelInfo = "Your payment has been cancel";
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> IndexAsync(string regId)
        {
            if (Guid.TryParse(regId, out _))
            {
                registrationID = regId;
                Guid id = new Guid(regId);
                var authenticate = await _context.Registrations.FirstOrDefaultAsync(
                     m => m.regId == id);
                if (authenticate != null)
                {
                    Debug.Print("Id matched!");
                    return RedirectToAction("PaymentGateway", new { rId = regId});
                }
                else
                {
                    Debug.Print("Id did not matched!");
                }
            }
            else {
                Debug.Print("invalid reg Id");
            }
            
            return View();
        }
    }
}

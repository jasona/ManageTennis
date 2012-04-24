using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Models;
using System.Configuration;
using AuthorizeNet;
using Oasis.Lib.Billing;
using Oasis.Lib.Util;

namespace Oasis.Lib.Controllers
{
    public class PaymentController : Controller
    {

        public ActionResult DisplayForm()
        {
            return View();
        }

        [ActionName("submit-payment")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitPayment(string Amount, string Description, string FirstName, string LastName, string Address1, string Address2, string City, string State, string ZipCode,
                string CCNumber, string CCType, string ExpirationMonth, string ExpirationYear)
        {
            string response = string.Empty;
            decimal amount;
            PaymentModel model = new PaymentModel();


            // TODO Refactor this into a Service
            if (decimal.TryParse(Amount, out amount)) model.Amount = amount;
            model.Description = Description;
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.Address1 = Address1;
            model.Address2 = Address2;
            model.City = City;
            model.State = State;
            model.ZipCode = ZipCode;
            model.CCNumber = CCNumber;
            model.CCType = CCType;
            model.ExpirationMonth = ExpirationMonth;
            model.ExpirationYear = ExpirationYear;

            PaymentProvider payment = new PaymentProvider();
            PaymentRequest request = new PaymentRequest();

            request.Amount = Convert.ToDouble(amount);
            request.ApiLoginId = ConfigurationManager.AppSettings["ApiLoginId"];
            request.ApiTransactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            
            request.FirstName = FirstName;
            request.LastName = LastName;
            request.Address = Address1;
            request.City = City;
            request.State = State;
            request.Zip = ZipCode;

            request.TransactionType = TransactionType.AUTH_CAPTURE;            
            request.CreditCardNumber = CCNumber;
            request.CreditCardExpirationDate = Convert.ToDateTime(ExpirationMonth + "/1/" + ExpirationYear);

            payment.PaymentRequest = request;

            try
            {
                payment.ProcessTransaction();

            }
            catch { }


            if (payment.PaymentResponse.ResponseCode == ResponseCode.Approved)
            {
                response = "Your payment has been processed, and your card has been charged!";
                model.AuthorizationCode = payment.PaymentResponse.ApprovalCode;
                model.TransactionApproved = true;

                string message = string.Empty;
                message += FirstName + ",\n";
                message += "\n";
                message += "Thank you for your recent paymen to Oasis Beach & Tennis Club! Below are the details of your payment:\n";
                message += "\n";
                message += "Authorization Code #" + model.AuthorizationCode + "\n";
                message += "Amount: $" + model.Amount.ToString() + "\n";
                message += "Credit Card Number: ********" + model.CCNumber.Substring(model.CCNumber.Length - 4, 4) + "\n";
                message += "Expiration: " + model.ExpirationMonth + "/" + model.ExpirationYear + "\n";
                message += "\n";
                message += "If you have any questions please don't hesitate to call us at (972) 772-7768.\n";
                message += "\n";
                message += "\n";
                message += "Thanks,\n";
                message += "-The OasisTennis.com Team";

                // Dont' have user's email address, currently
                //OasisUtility.SendEmail("website@oasistennis.com", new string[] { 
                OasisUtility.SendAdminEmail("OasisTennis.com Payment!", FirstName + " " + LastName + " just made a payment on the site for: " + model.Amount);
            }
            else
            {
                response = "Unfortunately, we were unable to process your card at this time. Please press 'back' on your browser, review your payment details and try again.<br/><br/>The error was: " + payment.PaymentResponse.ResponseReasonText + "(" + 
                    payment.PaymentResponse.ResponseReasonCode + ")<br/><br/>If you continue to have problems, please contact us at (972) 772-7768.";
                model.TransactionApproved = false;
            }

            model.ResponseMessage = response;
            model.ResponseCode = payment.PaymentResponse.ResponseReasonCode;

            return View(model);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Services;
using System.Web;
using Oasis.Lib.Components;

namespace Oasis.Lib.Controllers
{
    public class UserController : Controller
    {
        public IUserService UserService { get; set; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ActionName("registration-confirmation")]
        public ActionResult RegistrationConfirmation()
        {
            return View();
        }

        [ActionName("recover-password")]
        public ActionResult RecoverPassword()
        {
            return View();
        }

        [ActionName("send-password")]
        public ActionResult SendPassword(string emailAddress)
        {
            User user = UserService.GetUserByEmail(emailAddress);

            if (user != null)
            {
                if (user.UserStatusID == (int)UserStatuses.Unvalidated)
                {
                    UserService.SendActivationEmail(user.UserId);
                }
                else
                {
                    UserService.RecoverPassword(emailAddress);
                }                
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["User"] = null;

            return new RedirectResult("/user/");
        }

        [ActionName("sign-on")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignOn(string emailAddress, string password, string redirectUrl)
        {
            User foundUser = UserService.ValidateUser(emailAddress, password);

            if (foundUser == null)
            {
                return new RedirectResult("/user/?error=" + HttpUtility.UrlEncode("Incorrect email address or password. Please try again."));
            }
            else
            {
                if (foundUser.UserStatusID == (int)UserStatuses.Disabled)
                {
                    return new RedirectResult("/user/?error=" + HttpUtility.UrlEncode("Account disabled. Please contact suport with any questions."));
                }
                else if (foundUser.UserStatusID == (int)UserStatuses.Unvalidated)
                {
                    return new RedirectResult("/user/?error=" + HttpUtility.UrlEncode("Account has not been validated. Please check your original email for validation instructions."));
                }
                else
                {
                    Session["User"] = foundUser;

                    if (string.IsNullOrEmpty(redirectUrl))
                        return new RedirectResult("/");
                    else
                        return new RedirectResult(redirectUrl);
                }
            }
        }


        [ActionName("submit-registration")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitRegistration(string emailAddress, string password, string firstName, string lastName, string address1,
            string address2, string city, string state, string zipCode, string phoneNumber, string mobilePhoneNumber)
        {
            User user = UserService.GetUserByEmail(emailAddress);

            if (user != null)
            {
                return new RedirectResult("/user/register/?error=" + HttpUtility.UrlEncode("An account already exists for this email address, sorry."));
            }
            else
            {
                UserService.RegisterUser(emailAddress, password, firstName, lastName, address1, address2, city, state, zipCode, phoneNumber, mobilePhoneNumber);

                return new RedirectResult("/user/registration-confirmation/");
            }
            
        }


        [ActionName("activate-account")]
        public ActionResult ActivateAccount()
        {
            string id = Request.QueryString["id"];

            try
            {
                UserService.ActivateAccount(id);
            }
            catch
            {
                // Do nothing, in this case we're swallowing any exceptions defensively.
            }

            return View();
        }

    }
}

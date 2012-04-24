using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace Oasis.Lib.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult C4Academy()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Construction()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Members()
        {
            return View();
        }

        public ActionResult Membership()
        {
            return View();
        }

        public ActionResult Policy()
        {
            return View();
        }

        public ActionResult Staff()
        {
            return View();
        }


        public string SubmitContact(string Name, string Email, string Reason, string Message, string NewsletterSubscribe)
        {
            string message = "Your email has been sent! We will be in touch with you soon!";
            string to = "jason@oasistennis.com";

            try
            {
                SmtpClient client = new SmtpClient();
                MailMessage mail = new MailMessage();

                switch (Reason)
                {
                    case "Adult Programs":
                        to = "chill@oasistennis.com";
                        break;
                    case "Junior Programs":
                        to = "rmorant@oasistennis.com";
                        break;
                    case "Oasis Membership":
                        to = "mfeste@oasistennis.com";
                        break;

                }

                mail.To.Add(to);
                mail.Subject = "Email from website: " + Reason;
                mail.IsBodyHtml = true;
                mail.Body = "From: " + Name + " (" + Email + ")<br>Opted in to Newsletter: " + NewsletterSubscribe + "<br><br>Message:<br>" + Message;

                client.Send(mail);
            }
            catch (Exception e)
            {
                message = "I'm sorry, there was an error sending your email. Please email website@oasistennis.com directly for details.<br><br>Exception: " + e.Message;
            }

            return message;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Services;
using Oasis.Lib.Util;
using System.Web;
using Oasis.Lib.Components;
using Oasis.Lib.Models;

namespace Oasis.Lib.Controllers
{
    public class AdminMembersController : BaseAdminController
    {
        public IUserService UserService { get; set; }

        public AdminMembersController(IUserService userService)
        {
            UserService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int param)
        {
            AdminMembersEditModel model = new AdminMembersEditModel();
            User user = UserService.GetUserById(param);

            model.User = user;

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string emailAddress, string password, string firstName, string lastName, string address1,
            string address2, string city, string state, string zipCode, int userType, int userStatus, int rank, int[] userAccess, int userId, string phoneNumber, string mobilePhoneNumber)
        {
            UserService.UpdateUser(emailAddress, password, firstName, lastName, address1, address2, city, state, zipCode, userType, userStatus, rank, userAccess, userId, phoneNumber, mobilePhoneNumber);

            return View("EditConfirm");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string emailAddress, string password, string firstName, string lastName, string address1,
            string address2, string city, string state, string zipCode, int userType, int userStatus, int rank, int[] userAccess, string phoneNumber, string mobilePhoneNumber)
        {
            UserService.AddUser(emailAddress, password, firstName, lastName, address1, address2, city, state, zipCode, userType, userStatus, rank, userAccess, phoneNumber, mobilePhoneNumber);

            string body = "";

            body += string.Format("Hi {0},", firstName);
            body += "\n\n";
            body += "One of the OasisTennis.com team members have created you an account on OasisTennis.com with the following details:\n\n";
            body += string.Format("Email:\t{0}\n", emailAddress);
            body += string.Format("Password:\t{0}\n\n", password);
            body += "Please log into the site and change your password at your earliest convenience.\n";

            if (userAccess.Contains((int)UserPriviledges.Member))
            {
                body += "\nWe see that you are an Oasis Member, too! Thank you for your business!\n\n";
            }

            if (userAccess.Contains((int)UserPriviledges.Admin))
            {
                body += "\nYou have been given access to the OasisTennis.com management site at:\n\n";
                body += "http://manage.oasistennis.com\n\n";
            }

            body += "If you have any questions or concerns, please feel free to reply back to this email and we will answer you as soon as possible.\n\n";
            body += "Thanks,\n";
            body += "-The OasisTennis.com Team";

            OasisUtility.SendEmail("website@oasistennis.com", new string[] { emailAddress }, "OasisTennis.com - Account Created!", body);

            return View("CreateConfirm");
        }

        public ActionResult List(int? param)
        {
            if (param == null) param = 1;
            AdminMembersListModel model = new AdminMembersListModel();


            model.Users = UserService.GetUsersByPage(param);
            model.PageNumber = Convert.ToInt32(param);
            model.UserCount = UserService.GetTotalUsersCount();

            return View(model);
        }

        public ActionResult Search(string q)
        {
            AdminMembersSearchModel model = new AdminMembersSearchModel();
            List<User> users = UserService.SearchUsers(q);

            model.Users = users;

            return View(model);
        }
    }
}

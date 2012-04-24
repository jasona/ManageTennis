using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Messaging;
using Oasis.Lib.Services;
using Oasis.Lib.Models;
using System.Collections;
using System.IO;
using Oasis.Lib.Util;

namespace Oasis.Lib.Controllers
{
    public class AdminMessagingController : BaseAdminController
    {
        public IMessageService MessageService { get; set; }

        public AdminMessagingController(IMessageService messageService)
        {
            MessageService = messageService;
        }

        public ActionResult SendText()
        {
            AdminMessagingSendTextModel model = new AdminMessagingSendTextModel();

            Hashtable groupCounts = new Hashtable();

            model.TextGroups = MessageService.GetAllGroups();

            foreach (Group group in model.TextGroups)
            {
                groupCounts.Add(group.GroupId, MessageService.GetGroupCount(group.GroupId));
            }

            model.GroupCount = groupCounts;

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SendTextConfirm(int GroupId, string message)
        {
            MessageService.TextGroupMembers(GroupId, message);

            return View();
        }

        public ActionResult Groups(int? param)
        {
            if (param == null) param = 1;
            AdminGroupsListModel model = new AdminGroupsListModel();
            Hashtable groupCounts = new Hashtable();

            List<Group> groups = MessageService.GetGroupsByPage(param);

            foreach (Group group in groups)
            {
                groupCounts.Add(group.GroupId, MessageService.GetGroupCount(group.GroupId));
            }

            model.Groups = groups;
            model.NumberOfMembers = groupCounts;

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateGroupConfirm(string GroupName, string Description)
        {
            AdminGroupsCreateModel model = new AdminGroupsCreateModel();

            model.GroupId = MessageService.CreateGroup(GroupName, Description);

            return View(model);
        }

        public ActionResult EditGroup(int param)
        {
            AdminGroupsEditModel model = new AdminGroupsEditModel();

            model.Group = MessageService.GetGroupById(param);

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditGroupConfirm(int GroupId, string GroupName, string Description)
        {
            MessageService.UpdateGroup(GroupId, GroupName, Description);

            return View();
        }

        public ActionResult EditGroupMembers(int param)
        {
            AdminGroupsEditMembersModel model = new AdminGroupsEditMembersModel();

            model.GroupId = param;
            model.GroupMembers = MessageService.GetGroupMembers(param);
            model.NotGroupMembers = MessageService.GetMembersNotInGroup(param);

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditGroupMembersConfirm(int GroupId, String[] GroupMembers)
        {

            MessageService.UpdateGroupMembers(GroupId, GroupMembers);

            return View();
        }


        public ActionResult SendEmail()
        {
            AdminMessagingSendEmailModel model = new AdminMessagingSendEmailModel();
            Hashtable groupCounts = new Hashtable();

            model.EmailGroups = MessageService.GetAllGroups();

            foreach (Group group in model.EmailGroups)
            {
                groupCounts.Add(group.GroupId, MessageService.GetGroupCount(group.GroupId));
            }

            model.GroupCount = groupCounts;

            return View(model);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult SendEmailConfirm(int GroupId, string EmailSubject, string EmailBody)
        {
            string[] attachments = new string[Request.Files.Count];

            int i = 0;
            foreach (string upload in Request.Files)
            {
                if (!Request.Files[upload].HasFile()) continue;
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                attachments[i] = Path.Combine(path, filename);

                Request.Files[upload].SaveAs(Path.Combine(path, filename));
                i++;
            }

            MessageService.EmailGroupMembers(GroupId, EmailSubject, EmailBody, attachments);

            return View();
        }

    }
}

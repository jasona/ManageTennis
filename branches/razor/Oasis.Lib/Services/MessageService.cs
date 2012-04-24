using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Oasis.Lib.Util;
using Oasis.Lib.Messaging;

namespace Oasis.Lib.Services
{
    public class MessageService : IMessageService
    {

        public List<Group> GetGroupsByPage(int? page)
        {
            if (page == null) page = 1;
            List<Group> groups;
            int skip = ((Convert.ToInt32(page) - 1) * 10);

            using (OasisEntities o = new OasisEntities())
            {
                groups = o.Groups.OrderBy(g => g.GroupName).Skip(skip).Take(10).ToList<Group>();
            }

            return groups;            
        }

        public int GetGroupCount(int groupId)
        {
            int count = 0;

            using (OasisEntities o = new OasisEntities())
            {
                count = o.UserGroups.Where(g => g.GroupId == groupId).Count();
            }

            return count;
        }

        public int CreateGroup(string groupName, string description)
        {
            Group group = new Group();

            using (OasisEntities o = new OasisEntities())
            {
                group.GroupName = groupName;
                group.Description = description;

                o.Groups.AddObject(group);
                o.SaveChanges();
            }

            return group.GroupId;
        }

        public Group GetGroupById(int groupId)
        {
            Group group;

            using (OasisEntities o = new OasisEntities())
            {
                group = o.Groups.Where(g => g.GroupId == groupId).FirstOrDefault<Group>();
            }

            return group;
        }

        public void UpdateGroup(int groupId, string groupName, string description)
        {
            Group group;

            using (OasisEntities o = new OasisEntities())
            {
                group = o.Groups.Where(g => g.GroupId == groupId).FirstOrDefault<Group>();

                if (group != null)
                {
                    group.GroupName = groupName;
                    group.Description = description;

                    o.SaveChanges();
                }
            }

        }

        public List<User> GetGroupMembers(int groupId)
        {
            List<User> members;

            using (OasisEntities o = new OasisEntities())
            {
                members = o.UserGroups.Where(ug => ug.GroupId == groupId).Select(ug => ug.User).ToList<User>();
            }

            return members;
        }

        public List<User> GetMembersNotInGroup(int groupId)
        {
            List<int> userIds;
            List<User> members;

            using (OasisEntities o = new OasisEntities())
            {
                userIds = o.UserGroups.Where(ug => ug.GroupId == groupId).Select(ug => ug.UserId).ToList<int>();

                members = o.Users.Where(u => !userIds.Contains(u.UserId)).ToList<User>();
            }

            return members;
        }

        public void UpdateGroupMembers(int groupId, string[] memberIds)
        {
            int memberId = 0;

            using (OasisEntities o = new OasisEntities())
            {
                // Delete the current first
                var currentMembers = o.UserGroups.Where(ug => ug.GroupId == groupId).ToList<UserGroup>();

                foreach (UserGroup group in currentMembers)
                {
                    o.DeleteObject(group);
                }

                // Now, re-add them
                if (memberIds != null)
                {
                    foreach (string id in memberIds)
                    {
                        if (!int.TryParse(id, out memberId)) continue;

                        UserGroup group = new UserGroup();

                        group.GroupId = groupId;
                        group.UserId = memberId;

                        o.UserGroups.AddObject(group);
                    }
                }

                o.SaveChanges();
            }
        }


        public void EmailGroupMembers(int groupId, string subject, string body, string[] attachments)
        {

            using (OasisEntities o = new OasisEntities())
            {
                List<User> usersToEmail = o.UserGroups
                    .Join(o.Users, ug => ug.UserId, u => u.UserId, (ug, u) => new { UserGroup = ug, User = u })
                    .Where(g => g.UserGroup.GroupId == groupId)
                    .Select(g => g.User)
                    .ToList<User>();

                string output = string.Empty;
                foreach (User user in usersToEmail)
                {
                    OasisUtility.SendEmail("website@oasistennis.com", new string[] { user.EmailAddress }, subject, body, attachments);
                }
                
            }
        }


        public List<Group> GetAllGroups()
        {
            List<Group> groups = new List<Group>();


            using (OasisEntities o = new OasisEntities())
            {
                groups = o.Groups.OrderBy(g => g.GroupName).ToList<Group>();
            }

            return groups;
        }


        public void TextGroupMembers(int groupId, string message)
        {
            SMS sms = new SMS();


            using (OasisEntities o = new OasisEntities())
            {
                List<User> usersToEmail = o.UserGroups
                    .Join(o.Users, ug => ug.UserId, u => u.UserId, (ug, u) => new { UserGroup = ug, User = u })
                    .Where(g => g.UserGroup.GroupId == groupId)
                    .Select(g => g.User)
                    .ToList<User>();

                string output = string.Empty;
                foreach (User user in usersToEmail)
                {
                    if (!string.IsNullOrEmpty(user.MobilePhoneNumber))
                    {
                        
                        sms.To = OasisUtility.ScrubPhoneNumber(user.MobilePhoneNumber);
                        sms.Message = message;

                        sms.SendMessage();
                    }
                }

            }
        
        }
    }
}

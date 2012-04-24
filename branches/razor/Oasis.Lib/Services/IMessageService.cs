using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Services
{
    public interface IMessageService
    {
        List<Group> GetGroupsByPage(int? page);
        List<Group> GetAllGroups();
        Group GetGroupById(int groupId);
        int GetGroupCount(int groupId);
        int CreateGroup(string groupName, string description);
        void UpdateGroup(int groupId, string groupName, string description);
        List<User> GetGroupMembers(int groupId);
        List<User> GetMembersNotInGroup(int groupId);
        void UpdateGroupMembers(int groupId, string[] memberIds);
        void EmailGroupMembers(int groupId, string subject, string body, string[] attachments);
        void TextGroupMembers(int groupId, string message);
    }
}

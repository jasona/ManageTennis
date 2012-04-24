using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Services
{
    public interface IUserService
    {

        User ValidateUser(string emailAddress, string password);
        void RegisterUser(string emailAddress, string password, string firstName, string lastName, string address1, string address2, string city, string state, string zipCode, string phoneNumber, string mobilePhoneNumber);
        void AddUser(string emailAddress, string password, string firstName, string lastName, string address1, string address2, string city, string state, string zipCode, int userType, int userStatus, int rank, int[] userAccess, string phoneNumber, string mobilePhoneNumber);
        void SendActivationEmail(int userId);
        List<Rank> GetRanks();
        void ActivateAccount(string activationCode);
        void RecoverPassword(string emailAddress);
        List<User> GetUsersByPage(int? page);
        int GetTotalUsersCount();
        User GetUserById(int userId);
        User GetUserByEmail(string emailAddress);
        void UpdateUser(string emailAddress, string password, string firstName, string lastName, string address1, string address2, string city, string state, string zipCode, int userType, int userStatus, int rank, int[] userAccess, int userId, string phoneNumber, string mobilePhoneNumber);
        List<User> SearchUsers(string q);

    }
}

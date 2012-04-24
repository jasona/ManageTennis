using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oasis.Lib.Util;
using Oasis.Lib.Components;

namespace Oasis.Lib.Services
{
    public class UserService : IUserService
    {


        public User ValidateUser(string emailAddress, string password)
        {
            User foundUser = null;
            using (OasisEntities o = new OasisEntities())
            {
                foundUser = o.Users.Where(u => u.EmailAddress == emailAddress && u.Password == password).FirstOrDefault<User>();
            }

            return foundUser;
        }


        public void RegisterUser(string emailAddress, string password, string firstName, string lastName, string address1, string address2, string city, string state, string zipCode, string phoneNumber, string mobilePhoneNumber)
        {
            using (OasisEntities o = new OasisEntities())
            {
                Guid activationCode = Guid.NewGuid();
                User user = new User();

                // Build and save the user
                user.EmailAddress = emailAddress;
                user.Password = password;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.PhoneNumber = phoneNumber;
                user.MobilePhoneNumber = mobilePhoneNumber;
                user.Address1 = address1;
                user.Address2 = address2;
                user.City = city;
                user.State = state;
                user.ZipCode = zipCode;
                user.ActivationKey = activationCode;
                user.RegisterDate = DateTime.Now;
                user.LastLoginDate = DateTime.Now;
                user.AccountValidated = false;
                user.RankId = (int)Ranks.TWO_POINT_FIVE;
                user.UserStatusID = (int) UserStatuses.Unvalidated;
                user.UserTypeId = (int) UserTypes.WebUser;
                user.UserAccessBitMask = (int) UserPriviledges.User;

                o.Users.AddObject(user);
                o.SaveChanges();

                this.SendActivationEmail(user.UserId);

            }

        }


        public List<Rank> GetRanks()
        {
            List<Rank> ranks = new List<Rank>();

            using (OasisEntities o = new OasisEntities())
            {
                ranks = o.Ranks.ToList<Rank>();            
            }
            return ranks;
        }


        public void ActivateAccount(string activationCode)
        {
            using (OasisEntities o = new OasisEntities())
            {
                Guid activationKey = Guid.Parse(activationCode);
                User user = o.Users.Where(u => u.ActivationKey == activationKey).FirstOrDefault<User>();

                user.UserStatusID = (int) UserStatuses.Active;
                user.AccountValidated = true;

                o.SaveChanges();
            }
        }


        public void RecoverPassword(string emailAddress)
        {
            using (OasisEntities o = new OasisEntities())
            {
                User user = o.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefault<User>();

                if (user != null)
                {
                    string password = OasisUtility.GenerateTemporaryPassword();
                    StringBuilder email = new StringBuilder();

                    user.Password = password;
                    o.SaveChanges();

                    // Build the email
                    email.Append(user.FirstName + ",\n");
                    email.Append("\n");
                    email.Append("\n");
                    email.Append("You recently requested your password for OasisTennis.com.\n");
                    email.Append("\n");
                    email.Append("The following details is your membership information:\n");
                    email.Append("\n");
                    email.Append("Email address: " + user.EmailAddress + "\n");
                    email.Append("Password: " + password + "\n");
                    email.Append("\n");
                    email.Append("Let us know if you have any problems on the site!!\n");
                    email.Append("\n");
                    email.Append("\n");
                    email.Append("Thanks,\n");
                    email.Append("-The Oasis Team");

                    OasisUtility.SendEmail("website@oasistennis.com", new string[] { user.EmailAddress }, "OasisTennis.com Activation Email", email.ToString());

                }
            }
        }

        public List<User> GetUsersByPage(int? page)
        {
            if (page == null) page = 1;
            List<User> users;
            int skip = ((Convert.ToInt32(page) - 1) * 10);

            using (OasisEntities o = new OasisEntities())
            {
                users = o.Users.OrderBy(u => u.LastName).Skip(skip).Take(10).ToList<User>();
            }

            return users;
        }


        public int GetTotalUsersCount()
        {
            int count = 0;

            using (OasisEntities o = new OasisEntities())
            {
                count = o.Users.Count();
            }

            return count;
        }


        public void AddUser(string emailAddress, string password, string firstName, string lastName, string address1, string address2, string city, string state, string zipCode, int userType, int userStatus, int rank, int[] userAccess, string phoneNumber, string mobilePhoneNumber)
        {
            Guid activationCode = Guid.NewGuid();
            User user = new User();

            using (OasisEntities o = new OasisEntities())
            {
                int access = 0;

                foreach (int bit in userAccess)
                {
                    access = access | bit;
                }

                user.EmailAddress = emailAddress;
                user.Password = password;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.PhoneNumber = phoneNumber;
                user.MobilePhoneNumber = mobilePhoneNumber;
                user.Address1 = address1;
                user.Address2 = address2;
                user.City = city;
                user.State = state;
                user.ZipCode = zipCode;
                user.ActivationKey = activationCode;
                user.RegisterDate = DateTime.Now;
                user.LastLoginDate = DateTime.Now;
                user.AccountValidated = false;
                user.RankId = rank;
                user.UserStatusID = userStatus;
                user.UserTypeId = userType;
                user.UserAccessBitMask = access;

                o.Users.AddObject(user);
                o.SaveChanges();
            }

            this.SendActivationEmail(user.UserId);
        }


        public User GetUserById(int userId)
        {
            User user;

            using (OasisEntities o = new OasisEntities())
            {
                user = o.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
            }

            return user;
        }

        public void UpdateUser(string emailAddress, string password, string firstName, string lastName, string address1,
                                string address2, string city, string state, string zipCode, int userType, int userStatus, int rank, int[] userAccess, int userId, string phoneNumber, string mobilePhoneNumber)
        {
            User user;

            using (OasisEntities o = new OasisEntities())
            {
                user = o.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();

                if (user != null)
                {
                    int accessBitMask = 0;

                    foreach (int bit in userAccess)
                    {
                        accessBitMask = accessBitMask | bit;
                    }

                    user.EmailAddress = emailAddress;
                    user.Password = password;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.PhoneNumber = phoneNumber;
                    user.MobilePhoneNumber = mobilePhoneNumber;
                    user.Address1 = address1;
                    user.Address2 = address2;
                    user.City = city;
                    user.State = state;
                    user.ZipCode = zipCode;
                    user.UserTypeId = userType;
                    user.UserStatusID = userStatus;
                    user.RankId = rank;
                    user.UserAccessBitMask = accessBitMask;

                    o.SaveChanges();
                }
            }
        }

        public List<User> SearchUsers(string q)
        {
            List<User> users;

            using (OasisEntities o = new OasisEntities())
            {
                users = o.Users.Where(u => u.FirstName.ToLower().Contains(q.ToLower()) || u.LastName.ToLower().Contains(q.ToLower()) || u.EmailAddress.ToLower().Contains(q.ToLower())).ToList<User>();
            }

            return users;
        }

        public void SendActivationEmail(int userId)
        {
            // Now, send out the activation email!
            StringBuilder email = new StringBuilder();
            User user;

            using (OasisEntities o = new OasisEntities())
            {
                user = o.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
            }

            if (user != null)
            {
                email.Append(user.FirstName + ",\n");
                email.Append("\n");
                email.Append("\n");
                email.Append("Thanks for recently registered on OasisTennis.com! This is the last step to gaining access to the site.\n");
                email.Append("\n");
                email.Append("Please click on the following link to validate your email address, and activate your account:\n");
                email.Append("\n");
                email.Append("http://oasistennis.com/user/activate-account/?id=" + user.ActivationKey.ToString() + "\n");
                email.Append("\n");
                email.Append("We look forward to seeing you on the courts!\n");
                email.Append("\n");
                email.Append("\n");
                email.Append("Thanks,\n");
                email.Append("-The Oasis Team");

                OasisUtility.SendEmail("website@oasistennis.com", new string[] { user.EmailAddress }, "OasisTennis.com Activation Email", email.ToString());
                OasisUtility.SendAdminEmail("OasisTennis.com Registration!", user.FirstName + " " + user.LastName + " (" + user.EmailAddress + ") just registered on the site!");
            }
        }

        public User GetUserByEmail(string emailAddress)
        {
            User user;

            using (OasisEntities o = new OasisEntities())
            {
                user = o.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefault<User>();
            }

            return user;
        }

    }
}

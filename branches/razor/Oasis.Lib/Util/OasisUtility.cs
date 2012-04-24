using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Oasis.Lib.Components;
using System.Text.RegularExpressions;
using Oasis.Lib.Services;
using System.IO;

namespace Oasis.Lib.Util
{
    public class OasisUtility
    {

        public static string ScrubPhoneNumber(string phoneNumber)
        {
            StringBuilder number = new StringBuilder(phoneNumber);

            number.Replace("(", "");
            number.Replace(")", "");
            number.Replace(" ", "");
            number.Replace("-", "");

            return number.ToString();
        }

        public static string StripHTML(string inputString)
        {
            string HTML_TAG_PATTERN = "<.*?>"; 
            
            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }

        public static string EncodeString(string phrase)
        {
            int maxLength = 220;
            string str = phrase.ToLower();
    
            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");  
            // convert multiple spaces/hyphens into one space       
            str = Regex.Replace(str, @"[\s-]+", " ").Trim(); 
            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim(); 
            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        public static bool CheckBitMask(int bitMask, int bitToCheck)
        {
            if ((bitMask & bitToCheck) == bitToCheck)
                return true;
            else
                return false;
        }

        public static string GetBooleanString(bool value)
        {
            return (value ? "Yes" : "No");
        }

        public static string GetProName(int? proUserId)
        {
            UserService service = new UserService();
            if (proUserId == null) return "N/A";

            User user = service.GetUserById(Convert.ToInt32(proUserId));

            return user.FirstName + " " + user.LastName;
        }

        public static string GetUserStatusString(int userStatusId)
        {
            string userStatus = "Unknown";

            switch (userStatusId)
            {
                case (int)UserStatuses.Active:
                    userStatus = "Active";
                    break;
                case (int)UserStatuses.Disabled:
                    userStatus = "Disabled";
                    break;
                case (int)UserStatuses.Unvalidated:
                    userStatus = "Unvalidated";
                    break;
                default:
                    userStatus = "Unknown";
                    break;
            }

            return userStatus;
        }

        public static string GetDemographicString(int demographicId)
        {
            string demographic = "unknown";

            switch (demographicId)
            {
                case (int)Demographics.AdultMale:
                    demographic = "Adults Men";
                    break;
                case (int)Demographics.AdultFemale:
                    demographic = "Adults Women";
                    break;
                case (int)Demographics.JuniorMale:
                    demographic = "Juniors Boys";
                    break;
                case (int)Demographics.JuniorFemale:
                    demographic = "Juniors Girls";
                    break;
            }

            return demographic;
        }

        public static string GetUserTypeString(int userTypeId)        
        {
            string userType = "Unknown";

            switch (userTypeId)
            {
                case (int)UserTypes.CurrentMember:
                    userType = "Member";
                    break;
                case (int)UserTypes.Employee:
                    userType = "Employee";
                    break;
                case (int)UserTypes.PastMember:
                    userType = "Prior Member";
                    break;
                case (int)UserTypes.WebUser:
                    userType = "Web User";
                    break;
                default:
                    userType = "Unknown";
                    break;
            }

            return userType;
        }

        public static string GetEventTypeString(int eventTypeId)
        {
            string eventType = "unknown";

            switch (eventTypeId)
            {
                case (int)EventTypes.Clinic:
                    eventType = "Clinic";
                    break;
                case (int)EventTypes.ClubDinner:
                    eventType = "Club Dinner";
                    break;
                case (int)EventTypes.DoublesTournament:
                    eventType = "Doubles Tournament";
                    break;
                case (int)EventTypes.Mixer:
                    eventType = "Mixer";
                    break;
                case (int)EventTypes.Private:
                    eventType = "Private";
                    break;
                case (int)EventTypes.SinglesTournament:
                    eventType = "Singles Tournament";
                    break;
                case (int)EventTypes.ThreeAndMe:
                    eventType = "Three and Me";
                    break;
            }

            return eventType;
        }

        public static string GetDemographicBitMaskList(int? demographicBitMask)
        {
            string demoString = string.Empty;
            int demoMask = 0;
            if (demographicBitMask != null) demoMask = Convert.ToInt32(demographicBitMask);

            if (CheckBitMask(demoMask, (int)Demographics.AdultMale))
                demoString += ", " + GetDemographicString((int)Demographics.AdultMale);

            if (CheckBitMask(demoMask, (int)Demographics.AdultFemale))
                demoString += ", " + GetDemographicString((int)Demographics.AdultFemale);

            if (CheckBitMask(demoMask, (int)Demographics.JuniorMale))
                demoString += ", " + GetDemographicString((int)Demographics.JuniorMale);

            if (CheckBitMask(demoMask, (int)Demographics.JuniorFemale))
                demoString += ", " + GetDemographicString((int)Demographics.JuniorFemale);

            demoString = demoString.Substring(1, demoString.Length - 1);

            return demoString;
        }

        public static string GetRankBitMaskList(int? rankBitMask)
        {
            string rankString = string.Empty;
            int rankMask = 0;
            if (rankBitMask != null) rankMask = Convert.ToInt32(rankBitMask);


            if (CheckBitMask(rankMask, (int)Ranks.ONE_POINT_FIVE))
                rankString += ", " + GetRankString((int)Ranks.ONE_POINT_FIVE);

            if (CheckBitMask(rankMask, (int)Ranks.TWO_ZERO))
                rankString += ", " + GetRankString((int)Ranks.TWO_ZERO);

            if (CheckBitMask(rankMask, (int)Ranks.TWO_POINT_FIVE))
                rankString += ", " + GetRankString((int)Ranks.TWO_POINT_FIVE);

            if (CheckBitMask(rankMask, (int)Ranks.THREE_ZERO))
                rankString += ", " + GetRankString((int)Ranks.THREE_ZERO);

            if (CheckBitMask(rankMask, (int)Ranks.THREE_POINT_FIVE))
                rankString += ", " + GetRankString((int)Ranks.THREE_POINT_FIVE);

            if (CheckBitMask(rankMask, (int)Ranks.FOUR_ZERO))
                rankString += ", " + GetRankString((int)Ranks.FOUR_ZERO);

            if (CheckBitMask(rankMask, (int)Ranks.FOUR_POINT_FIVE))
                rankString += ", " + GetRankString((int)Ranks.FOUR_POINT_FIVE);

            if (CheckBitMask(rankMask, (int)Ranks.FIVE_ZERO))
                rankString += ", " + GetRankString((int)Ranks.FIVE_ZERO);

            if (CheckBitMask(rankMask, (int)Ranks.FIVE_POINT_FIVE))
                rankString += ", " + GetRankString((int)Ranks.FIVE_POINT_FIVE);

            if (CheckBitMask(rankMask, (int)Ranks.SIX_ZERO))
                rankString += ", " + GetRankString((int)Ranks.SIX_ZERO);

            if (CheckBitMask(rankMask, (int)Ranks.SIX_POINT_FIVE))
                rankString += ", " + GetRankString((int)Ranks.SIX_POINT_FIVE);

            if (CheckBitMask(rankMask, (int)Ranks.SEVEN_ZERO))
                rankString += ", " + GetRankString((int)Ranks.SEVEN_ZERO);

            rankString = rankString.Substring(1, rankString.Length - 1);

            return rankString;
        }

        public static string GetRankString(int rank)
        {
            string rankString = "unknown";

            switch (rank)
            {
                case (int)Ranks.ONE_POINT_FIVE:
                    rankString = "1.5";
                    break;
                case (int)Ranks.TWO_ZERO:
                    rankString = "2.0";
                    break;
                case (int)Ranks.TWO_POINT_FIVE:
                    rankString = "2.5";
                    break;
                case (int)Ranks.THREE_ZERO:
                    rankString = "3.0";
                    break;
                case (int)Ranks.THREE_POINT_FIVE:
                    rankString = "3.5";
                    break;
                case (int)Ranks.FOUR_ZERO:
                    rankString = "4.0";
                    break;
                case (int)Ranks.FOUR_POINT_FIVE:
                    rankString = "4.5";
                    break;
                case (int)Ranks.FIVE_ZERO:
                    rankString = "5.0";
                    break;
                case (int)Ranks.FIVE_POINT_FIVE:
                    rankString = "5.5";
                    break;
                case (int)Ranks.SIX_ZERO:
                    rankString = "6.0";
                    break;
                case (int)Ranks.SIX_POINT_FIVE:
                    rankString = "6.5";
                    break;
                case (int)Ranks.SEVEN_ZERO:
                    rankString = "7.0";
                    break;
            }

            return rankString;
        }

        public static string GenerateTemporaryPassword()
        {
            Random rng = new Random();
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string password = "";

            for (int i = 0; i < 12; i++)
            {
                password += letters[rng.Next(26)];
            }

            return password;
        }

        public static void SendAdminEmail(string subject, string body)
        {
            OasisUtility.SendEmail("website@oasistennis.com", new string[] { "jason@oasistennis.com" }, subject, body);
        }

        public static void SendEmail(string from, string[] to, string subject, string body)
        {
            OasisUtility.SendEmail(from, to, subject, body, null);
        }

        public static void SendEmail(string from, string[] to, string subject, string body, string[] attachments)
        {
            MailMessage message = new MailMessage();

            foreach (string addr in to)
            {
                message.To.Add(addr);
            }

            MailAddress addrFrom = new MailAddress(from);
            message.From = addrFrom;
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            if (attachments != null)
            {

                foreach (string attachment in attachments)
                {

                    if (!string.IsNullOrEmpty(attachment))
                    {
                        try
                        {
                            Attachment attachFile = new Attachment(attachment);
                            message.Attachments.Add(attachFile);
                        }
                        catch { }
                    }
                }
            }

            SmtpClient client = new SmtpClient
                                {
                                    Host = "smtp.elasticemail.com",
                                    Port = 2525,
                                    DeliveryMethod = SmtpDeliveryMethod.Network,
                                    UseDefaultCredentials = false,
                                    Credentials = new System.Net.NetworkCredential("jason@oasistennis.com", "4edee326-1188-4cb4-9787-6696e746efe9")
                                };

            client.Send(message);

            foreach (string attachment in attachments)
            {
                if (!string.IsNullOrEmpty(attachment))
                {
                    try
                    {
                        File.Delete(attachment);
                    }
                    catch { }
                }
            }
        }

    }
}

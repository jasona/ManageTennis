using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TwilioRest;

namespace Oasis.Lib.Messaging
{
    public class SMS
    {
        // Twilio REST API version
        const string API_VERSION = "2010-04-01";

        // Twilio AccountSid and AuthToken
        const string ACCOUNT_SID = "AC39e3ccc56238d71666b8bf4cfc6907ba";
        const string ACCOUNT_TOKEN = "0c8e15f580ee6df5cfe3f47ca040bf25";

        // Outgoing Caller ID previously validated with Twilio
        const string CALLER_ID = "9723169133";

        public string To { get; set; }
        public string Message { get; set; }

        public void SendMessage()
        {
            Account account;
            Hashtable h;

            if (Message.Length > 160)
            {
                throw new ApplicationException("Message cannot be longer than 160 characters. Current message is: " + Message.Length);
            }

            // Create Twilio REST account object using Twilio account ID and token
            account = new Account(ACCOUNT_SID, ACCOUNT_TOKEN);

            h = new Hashtable();
            h.Add("From", CALLER_ID);
            h.Add("To", To);
            h.Add("Body", Message);

            account.request(String.Format("/{0}/Accounts/{1}/SMS/Messages", API_VERSION, ACCOUNT_SID), "POST", h);

        }
    }
}

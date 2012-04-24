using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Oasis.Lib
{
    public class OasisSession
    {
        public OasisSession()
        {

        }

        public OasisSession(HttpCookie hydrateValues)
        {
            this.HydrateSession(hydrateValues);

        }


        public void HydrateSession(HttpCookie hydrateValues)
        {
            SessionId = hydrateValues["SessionId"];
            UserName = hydrateValues["UserName"];
            OpenDate = DateTime.Parse(hydrateValues["OpenDate"]);
            LastActivityDate = DateTime.Parse(hydrateValues["LastActivityDate"]);
        }

        public string SessionId { get; set; }
        public string UserName { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime LastActivityDate { get; set; }

        public static OasisSession NewSession()
        {
            OasisSession session = new OasisSession();

            session.SessionId = Guid.NewGuid().ToString();
            session.OpenDate = DateTime.Now;

            return session;
        }

        public void Save(HttpResponseBase response)
        {
            response.Cookies["OasisSession"]["SessionId"] = this.SessionId;
            response.Cookies["OasisSession"]["UserName"] = this.UserName;
            response.Cookies["OasisSession"]["OpenDate"] = this.OpenDate.ToString("G");
            response.Cookies["OasisSession"]["LastActivityDate"] = this.LastActivityDate.ToString("G");

            response.Cookies["OasisSession"].Expires = DateTime.Now.AddMinutes(20);
        }

    }
}

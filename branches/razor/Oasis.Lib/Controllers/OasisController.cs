using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Oasis.Lib.Controllers
{
    public class OasisController : Controller
    {

        public OasisController()
            : base()
        {
        }

        private OasisSession _session;
        public new OasisSession Session
        {
            get
            {
                if (HttpContext.Request.Cookies["OasisSession"] == null)
                {
                    _session = OasisSession.NewSession();
                }
                else
                {
                    _session = new OasisSession(this.ControllerContext.HttpContext.Request.Cookies["OasisSession"]);
                    _session.LastActivityDate = DateTime.Now;
                }

                _session.Save(this.ControllerContext.HttpContext.Response);

                return _session;
            }
            set
            {
                _session = value;
                _session.Save(this.ControllerContext.HttpContext.Response);
            }

        }

    }
}

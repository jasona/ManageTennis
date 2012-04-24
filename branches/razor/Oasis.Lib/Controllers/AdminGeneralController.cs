using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Components;
using Oasis.Lib.Util;
using System.Web;

namespace Oasis.Lib.Controllers
{
    public class AdminGeneralController : BaseAdminController
    {

        public ActionResult Dashboard()
        {
            return View();
        }


    }
}

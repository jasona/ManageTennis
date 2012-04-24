using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oasis.Lib.Services;

namespace Oasis.Lib.Controllers
{
    public class InvoicePaymentController : Controller
    {
        public IInvoiceService InvoiceService { get; set; }

        public InvoicePaymentController(IInvoiceService invoiceService)
        {
            InvoiceService = invoiceService;
        }

        //public ActionResult Display(string id)
        //{
        //    try
        //    {

        //        Invoice invoice = InvoiceService.GetInvoiceById(id);

        //        if (invoice == null) return Redirect("/invoice/not-found/");

        //        return View();
        //    }
        //    catch (FormatException fe)
        //    {
        //        return Redirect("/invoice/not-found/");
        //    }
        //}

    }
}

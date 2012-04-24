using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oasis.Lib.Models
{
    public class PaymentModel
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CCNumber { get; set; }
        public string CCType { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string AuthorizationCode { get; set; }
        public bool TransactionApproved { get; set; }
    }
}

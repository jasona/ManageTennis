/*
 * Authorize.net Payment Processing API
 * Jason Alexander (jalexander@telligent.com), http://JasonA.net
 * 
 * Copyright (c) 2007, Jason Alexander
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met. See LICENSE.txt for full details.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Oasis.Lib.Util;

namespace Oasis.Lib.Billing
{

    /// <summary>
    /// PaymentResponse is the object that is returned to the calling interface and encapsulates all the data from the Authorize.net response.
    /// </summary>
    [Serializable]
    public class PaymentResponse
    {

        #region Private Members

        private string _rawResponse = String.Empty;
        private string[] _responseElements = { };

        #endregion

        #region Constructor(s)

        public PaymentResponse(string rawResponse)
        {
            if (rawResponse != string.Empty)
                this.RawResponse = rawResponse;
        }

        #endregion

        #region Public Properties

        public ResponseCode ResponseCode
        {
            get { return (ResponseCode)int.Parse(GetResponse(0)); }
            set { ; }
        }

        public string ResponseSubcode
        {
            get { return GetResponse(1); }
            set { ; }
        }

        public string ResponseReasonCode
        {
            get { return GetResponse(2); }
            set { ; }
        }

        public string ResponseReasonText
        {
            get { return GetResponse(3); }
            set { ; }
        }

        public string ApprovalCode
        {
            get { return GetResponse(4); }
            set { ; }
        }

        public string AvsResultCode
        {
            get { return GetResponse(5); }
            set { ; }
        }

        public string TransactionId
        {
            get { return GetResponse(6); }
            set { ; }
        }

        public string InvoiceNumber
        {
            get { return GetResponse(7); }
            set { ; }
        }

        public string Description
        {
            get { return GetResponse(8); }
            set { ; }
        }

        public string Amount
        {
            get { return GetResponse(9); }
            set { ; }
        }

        public string Method
        {
            get { return GetResponse(10); }
            set { ; }
        }

        public string TransactionType
        {
            get { return GetResponse(11); }
            set { ; }
        }

        public string CustomerId
        {
            get { return GetResponse(12); }
            set { ; }
        }

        public string CardHolderFirstName
        {
            get { return GetResponse(13); }
            set { ; }
        }

        public string CardHolderLastName
        {
            get { return GetResponse(14); }
            set { ; }
        }

        public string Company
        {
            get { return GetResponse(15); }
            set { ; }
        }

        public string BillingAddress
        {
            get { return GetResponse(16); }
            set { ; }
        }

        public string City
        {
            get { return GetResponse(17); }
            set { ; }
        }

        public string State
        {
            get { return GetResponse(18); }
            set { ; }
        }

        public string Zip
        {
            get { return GetResponse(19); }
            set { ; }
        }

        public string Country
        {
            get { return GetResponse(20); }
            set { ; }
        }

        public string Phone
        {
            get { return GetResponse(21); }
            set { ; }
        }

        public string Fax
        {
            get { return GetResponse(22); }
            set { ; }
        }

        public string Email
        {
            get { return GetResponse(23); }
            set { ; }
        }

        public string ShipToFirstName
        {
            get { return GetResponse(24); }
            set { ; }
        }

        public string ShipToLastName
        {
            get { return GetResponse(25); }
            set { ; }
        }

        public string ShipToCompany
        {
            get { return GetResponse(26); }
            set { ; }
        }

        public string ShipToAddress
        {
            get { return GetResponse(27); }
            set { ; }
        }

        public string ShipToCity
        {
            get { return GetResponse(28); }
            set { ; }
        }

        public string ShipToState
        {
            get { return GetResponse(29); }
            set { ; }
        }

        public string ShipToZip
        {
            get { return GetResponse(30); }
            set { ; }
        }

        public string ShipToCountry
        {
            get { return GetResponse(31); }
            set { ; }
        }

        public string TaxAmount
        {
            get { return GetResponse(32); }
            set { ; }
        }

        public string DutyAmount
        {
            get { return GetResponse(33); }
            set { ; }
        }

        public string FreightAmount
        {
            get { return GetResponse(34); }
            set { ; }
        }

        public string TaxExemptFlag
        {
            get { return GetResponse(35); }
            set { ; }
        }

        public string PONumber
        {
            get { return GetResponse(36); }
            set { ; }
        }

        public string MD5Hash
        {
            get { return GetResponse(37); }
            set { ; }
        }

        public string CardCodeResponseCode
        {
            get { return GetResponse(38); }
            set { ; }
        }

        public string CardholderAuthenticationVerificationResponseCode
        {
            get { return GetResponse(39); }
            set { ; }
        }

        public string RawResponse
        {
            get { return _rawResponse; }
            set { 
                _rawResponse = value;
                this.ParseResponse();
            }
        }

        public string[] ResponseElements
        {
            get { return _responseElements; }
            set { _responseElements = value; }
        }

        #endregion

        #region Methods

        public string GetResponse(int index)
        {
            if (_responseElements != null && _responseElements.Length > 0 && _responseElements.Length >= index + 1)
            {
                return _responseElements[index];
            }
            return "";
        }

        protected void ParseResponse()
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            CSVReader reader = new CSVReader(new MemoryStream(encoder.GetBytes(_rawResponse)));

            _responseElements = reader.GetCSVLine();

        }

        #endregion

    }
}

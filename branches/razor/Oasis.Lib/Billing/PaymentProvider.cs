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
using System.Net;
using System.Web;

namespace Oasis.Lib.Billing
{

    /// <summary>
    /// PaymentProvider is the main class that handles the basic communication back and forth with Authorize.net's payment gateway.
    /// </summary>
    public class PaymentProvider
    {

        #region Member Variables

        private PaymentRequest _paymentRequest = new PaymentRequest();
        private PaymentResponse _paymentResponse;

        // These members are the extra values you can submit to authorize.net

        // These members are the response fields
        private ResponseCode _responseCode = ResponseCode.Unknown;
        private string _responseSubCode = String.Empty;
        private string _responseReasonCode = String.Empty;
        private string _responseReasonText = String.Empty;
        private string _approvalCode = String.Empty;
        private string _avsResultCode = String.Empty;
        private string _transactionId = String.Empty;
        private string _invoiceNumber = String.Empty;
        private string _description = String.Empty;
        private string _transactionAmount = String.Empty;
        private string _method = String.Empty;
        private string _transactionType = String.Empty;
        private string _customerId = String.Empty;
        

        #endregion

        #region Constructor(s)

        public PaymentProvider()
        {
        }

        #endregion

        #region Properties


        public PaymentRequest PaymentRequest
        {
            get { return _paymentRequest; }
            set { _paymentRequest = value; }
        }

        public PaymentResponse PaymentResponse
        {
            get { return _paymentResponse; }
            set { _paymentResponse = value; }
        }

        #endregion

        #region Methods

        public void ProcessTransaction()
        {
            ASCIIEncoding encoder   = new ASCIIEncoding();
            string gatewayUrl       = (_paymentRequest.TestMode ? "https://certification.authorize.net/gateway/transact.dll" : "https://secure.authorize.net/gateway/transact.dll");
            HttpWebRequest request  = (HttpWebRequest) HttpWebRequest.Create(gatewayUrl);


            // Check for required properties
            if (string.IsNullOrEmpty(_paymentRequest.ApiLoginId)) throw new PaymentException("ApiLoginId must be set in order to process a transaction.");
            if (string.IsNullOrEmpty(_paymentRequest.ApiTransactionKey)) throw new PaymentException("ApiTransactionKey must be set in order to process a transaction.");
            if (_paymentRequest.Amount == double.MinValue) throw new PaymentException("Amount must be set in order to process a transaction.");
            if (string.IsNullOrEmpty(_paymentRequest.CreditCardNumber)) throw new PaymentException("CreditCardNumber must be set in order to process a transaction.");
            if (_paymentRequest.CreditCardExpirationDate == null || _paymentRequest.CreditCardExpirationDate == DateTime.MinValue) throw new PaymentException("CreditCardExpirationDate must be set in order to process a transaction.");


            // Static fields
            _paymentRequest.AddField("x_version", "3.1");
            _paymentRequest.AddField("x_delim_data", "TRUE");
            _paymentRequest.AddField("x_relay_response", "FALSE");

            
            // Construct the request
            byte[] data = encoder.GetBytes(_paymentRequest.ToString());

            // Setup the post
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            // Grab and write the data to the request stream
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            // Now, get the response
            WebResponse response        = request.GetResponse();
            StreamReader responseReader = new StreamReader(response.GetResponseStream());

            _paymentResponse = new PaymentResponse(responseReader.ReadToEnd());

            if (_paymentResponse.ResponseCode == ResponseCode.Error)
                throw new TransactionException(_paymentResponse.ResponseReasonText + " (Reason code: " + _paymentResponse.ResponseReasonCode + ")");
    
        }

        #endregion

    }
}

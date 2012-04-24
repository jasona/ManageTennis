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
using System.Web;
using System.Collections;

namespace Oasis.Lib.Billing
{

    /// <summary>
    /// PaymentRequest is a strongly typed object to handle the request that will be sent to Authorize.net.
    /// </summary>
    [Serializable]
    public class PaymentRequest
    {

        #region Private Members

        private string _apiLoginId = String.Empty;
        private string _apiTransactionKey = String.Empty;
        private double _amount = double.MinValue;
        private string _creditCardNumber = String.Empty;
        private DateTime _creditCardExpirationDate = DateTime.MinValue;
        private TransactionType _transactionType = TransactionType.AUTH_ONLY;
        private StringBuilder _requestParams = new StringBuilder();
        private ArrayList _requestParamsKeys = new ArrayList();
        private bool _testMode = false;


        // TODO Implement line item support
        // TODO Implement recurring/WBE support
        // TODO Implement echeck support
        // TODO Implement tax support
        // TODO Implement freight support
        // TODO Implement duty support

        private bool _delimitData = true;
        private char _delimitCharacter = char.MinValue;
        private char _encapsulateCharacter = char.MinValue;
        private bool _relayResponse = false;
        private int _duplicateWindow = int.MinValue;
        private string _firstName = String.Empty;
        private string _lastName = String.Empty;
        private string _company = String.Empty;
        private string _address = String.Empty;        
        private string _city = String.Empty;
        private string _state = String.Empty;
        private string _zip = String.Empty;
        private string _country = String.Empty;        
        private string _phone = String.Empty;
        private string _fax = String.Empty;
        private string _customerId = String.Empty;
        private string _customerIp = String.Empty;
        private string _customerTaxId = String.Empty;
        private string _email = String.Empty;
        private bool _emailCustomer = false;
        private string _merchantEmail = String.Empty;
        private string _invoiceNumber = String.Empty;
        private string _description = String.Empty;
        private string _shipToFirstName = String.Empty;
        private string _shipToLastName = String.Empty;
        private string _shipToCompany = String.Empty;
        private string _shipToAddress = String.Empty;
        private string _shipToCity = String.Empty;
        private string _shipToZip = String.Empty;
        private string _shipToCountry = String.Empty;
        private string _currencyCode = String.Empty;
        private string _method = String.Empty; // TODO Update this to use an enum
        private bool _recurringBilling = false;
        private string _cardCode = String.Empty;
        private string _transactionId = String.Empty;
        private string _authCode = String.Empty;
        private string _authIndicator = String.Empty;
        private string _cardHolderAuthValue = String.Empty;
        private string _poNumber = String.Empty;
        private bool _taxExempt = false;

        #endregion

        #region Constructor(s)

        public PaymentRequest()
        {
        }

        #endregion

        #region Properties

        public string ApiLoginId
        {
            get { return _apiLoginId; }
            set { _apiLoginId = value; }
        }

        public string ApiTransactionKey
        {
            get { return _apiTransactionKey; }
            set { _apiTransactionKey = value; }
        }

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string CreditCardNumber
        {
            get { return _creditCardNumber; }
            set { _creditCardNumber = value; }
        }

        public DateTime CreditCardExpirationDate
        {
            get { return _creditCardExpirationDate; }
            set { _creditCardExpirationDate = value; }
        }

        public TransactionType TransactionType
        {
            get { return _transactionType; }
            set { _transactionType = value; }
        }

        public bool TestMode
        {
            get { return _testMode; }
            set { _testMode = value; }
        }

        public bool DelimitData
        {
            get { return _delimitData; }
            set { _delimitData = value; }
        }

        public char DelimitCharacter
        {
            get { return _delimitCharacter; }
            set { _delimitCharacter = value; }
        }

        public char EncapsulateCharacter
        {
            get { return _encapsulateCharacter; }
            set { _encapsulateCharacter = value; }
        }

        public bool RelayResponse
        {
            get { return _relayResponse; }
            set { _relayResponse = value; }
        }

        public int DuplicateWindow
        {
            get { return _duplicateWindow; }
            set { _duplicateWindow = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public string CustomerIp
        {
            get { return _customerIp; }
            set { _customerIp = value; }
        }

        public string CustomerTaxId
        {
            get { return _customerTaxId; }
            set { _customerTaxId = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public bool EmailCustomer
        {
            get { return _emailCustomer; }
            set { _emailCustomer = value; }
        }

        public string MerchantEmail
        {
            get { return _merchantEmail; }
            set { _merchantEmail = value; }
        }

        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { _invoiceNumber = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string ShipToFirstName
        {
            get { return _shipToFirstName; }
            set { _shipToFirstName = value; }
        }

        public string ShipToLastName
        {
            get { return _shipToLastName; }
            set { _shipToLastName = value; }
        }

        public string ShipToCompany
        {
            get { return _shipToCompany; }
            set { _shipToCompany = value; }
        }

        public string ShipToAddress
        {
            get { return _shipToAddress; }
            set { _shipToAddress = value; }
        }

        public string ShipToCity
        {
            get { return _shipToCity; }
            set { _shipToCity = value; }
        }
        private string _shipToState = String.Empty;

        public string ShipToState
        {
            get { return _shipToState; }
            set { _shipToState = value; }
        }

        public string ShipToZip
        {
            get { return _shipToZip; }
            set { _shipToZip = value; }
        }

        public string ShipToCountry
        {
            get { return _shipToCountry; }
            set { _shipToCountry = value; }
        }

        public string CurrencyCode
        {
            get { return _currencyCode; }
            set { _currencyCode = value; }
        }

        public string Method
        {
            get { return _method; }
            set { _method = value; }
        }

        public bool RecurringBilling
        {
            get { return _recurringBilling; }
            set { _recurringBilling = value; }
        }

        public string CardCode
        {
            get { return _cardCode; }
            set { _cardCode = value; }
        }

        public string TransactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }

        public string AuthCode
        {
            get { return _authCode; }
            set { _authCode = value; }
        }

        public string AuthIndicator
        {
            get { return _authIndicator; }
            set { _authIndicator = value; }
        }

        public string CardHolderAuthValue
        {
            get { return _cardHolderAuthValue; }
            set { _cardHolderAuthValue = value; }
        }

        public string PoNumber
        {
            get { return _poNumber; }
            set { _poNumber = value; }
        }

        public bool TaxExempt
        {
            get { return _taxExempt; }
            set { _taxExempt = value; }
        }

        #endregion

        #region Methods

        public void AddField(string key, string value)
        {
            // Default value checking

            // Strings
            if (value == null || value == String.Empty || value == "") return;

            // Doubles
            double doubleTemp;
            if (double.TryParse(value, out doubleTemp))
            {
                if (doubleTemp == double.MinValue) return;
            }

            // Ints
            int intTemp;
            if (int.TryParse(value, out intTemp))
            {
                if (intTemp == int.MinValue) return;
            }

            // DateTime
            DateTime dateTemp;
            if (DateTime.TryParse(value, out dateTemp))
            {
                if (dateTemp == DateTime.MinValue) return;
            }

            // Chars
            char charTemp;
            if (char.TryParse(value, out charTemp))
            {
                if (charTemp == char.MinValue) return;
            }

            if (_requestParamsKeys.Contains(key)) return;
            else
            {
                _requestParamsKeys.Add(key);
            }

            if (_requestParams.Length == 0)
                _requestParams.Append(key + "=" + HttpUtility.UrlEncode(value));
            else
                _requestParams.Append("&" + key + "=" + HttpUtility.UrlEncode(value));
        }

        public override string ToString()
        {

            
            // Variable fields            
            this.AddField("x_login", this.ApiLoginId);
            this.AddField("x_tran_key", this.ApiTransactionKey);
            this.AddField("x_MD5_Hash", this.ApiTransactionKey);
            this.AddField("x_amount", this.Amount.ToString());
            this.AddField("x_card_num", this.CreditCardNumber);
            this.AddField("x_exp_date", this.CreditCardExpirationDate.ToString("MM'-'yyyy"));
            this.AddField("x_type", this.TransactionType.ToString());
            this.AddField("x_encap_char", "\"");
            this.AddField("x_test_request", (this.TestMode ? "TRUE" : "FALSE"));

            this.AddField("x_delim_data", (this.DelimitData ? "TRUE" : "FALSE"));
            this.AddField("x_delim_char", this.DelimitCharacter.ToString());
            this.AddField("x_encap_char", this.EncapsulateCharacter.ToString());
            this.AddField("x_relay_response", (this.RelayResponse ? "TRUE" : "FALSE"));
            this.AddField("x_duplicate_window", this.DuplicateWindow.ToString());
            this.AddField("x_first_name", this.FirstName);
            this.AddField("x_last_name", this.LastName);
            this.AddField("x_company", this.Company);
            this.AddField("x_address", this.Address);
            this.AddField("x_city", this.City);
            this.AddField("x_state", this.State);
            this.AddField("x_zip", this.Zip);
            this.AddField("x_country", this.Country);
            this.AddField("x_phone", this.Phone);
            this.AddField("x_fax", this.Fax);
            this.AddField("x_cust_id", this.CustomerId);
            this.AddField("x_customer_ip", this.CustomerIp);
            this.AddField("x_customer_tax_id", this.CustomerTaxId);
            this.AddField("x_email", this.Email);
            this.AddField("x_email_customer", (this.EmailCustomer ? "TRUE" : "FALSE"));
            this.AddField("x_merchant_email", this.MerchantEmail);
            this.AddField("x_invoice_num", this.InvoiceNumber);
            this.AddField("x_description", this.Description);
            this.AddField("x_ship_to_first_name", this.ShipToFirstName);
            this.AddField("x_ship_to_last_name", this.ShipToLastName);
            this.AddField("x_ship_to_company", this.ShipToCompany);
            this.AddField("x_ship_to_address", this.ShipToAddress);
            this.AddField("x_ship_to_city", this.ShipToCity);
            this.AddField("x_ship_to_state", this.ShipToState);
            this.AddField("x_ship_to_zip", this.ShipToZip);
            this.AddField("x_ship_to_country", this.ShipToCountry);
            this.AddField("x_currency_code", this.CurrencyCode);
            this.AddField("x_method", this.Method);
            this.AddField("x_card_code", this.CardCode);
            this.AddField("x_trans_id", this.TransactionId);
            this.AddField("x_auth_code", this.AuthCode);
            this.AddField("x_authentication_indicator", this.AuthIndicator);
            this.AddField("x_cardholder_authentication_value", this.CardHolderAuthValue);
            this.AddField("x_po_num", this.PoNumber);
            this.AddField("x_tax_exempt", (this.TaxExempt ? "TRUE" : "FALSE"));

            return _requestParams.ToString();
        }

        #endregion

    }
}

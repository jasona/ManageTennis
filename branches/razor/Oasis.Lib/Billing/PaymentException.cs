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

namespace Oasis.Lib.Billing
{

    /// <summary>
    /// PaymentException is thrown whenever an API-level exception has occurred.
    /// </summary>
    public class PaymentException : Exception
    {

        public PaymentException() : base() { }
        public PaymentException(string message) : base(message) { }

    }
}

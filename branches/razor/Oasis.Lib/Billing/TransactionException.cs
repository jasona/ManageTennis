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
    /// TransactionExceptions are thrown when an exception from Authorize.net is returned.
    /// </summary>
    public class TransactionException : Exception
    {

        public TransactionException() : base() { }
        public TransactionException(string message) : base(message) { }

    }

}

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
    /// Transaction type is used to drive exactly how the interactions and calls are made to Authorize.net.
    /// </summary>
    public enum TransactionType : int
    {
        AUTH_CAPTURE = 1,
        AUTH_ONLY = 2,
        CAPTURE_ONLY = 3,
        CREDIT = 4,
        VOID = 5,
        PRIOR_AUTH_CAPTURE = 6
    }

}

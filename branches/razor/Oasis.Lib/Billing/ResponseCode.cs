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
    /// ResponseCode is the parsed enum response from Authorize.net.
    /// </summary>
    public enum ResponseCode : int
    {
        Approved = 1,
        Declined = 2,
        Error = 3,
        Unknown = 4
    }

}

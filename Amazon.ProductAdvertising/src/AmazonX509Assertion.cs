/*
	Copyright © Bryan Apellanes 2015  
*/
/**********************************************************************************************
 * Copyright 2009 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not use this file 
 * except in compliance with the License. A copy of the License is located at
 *
 *       http://aws.amazon.com/apache2.0/
 *
 * or in the "LICENSE.txt" file accompanying this file. This file is distributed on an "AS IS"
 * BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under the License. 
 *
 * ********************************************************************************************
 *
 *  Amazon Product Advertising API
 *  Signed Requests Sample Code
 *
 *  API Version: 2009-03-31
 *
 */

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;
using Microsoft.Web.Services3.Security;
using Microsoft.Web.Services3.Security.Tokens;

namespace Amazon.ProductAdvertising
{
    // To sign the outgoing request with your X.509 certificate.
    // MutualCertificate10Assertion does all the work, we only
    // need to provided it with the certificate.
    public class AmazonX509Assertion : MutualCertificate10Assertion
    {
        // Constructor: initializes MutualCertificate10Assertion.
        // The certSubject param identifies the X.509 certificate to use.
        public AmazonX509Assertion(String certSubject)
        {
            // Make the certificate available to the 
            ClientX509TokenProvider = new X509TokenProvider(StoreLocation.CurrentUser,
                                                             StoreName.My, certSubject);
            ServiceX509TokenProvider = new X509TokenProvider(StoreLocation.CurrentUser,
                                                             StoreName.My, certSubject);

            // But do not encrypt the body!
            Protection.Request.EncryptBody = false;
            Protection.Response.EncryptBody = false;
        }

        public Policy Policy()
        {
            Policy policy = new Policy();
            policy.Assertions.Add(this);

            return policy;
        }

        // don't check the signature in the incoming response!
        public override SoapFilter CreateClientInputFilter(FilterCreationContext context)
        {
            return null;
        }
    }
}

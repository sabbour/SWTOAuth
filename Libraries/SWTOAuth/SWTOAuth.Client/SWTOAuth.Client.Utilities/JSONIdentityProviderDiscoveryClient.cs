//---------------------------------------------------------------------------------
// Copyright 2010 Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License"); 
// You may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, 
// INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
// CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, 
// MERCHANTABLITY OR NON-INFRINGEMENT. 

// See the Apache 2 License for the specific language governing 
// permissions and limitations under the License.
//---------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SWTOAuth.Client.Utilities
{
    public class JSONIdentityProviderDiscoveryClient
    {
        public async Task<IEnumerable<IdentityProviderInformation>> GetIdentityProviderListAsync(Uri identityProviderListServiceEndpoint)
        {
            Exception error = null;
            IEnumerable<IdentityProviderInformation> identityProviders = null;

            HttpClient httpClient = new HttpClient();
            var jsonList = await httpClient.GetStringAsync(identityProviderListServiceEndpoint);

            try
            {
                using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonList)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(IdentityProviderInformation[]));
                    identityProviders = serializer.ReadObject(ms) as IEnumerable<IdentityProviderInformation>;
                }
            }
            catch (Exception ex)
            {
                error = ex;
            }

            return identityProviders;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWTOAuth.Server
{
    public class PrincipalInitializerFactory
    {
        protected const string IdentityProviderClaimType = "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider"; 

        // Identity providers identifiers
        protected const string MSFTFederation = "urn:federation:MSFT";

        public static IPrincipalInitializer GetInitializerFromToken(SimpleWebToken token)
        {
            IPrincipalInitializer initializer = null;
            var identityProviderClaim = token.Claims[IdentityProviderClaimType];
            if(!String.IsNullOrEmpty(identityProviderClaim)) {
                switch (identityProviderClaim) {
                    case MSFTFederation: initializer = new MicrosoftADFSPrincipalInitializer(); break;
                    default: initializer = new PrincipalInitializer(); break; // if all else fails, return the default PrincipalInitializer
                }
            }

            return initializer;
        }
    }
}

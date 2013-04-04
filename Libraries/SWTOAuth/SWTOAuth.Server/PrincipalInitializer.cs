using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SWTOAuth.Server
{
    public class PrincipalInitializer : IPrincipalInitializer
    {
        protected const string DefaultNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        protected const string AcsNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"; 

        // <summary> 
        /// Used to determine whether the parameter claim type is one of the reserved 
        /// SimpleWebToken claim types: Audience, HMACSHA256, ExpiresOn or Issuer. 
        /// </summary> 
        /// <param name="keyName">The key name to be compared.</param> 
        /// <returns>True if the key is a reserved key name.</returns>
        protected virtual bool IsReservedKeyName(string keyName)
        {
            if (String.IsNullOrEmpty(keyName))
            {
                throw new ArgumentNullException("keyName");
            }

            switch (keyName)
            {
                case SwtConstants.Audience:
                case SwtConstants.ExpiresOn:
                case SwtConstants.Issuer:
                case SwtConstants.HmacSha256: return true;
                default: return false;
            }
        } 

        public virtual void InitializeCurrentPrincipalFromToken(SimpleWebToken swtToken)
        {
            ClaimsIdentity identity = new ClaimsIdentity("Federation");
            foreach (var pair in swtToken.Claims)
            {
                if (!IsReservedKeyName(pair.Key) && !string.IsNullOrEmpty(pair.Value))
                {
                    identity.AddClaim(new Claim(pair.Key, pair.Value, ClaimValueTypes.String, swtToken.Issuer));
                    if (pair.Key == AcsNameClaimType)
                    {
                        // add a default name claim from the Name identifier claim. 
                        identity.AddClaim(new Claim(DefaultNameClaimType, pair.Value, ClaimValueTypes.String, swtToken.Issuer));
                    }
                }
            }

            var principal = new ClaimsPrincipal(new List<ClaimsIdentity> { identity });
            Thread.CurrentPrincipal = principal;
        }
    }
}

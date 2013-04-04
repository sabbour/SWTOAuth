using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SWTOAuth.Server
{
    class MicrosoftADFSPrincipalInitializer : PrincipalInitializer
    {
        public const string FirstNameClaimType = "http://sts.msft.net/user/FirstName";
        public const string LastNameClaimType = "http://sts.msft.net/user/LastName";
        public const string EmailAddressClaimType = "http://sts.msft.net/user/EmailAddress";
        public const string UPNClaimType = "http://sts.msft.net/user/UPN";

        public override void InitializeCurrentPrincipalFromToken(SimpleWebToken swtToken)
        {
            ClaimsIdentity identity = new ClaimsIdentity("Federation");
            string firstName = String.Empty;
            string lastName = String.Empty;
            foreach (var pair in swtToken.Claims)
            {
                if (!IsReservedKeyName(pair.Key) && !string.IsNullOrEmpty(pair.Value))
                {
                    // Add the claim
                    identity.AddClaim(new Claim(pair.Key, pair.Value, ClaimValueTypes.Email, swtToken.Issuer));

                    // The email address is unique, set it as the "Name" identitifier accessible at User.Identity.Name
                    if (pair.Key == EmailAddressClaimType)
                        identity.AddClaim(new Claim(DefaultNameClaimType, pair.Value, ClaimValueTypes.String, swtToken.Issuer));
                }
            }

            var principal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = principal;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ClaimsDemoController : ApiController
    {
        // GET api/claims
        public Dictionary<string, string> Get()
        {
            // Get the User.Identity which would have been set by the SWTOAuth module
            var identity = User.Identity as ClaimsIdentity;

            // Parse the incoming claims
            Dictionary<string, string> parsedClaims = new Dictionary<string, string>();
            foreach (var claim in identity.Claims)
                parsedClaims[claim.Type] = claim.Value;

            // Return them, just for show
            return parsedClaims;
        }
    }
}
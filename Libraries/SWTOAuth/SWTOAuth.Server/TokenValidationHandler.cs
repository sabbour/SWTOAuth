using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Security.Claims;
using System.Web.Http;
using System.Net;

namespace SWTOAuth.Server
{
    public class TokenValidationHandler : DelegatingHandler
    {
       
        private string _sharedKeyBase64;

        public TokenValidationHandler(string sharedKeyBase64)
        {
            _sharedKeyBase64 = sharedKeyBase64;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(_sharedKeyBase64))
                throw new ApplicationException("You need to pass in your shared symmetric token signing key.");

            // Validate the existence of the Authorization header
            string authHeader = String.Empty;
            try
            {
                authHeader = request.Headers.GetValues("Authorization").First();
            }
            catch (InvalidOperationException)
            {
                throw new HttpException(400, "Missing Authorization header");
            }

            // Validate the Authorization header
            string header = "OAuth ";
            string token = string.Empty;
            if (string.CompareOrdinal(authHeader, 0, header, 0, header.Length) == 0)
            {
                token = authHeader.Remove(0, header.Length);
            }
            else
            {
                throw new HttpException(401, "The authorization header was invalid");
            }
            // Validate the token
            var validator = new SimpleWebTokenValidator
            {
                SharedKeyBase64 = _sharedKeyBase64
            };

            // Validate the token, throws HttpResponseExceptions if validation fails
            var swt = validator.ValidateToken(token);

            var swtToken = new SimpleWebToken(token);

            // Determine the identity provider and use a principal initializer accordingly
            IPrincipalInitializer principalInitalizer = PrincipalInitializerFactory.GetInitializerFromToken(swtToken);
            principalInitalizer.InitializeCurrentPrincipalFromToken(swtToken);

            return base.SendAsync(request, cancellationToken);
        }


    }
}

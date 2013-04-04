using Microsoft.Phone.Controls;
using SWTOAuth.Client.WP8.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace SWTOAuth.Client.WP8
{
    public static class SignInHandler
    {
        private static AutoResetEvent authenticateFinishedEvent = new AutoResetEvent(false);

        public static Task<bool> ValidateSecurityTokenAsync(this PhoneApplicationFrame rootFrame, RequestSecurityTokenResponseStore tokenStore, string realm, string serviceNamespace, string acsHostUrl)
        {
            //if (!tokenStore.ContainsValidRequestSecurityTokenResponse())
            //{
            AccessControlServiceSignIn.RequestSecurityTokenResponseCompleted += AccessControlServiceSignIn_RequestSecurityTokenResponseCompleted;

            var signInPage = new SignInPage(tokenStore.IsTokenExpired,realm,serviceNamespace,acsHostUrl);
            var acsPopup = new Popup() { IsOpen = true, Child = signInPage };

            Task<bool> task = Task<bool>.Factory.StartNew(() =>
            {
                authenticateFinishedEvent.WaitOne();
                rootFrame.Dispatcher.BeginInvoke(() =>
                {
                    acsPopup.IsOpen = false;
                    signInPage = null;
                    acsPopup = null;
                }
                );
                return tokenStore.ContainsValidRequestSecurityTokenResponse();
            });

            return task;
            //}
            //return Task<bool>.Factory.StartNew(() => { return true; }); ;
        }

        private static void AccessControlServiceSignIn_RequestSecurityTokenResponseCompleted(object sender, Utilities.RequestSecurityTokenResponseCompletedEventArgs e)
        {
            authenticateFinishedEvent.Set();
        }
    }
}

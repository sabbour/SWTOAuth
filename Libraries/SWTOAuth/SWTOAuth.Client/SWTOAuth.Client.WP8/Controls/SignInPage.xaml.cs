using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SWTOAuth.Client.Utilities;

namespace SWTOAuth.Client.WP8.Controls
{
    public partial class SignInPage : PhoneApplicationPage
    {
        private bool _expired;

        public SignInPage(bool expired, string realm, string serviceNamespace, string acsHostUrl)
        {
            InitializeComponent();
            SignInControl.AcsHostUrl = acsHostUrl;
            SignInControl.ServiceNamespace = serviceNamespace;
            SignInControl.Realm = realm;

            this.Loaded += SignIn_Loaded;
            this.BackKeyPress += SignIn_BackKeyPress;

            if (_expired)
                titleText.Text = "refresh sign in";
        }

        void SignIn_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SignInControl.CanGoBack)
            {
                SignInControl.GoBack();
                e.Cancel = true;
            }
        }

        void SignIn_Loaded(object sender, RoutedEventArgs e)
        {
             SignInControl.GetSecurityToken();
        }
    }
}
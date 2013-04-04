using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SampleWP8.Resources;
using SWTOAuth.Client.WP8;
using System.Threading.Tasks;
using System.Net.Http;

namespace SampleWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

         protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string jsonResult = await Get("http://your.api.com/api/values");
        }

         protected async Task<string> Get(string endpoint)
         {
             var client = new HttpClient();

             //Validate security token and renew it if expired
             bool validToken = await App.RootFrame.ValidateSecurityTokenAsync(App.RSTRStore, "your realm","your acs namespace","accesscontrol.windows.net");

             if (validToken)
             {
                 // Create request
                 var request = new HttpRequestMessage() { RequestUri = new Uri(endpoint), Method = HttpMethod.Get };

                 // Add Authorization header
                 request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", App.RSTRStore.SecurityToken);

                 var responseMessage = await client.SendAsync(request);
                 return await responseMessage.Content.ReadAsStringAsync();
             }
             else
             {
                 throw new ApplicationException("Unable to obtain a valid token.");
             }
         }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}
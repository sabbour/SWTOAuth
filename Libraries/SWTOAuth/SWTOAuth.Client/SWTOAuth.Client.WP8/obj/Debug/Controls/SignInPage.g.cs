﻿#pragma checksum "C:\Users\v-ahsab\Documents\Visual Studio 2012\Projects\Demos\OAuthWebAPIDemo\Libraries\SWTOAuth\SWTOAuth.Client\SWTOAuth.Client.WP8\Controls\SignInPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "494F30954F2FF65779F2CEC97F4E3B9A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using SWTOAuth.Client.WP8.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SWTOAuth.Client.WP8.Controls {
    
    
    public partial class SignInPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock titleText;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal SWTOAuth.Client.WP8.Controls.AccessControlServiceSignIn SignInControl;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/SWTOAuth.Client.WP8;component/Controls/SignInPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.titleText = ((System.Windows.Controls.TextBlock)(this.FindName("titleText")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.SignInControl = ((SWTOAuth.Client.WP8.Controls.AccessControlServiceSignIn)(this.FindName("SignInControl")));
        }
    }
}

